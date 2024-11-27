using Sisgea.GerarHorario.Core;

namespace Gerar_Horario_Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            System.Console.WriteLine("teste");
            var mainTestResult = ProgramTest.MainTest();
            RestricoesTest.ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(mainTestResult.Item2, mainTestResult.Item1);
        }

        [Test]
        public void Test1()
        {
        }
    }
}