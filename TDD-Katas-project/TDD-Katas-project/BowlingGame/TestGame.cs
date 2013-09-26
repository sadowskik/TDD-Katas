using NUnit.Framework;
using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    [TestFixture]
    [Category("TheBowlingGameKata")]
    public class TestGame : Specification<Game>
    {
        protected override Game CreateAggregate()
        {
            return new Game();
        }
    }
}
