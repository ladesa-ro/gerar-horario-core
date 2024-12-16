using Sisgea.GerarHorario.Core;
using Sisgea.GerarHorario.Core.Dtos.Entidades;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;
using System.Security.Cryptography;

public static class RestricoesTest
{

    //RESTRIÇÃO TEST: O professor não pode trabalhar 3 turnos e o professor não pode trabalhar de manhã e à noite.
    public static void ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            foreach (var diaSemanaIso in Enumerable.Range(contexto.Options.DiaSemanaInicio, contexto.Options.DiaSemanaFim))
            {
                var propostasManha = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 0 && aula.IntervaloDeTempo <= 4
                                     )
                                     select aula;

                var propostasTarde = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 5 && aula.IntervaloDeTempo <= 9
                                     )
                                     select aula;

                var propostasNoite = from aula in horarioGerado
                                     where aula.ProfessorId == professor.Id
                                     && aula.DiaDaSemanaIso == diaSemanaIso
                                     &&
                                     (
                                         aula.IntervaloDeTempo >= 10 && aula.IntervaloDeTempo <= 14
                                     )
                                     select aula;

                if (propostasManha.Any() && propostasTarde.Any() && propostasNoite.Any())
                {
                    if (propostasManha.Any() && propostasTarde.Any() && propostasNoite.Any())
                    {
                        Assert.Fail("ERROR: PROFESSOR TRABALHA NOS 3 TURNOS");

                    }
                    else if (propostasManha.Any() && propostasNoite.Any())
                    {
                        Assert.Fail("ERROR: PROFESSOR TRABALHA NO TURNO MANHA E NOITE");
                    }

                }
            }
        }

    }

    //RESTRIÇÃO TEST: Mínimo de 1h30 de almoço para o professor e turmas.
    public static void HorarioAlmoçoTurmaTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var turma in contexto.Options.Turmas)
        {

            var propostaAulaTurma = from proposta in horarioGerado
                                    where (proposta.DiaDaSemanaIso == 2 || proposta.DiaDaSemanaIso == 4)
                                        && proposta.TurmaId == turma.Id
                                        && (
                                            Intervalo.VerificarIntervalo(
                                                new Intervalo("11:30:00", "11:59:59"),
                                                contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioFim
                                            )
                                            || Intervalo.VerificarIntervalo(
                                                new Intervalo("13:00:00", "13:30:00"),
                                                 contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioInicio
                                            )
                                        )
                                    select proposta;

            if (propostaAulaTurma.Count() > 2)
            {
                Assert.Fail("ERROR: HORARIO DE ALMOÇO TURMA ERROR");
            }

        }
    }
    public static void HorarioAlmoçoProfessorTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {

            var propostaAulaProfessor = from proposta in horarioGerado
                                        where (proposta.DiaDaSemanaIso == 2 || proposta.DiaDaSemanaIso == 4)
                                            && proposta.ProfessorId == professor.Id
                                            && (
                                                Intervalo.VerificarIntervalo(
                                                    new Intervalo("11:30:00", "11:59:59"),
                                                    contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioFim
                                                )
                                                || Intervalo.VerificarIntervalo(
                                                    new Intervalo("13:00:00", "13:30:00"),
                                                     contexto.Options.HorariosDeAula[proposta.IntervaloDeTempo].HorarioInicio
                                                )
                                            )
                                        select proposta;

            if (propostaAulaProfessor.Count() > 2)
            {
                Assert.Fail("ERROR: HORARIO DE ALMOÇO PROFESSOR ERROR");
            }

        }
    }

    public static void PRDTest(IEnumerable<HorarioGeradoAula> horarioGerado, GerarHorarioContext contexto)
    {
        foreach (var professor in contexto.Options.Professores)
        {
            var propostaAulaProfessor = from proposta in horarioGerado
                                        where proposta.DiaDaSemanaIso == professor.DiaPRD
                                        && proposta.ProfessorId == professor.Id
                                        select proposta.DiaDaSemanaIso;

            if (propostaAulaProfessor.Any())
            {
                Assert.Fail($"ERROR: PROFESSOR TRABALHANDO NO SEU PRD \n Prova: Professor {professor.Id} esta trabalhando no dia {professor.DiaPRD}");

            }
        }
    }
}
