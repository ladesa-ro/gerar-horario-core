using Sisgea.GerarHorario.Core;

namespace Gerar_Horario_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void Test1()
        {
            Console.WriteLine("teste");
            var mainTestResult = ProgramTest.MainTest();

            //RestricoesTest.ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(mainTestResult.Item1, mainTestResult.Item2);
            RestricoesTest.HorarioAlmoçoTurmaTest(mainTestResult.Item1, mainTestResult.Item2);
            RestricoesTest.HorarioAlmoçoProfessorTest(mainTestResult.Item1, mainTestResult.Item2);
        }
    }
}