using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using Google.OrTools.ModelBuilder;
using Google.OrTools.Sat;
using Sisgea.GerarHorario.Core.Dtos.Configuracoes;
using Sisgea.GerarHorario.Core.Dtos.Entidades;

namespace Sisgea.GerarHorario.Core;

using CombinacaoAula = (int diaSemanaIso, int intervaloIndex, string turmaId, string diarioId, string professorId);
using LinearExpr = Google.OrTools.Sat.LinearExpr;

public class Restricoes
{
    ///<summary>
    /// UTILITÁRIO: Verifica que um (diaSemanaIso, intervalo)
    /// pode ocorer em um conjunto de disponibilidades.
    ///</summary>
    public static bool VerificarIntervaloEmDisponibilidades(
        IEnumerable<DisponibilidadeDia> disponibilidades,
        int diaSemanaIso,
        Intervalo intervalo
    )
    {

        return disponibilidades.Any(disponibilidade =>
        {
            if (disponibilidade.DiaSemanaIso == diaSemanaIso)
            {
                return Intervalo.VerificarIntervalo(disponibilidade.Intervalo, intervalo);
            }

            return false;
        });
    }

    ///<summary>
    /// UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis
    /// sem respeitar nenhum critério.
    ///</summary>
    public static IEnumerable<CombinacaoAula> GerarTodasAsCombinacoesPossiveisInclusiveIndisponiveis(GerarHorarioOptions options)
    {
        for (int diaSemanaIso = options.DiaSemanaInicio; diaSemanaIso <= options.DiaSemanaFim; diaSemanaIso++)
        {
            for (int intervaloIndex = 0; intervaloIndex < options.HorariosDeAula.Length; intervaloIndex++)
            {
                foreach (var turma in options.Turmas)
                {
                    foreach (var diario in options.DiariosByTurmaId(turma.Id))
                    {
                        yield return (diaSemanaIso, intervaloIndex, turma.Id, diario.Id, diario.ProfessorId);
                    }
                }
            }
        }
    }

    ///<summary>
    /// UTILITÁRIO: Gera uma lista com todas as combinações de aula possíveis,
    /// respeitando as disponibilidades da turma e disponibilidades do professor.
    ///</summary>
    public static IEnumerable<CombinacaoAula> GerarCombinacoesComDisponibilidade(GerarHorarioOptions options)
    {
        foreach (var combinacao in Restricoes.GerarTodasAsCombinacoesPossiveisInclusiveIndisponiveis(options))
        {
            // =====================================================================================
            var intervaloDeTempo = options.HorarioDeAulaFindByIdStrict(combinacao.intervaloIndex);

            var turma = options.TurmaFindByIdStrict(combinacao.turmaId);
            var diario = options.DiarioFindByIdStrict(combinacao.diarioId);

            var professor = options.ProfessorFindByIdStrict(
                diario.ProfessorId,
                exceptionContext: $" (diário: {diario.Id}, turma: {turma.Id})"
            )!;

            // =====================================================================================

            var disponivelParaTurma = Restricoes.VerificarIntervaloEmDisponibilidades(turma.Disponibilidades, combinacao.diaSemanaIso, intervaloDeTempo);

            // ===================================

            var disponivelParaProfessor = Restricoes.VerificarIntervaloEmDisponibilidades(professor.Disponibilidades, combinacao.diaSemanaIso, intervaloDeTempo);

            // ===================================

            var disponivel = disponivelParaTurma && disponivelParaProfessor;

            // =====================================================================================

            if (disponivel)
            {
                yield return combinacao;
            }
        }
    }

    ///<summary>
    /// RESTRIÇÃO: Diário: respeitar limite de quantidade máxima na semana.
    ///</summary>
    public static void AplicarLimiteDeDiarioNaSemana(
      GerarHorarioContext contexto
    )
    {
        foreach (var turma in contexto.Options.Turmas)
        {
            foreach (var diario in turma.DiariosDaTurma)
            {
                var propostasDoDiario = from propostaAula in contexto.TodasAsPropostasDeAula
                                        where
                                            propostaAula.DiarioId == diario.Id
                                        select propostaAula.ModelBoolVar;

                if (propostasDoDiario.Any())
                {
                    contexto.Model.Add(LinearExpr.Sum(propostasDoDiario) <= diario.QuantidadeMaximaSemana);
                }
            }
        }

    }

    ///<summary>
    /// RESTRIÇÃO: Turma: não ter mais de uma aula ativa ao mesmo tempo.
    ///</summary>
    public static void AplicarLimiteDeNoMaximoUmDiarioAtivoPorTurmaEmUmHorario(GerarHorarioContext contexto)
    {
        foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
        {
            foreach (var intervaloIndex in Enumerable.Range(0, contexto.Options.HorariosDeAula.Length))
            {
                foreach (var turma in contexto.Options.Turmas)
                {
                    var propostas = (from propostaAula in contexto.TodasAsPropostasDeAula
                                     where
                                        propostaAula.DiaSemanaIso == diaSemanaIso // mesmo dia
                                        && propostaAula.IntervaloIndex == intervaloIndex // mesmo horário
                                        && propostaAula.TurmaId == turma.Id // mesma turma

                                     select propostaAula.ModelBoolVar).ToList();

                    if (propostas.Count != 0)
                    {
                        contexto.Model.AddAtMostOne(propostas);
                    }

                }
            }

            Console.WriteLine("");
        }
    }

    ///<summary>
    /// RESTRIÇÃO: Professor: não ter mais de uma aula ativa ao mesmo tempo.
    ///</summary>
    public static void AplicarLimiteDeNoMaximoUmDiarioAtivoPorProfessorEmUmHorario(GerarHorarioContext contexto)
    {

        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                foreach (var intervaloIndex in Enumerable.Range(0, contexto.Options.HorariosDeAula.Length))
                {
                    var propostas = from propostaDeAula in contexto.TodasAsPropostasDeAula
                                    where
                                        propostaDeAula.DiaSemanaIso == diaSemanaIso
                                        &&
                                        propostaDeAula.IntervaloIndex == intervaloIndex
                                        &&
                                        contexto.Options.ProfessorEstaVinculadoAoDiario(diarioId: propostaDeAula.DiarioId, professorId: professor.Id)
                                    select propostaDeAula.ModelBoolVar;

                    if (propostas.Any())
                    {

                        contexto.Model.AddAtMostOne(propostas);
                    }

                }
            }
        }
    }

    ///<summary>
    /// RESTRIÇÃO: Mínimo de 1h30 de almoço para o professor
    ///</summary>

    public static void HorarioAlmocoProfessor(GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                var propostaAulaProfessor = from proposta in contexto.TodasAsPropostasDeAula
                                            where proposta.DiaSemanaIso == diaSemanaIso
                                                && proposta.ProfessorId == professor.Id
                                                && (
                                                    Intervalo.VerificarIntervalo(
                                                        new Intervalo("11:30:00", "12:00:00"),
                                                        proposta.Intervalo.HorarioFim
                                                    )
                                                    || Intervalo.VerificarIntervalo(
                                                        new Intervalo("13:00:00", "13:30:00"),
                                                        proposta.Intervalo.HorarioInicio
                                                    )
                                                )
                                            select proposta.ModelBoolVar;

                contexto.Model.AddAtMostOne(propostaAulaProfessor);
            }
        }
    }

    ///<summary>
    /// RESTRIÇÃO: Mínimo de 1h30 de almoço para a turma
    ///</summary>

    public static void HorarioAlmocoTurma(GerarHorarioContext contexto)
    {
        foreach (var turma in contexto.Options.Turmas)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                var propostaAulaProfessor = from proposta in contexto.TodasAsPropostasDeAula
                                            where proposta.DiaSemanaIso == diaSemanaIso
                                                && proposta.TurmaId == turma.Id
                                                && (
                                                    Intervalo.VerificarIntervalo(
                                                        new Intervalo("11:30:00", "12:00:00"),
                                                        proposta.Intervalo.HorarioFim
                                                    )
                                                    || Intervalo.VerificarIntervalo(
                                                        new Intervalo("13:00:00", "13:30:00"),
                                                        proposta.Intervalo.HorarioInicio
                                                    )
                                                )
                                            select proposta.ModelBoolVar;

                contexto.Model.AddAtMostOne(propostaAulaProfessor);
            }
        }
    }

    ///<summary>
    /// RESTRIÇÃO: O professor não pode trabalhar 3 turnos.
    ///</summary>
    ///


    public static void ProfessorNaoPodeTrabalharEmTresTurnosDiferentes(GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                var propostasManha = from proposta in contexto.TodasAsPropostasDeAula
                                     where
                                     proposta.ProfessorId == professor.Id
                                     &&
                                     proposta.DiaSemanaIso == diaSemanaIso
                                     &&
                                     proposta.IntervaloIndex >= 0 && proposta.IntervaloIndex <= 4
                                     select proposta.ModelBoolVar;

                var propostasTarde = from proposta in contexto.TodasAsPropostasDeAula
                                     where
                                     proposta.ProfessorId == professor.Id
                                     &&
                                     proposta.DiaSemanaIso == diaSemanaIso
                                     &&
                                     proposta.IntervaloIndex >= 5 && proposta.IntervaloIndex <= 9

                                     select proposta.ModelBoolVar;

                var propostasNoite = from proposta in contexto.TodasAsPropostasDeAula
                                     where
                                     proposta.ProfessorId == professor.Id
                                     &&
                                     proposta.DiaSemanaIso == diaSemanaIso
                                     &&
                                     proposta.IntervaloIndex >= 10 && proposta.IntervaloIndex <= 14

                                     select proposta.ModelBoolVar;

                /*
                Possibilidades

                | descricao            | manha | tarde | noite |
                | -------------------- | ----- | ----- | ----- |
                | nao dar aula no dia  | false | false | false | 
                | dar aula so de MANHA |  true | false | false | 
                |  dar aula so a tarde | false |  true | false | 
                |  dar aula so a noite | false | false |  true | 
                |       manha e tarde  |  true |  true | false | 
                |       tarde e noite  | false |  true |  true | 
                */
                if (propostasManha.Any() && propostasTarde.Any() && propostasNoite.Any())
                {
                    //Console.WriteLine("toppp");
                    long[,] possibilidadesPermitidas = {
                        { 0, 0, 0 }, // nao dar aula no dia
                        { 1, 0, 0 }, //dar aula so de MANHA
                        { 0, 1, 0 }, //dar aula so a tarde
                        { 0, 0, 1 }, //dar aula so a noite
                        { 1, 1, 0 }, //manha e tarde
                        { 0, 1, 1 }  //tarde e noite
                    };


                    var qntAulasManha = contexto.Model.NewIntVar(0, propostasManha.Count(), $"qnt_ativo_{professor.Id}_{diaSemanaIso}_Manha");
                    var qntAulasTarde = contexto.Model.NewIntVar(0, propostasTarde.Count(), $"qnt_ativo_{professor.Id}_{diaSemanaIso}_Tarde");
                    var qntAulasNoite = contexto.Model.NewIntVar(0, propostasNoite.Count(), $"qnt_ativo_{professor.Id}_{diaSemanaIso}_Noite");

                    contexto.Model.Add(qntAulasManha == LinearExpr.Sum(propostasManha));
                    contexto.Model.Add(qntAulasTarde == LinearExpr.Sum(propostasTarde));
                    contexto.Model.Add(qntAulasNoite == LinearExpr.Sum(propostasNoite));

                    var alumgaAulaManha = contexto.Model.NewBoolVar($"ativo_{professor.Id}_{diaSemanaIso}_Manha"); // == LinearExpr.Sum(propostasManha) > 0;
                    var alumgaAulaTarde = contexto.Model.NewBoolVar($"ativo_{professor.Id}_{diaSemanaIso}_Tarde"); // == LinearExpr.Sum(propostasTarde) > 0;
                    var alumgaAulaNoite = contexto.Model.NewBoolVar($"ativo_{professor.Id}_{diaSemanaIso}_Noite"); // == LinearExpr.Sum(propostasNoite) > 0;

                    contexto.Model.Add(qntAulasManha >= 1).OnlyEnforceIf(alumgaAulaManha);
                    contexto.Model.Add(qntAulasTarde >= 1).OnlyEnforceIf(alumgaAulaTarde);
                    contexto.Model.Add(qntAulasNoite >= 1).OnlyEnforceIf(alumgaAulaNoite);

                    contexto.Model.Add(qntAulasManha < 1).OnlyEnforceIf(alumgaAulaManha.Not());
                    contexto.Model.Add(qntAulasTarde < 1).OnlyEnforceIf(alumgaAulaTarde.Not());
                    contexto.Model.Add(qntAulasNoite < 1).OnlyEnforceIf(alumgaAulaNoite.Not());

                    contexto.Model.AddAllowedAssignments([alumgaAulaManha, alumgaAulaTarde, alumgaAulaNoite]).AddTuples(possibilidadesPermitidas);
                }

            }
        }
    }

    ///<summary>
    /// RESTRIÇÃO: A diferença entre os turnos de trabalho do professor deve ser de 12 horas.
    ///</summary>

    static bool debugValor(PropostaDeAula carro)
    {
        // Console.WriteLine($"- LA ELE Dia: {carro.DiaSemanaIso} | Intervalo: {carro.IntervaloIndex} | Professor: {carro.ProfessorId} | Diario: {carro.DiarioId}");
        return true;
    }
    public static void DiferencaTurnos12Horas(GerarHorarioContext contexto)
    {
        foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim - 1))
        {
            foreach (var professor in contexto.Options.Professores)
            {
                var propostasNoite = from proposta in contexto.TodasAsPropostasDeAula
                                     where
                                     proposta.ProfessorId == professor.Id
                                     &&
                                     proposta.DiaSemanaIso == diaSemanaIso
                                     &&
                                     proposta.IntervaloIndex >= 10 && proposta.IntervaloIndex <= 14
                                     select proposta;

                foreach (var propostaNoite in propostasNoite)
                {
                    // DIA SEGUIBTE
                    int diaSemanaIsoSeguinte = (diaSemanaIso % 7) + 1;

                    var propostasConflitantesManhaSeguinte = from proposta in contexto.TodasAsPropostasDeAula
                                                             where proposta.DiaSemanaIso == diaSemanaIsoSeguinte
                                                             &&
                                                             proposta.ProfessorId == propostaNoite.ProfessorId
                                                                   && proposta.IntervaloIndex >= 0 && proposta.IntervaloIndex <= 4//SELECIONA OS INTERVALOS DE 0 A 4
                                                                   && proposta.IntervaloIndex <= propostaNoite.IntervaloIndex - 10//DIMUI 10 DO ULTIMO INTERVALO QUE SERA IGUAL AO INTERVALO QUE DEVERA SER REMOVIDO
                                                                   && debugValor(proposta)
                                                             select proposta.ModelBoolVar;

                    var negatedVariables = propostasConflitantesManhaSeguinte.Select(v => v.Not()).ToArray();

                    contexto.Model.AddBoolAnd(negatedVariables).OnlyEnforceIf(propostaNoite.ModelBoolVar);
                }
            }
        }

    }
    ///<summary>
    /// RESTRIÇÃO: Permitir escolher dias e turnos de aula de um professor.
    ///</summary>
    public static void AgruparDisciplinasParametro(GerarHorarioContext contexto, string professorId, int diaSemana, Intervalo intervalos)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            if (professor.Id == professorId)
            {
                professor.IntervaloEscolhido = intervalos;
                professor.DiaAulaEscolhido = diaSemana;
            }
        }

        var propostasExcluir = from proposta in contexto.TodasAsPropostasDeAula
                               where proposta.ProfessorId == professorId
                               && proposta.DiaSemanaIso != diaSemana
                               select proposta.ModelBoolVar;

        var propostasEscolhidas = from proposta in contexto.TodasAsPropostasDeAula
                                  where proposta.ProfessorId != professorId
                                  && proposta.DiaSemanaIso == diaSemana
                                  && Intervalo.VerificarIntervalo(
                                                          new Intervalo(intervalos.HorarioInicio, intervalos.HorarioFim),
                                                          proposta.Intervalo.HorarioInicio
                                                      )
                                  select proposta.ModelBoolVar;

        var negatedVariables = propostasExcluir.Select(v => v.Not()).ToArray();
        contexto.Model.AddBoolAnd(negatedVariables);

        var negatedVariables1 = propostasEscolhidas.Select(v => v.Not()).ToArray();
        contexto.Model.AddBoolAnd(negatedVariables1);
    }


    ///<summary>
    /// RESTRIÇÃO: Todo professor deve ter 1 dia sem aulas (PRD na segunda ou na sexta).
    ///</summary>
    public static void PadronizarPRD(GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {

            int[] DiaSemanaIsoPRD = { 1, 5 };
            Random sorteio = new Random();
            int indiceSorteado = sorteio.Next(DiaSemanaIsoPRD.Length);
            int diaSorteado = DiaSemanaIsoPRD[indiceSorteado];

            professor.DiaPRD = diaSorteado;
            //System.Console.WriteLine("O PRD do professor " + professor.Id + " sera no dia " + diaSorteado);

            var propostasSorteada = from proposta in contexto.TodasAsPropostasDeAula
                                    where proposta.ProfessorId == professor.Id
                                    && proposta.DiaSemanaIso == diaSorteado
                                    select proposta.ModelBoolVar;

            var negatedVariables = propostasSorteada.Select(v => v.Not()).ToArray();
            contexto.Model.AddBoolAnd(negatedVariables);

        }
    }

    ///<summary>
    /// RESTRIÇÃO: Permitir escolher o dia de PRD de um professor.
    ///</summary>
    public static void EspecificarPRD(GerarHorarioContext contexto, string idProfessor, int DiaSemanaEscolhido)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            if (professor.Id == idProfessor)
            {
                professor.DiaPRD = DiaSemanaEscolhido;
            }
        }
        var propostasSorteada = from proposta in contexto.TodasAsPropostasDeAula
                                where proposta.ProfessorId == idProfessor
                                && proposta.DiaSemanaIso == DiaSemanaEscolhido
                                select proposta.ModelBoolVar;


        Console.WriteLine("o PRD do professor " + idProfessor + " foi escolhido para ser no " + DiaSemanaEscolhido);
        var negatedVariables = propostasSorteada.Select(v => v.Not()).ToArray();
        contexto.Model.AddBoolAnd(negatedVariables);
    }

}



///<summary>
/// RESTRIÇÃO: N/A
///</summary>
