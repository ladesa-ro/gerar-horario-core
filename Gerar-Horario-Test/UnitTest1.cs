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
            RestricoesTest.ProfessorNaoPodeTrabalharEmTresTurnosDiferentesTest(ProgramTest.MainTest().Item2, ProgramTest.MainTest().Item1);
        }
    }
}