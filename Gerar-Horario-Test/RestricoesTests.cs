using Sisgea.GerarHorario.Core;
using Sisgea.GerarHorario.Core.Dtos.Entidades;
using Sisgea.GerarHorario.Core.Dtos.HorarioGerado;
using System.Security.Cryptography;

public static class RestricoesTest
{
    public static void ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(GerarHorarioContext contexto, IEnumerable<HorarioGeradoAula> horarioGerado)
    {

        var propostasManha = from aula in horarioGerado
                             select aula;

        foreach (var aula in propostasManha)
        {
            Console.WriteLine($"- Dia TESTE: {aula.DiaDaSemanaIso} | Intervalo: {aula.IntervaloDeTempo} | Professor: {aula.ProfessorId} | Diario: {aula.DiarioId}");
        }
        /* foreach (var professor in contexto.Options.Professores)
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

                 foreach (var aula in propostasManha)
                 {
                     Console.WriteLine($"- Dia MANHA: {aula.DiaDaSemanaIso} | Intervalo: {aula.IntervaloDeTempo} | Professor: {aula.ProfessorId} | Diario: {aula.DiarioId}");
                 }

                 // Iterar sobre as aulas da tarde
                 foreach (var aula in propostasTarde)
                 {
                     Console.WriteLine($"- Dia TARDE: {aula.DiaDaSemanaIso} | Intervalo: {aula.IntervaloDeTempo} | Professor: {aula.ProfessorId} | Diario: {aula.DiarioId}");
                 }

                 // Iterar sobre as aulas da noite
                 foreach (var aula in propostasNoite)
                 {
                     Console.WriteLine($"- Dia NOITE: {aula.DiaDaSemanaIso} | Intervalo: {aula.IntervaloDeTempo} | Professor: {aula.ProfessorId} | Diario: {aula.DiarioId}");
                 }


                 if (!propostasManha.Any() && !propostasTarde.Any() && !propostasNoite.Any())
                 {
                     System.Console.WriteLine("executado: o professor");
                     Assert.Fail("ERROR: TRABALHA NOS 3 TURNOS");
                 }
             }
         }*/

    }
}