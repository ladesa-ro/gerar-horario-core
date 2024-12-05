using Sisgea.GerarHorario.Core;
using Sisgea.GerarHorario.Core.Dtos.Entidades;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;
using System.Security.Cryptography;

public static class RestricoesTest
{
    public static void ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(GerarHorarioContext contexto, IEnumerable<HorarioGeradoAula> horarioGerado)
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
}