using System.Collections.Generic;
using System.Linq;
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

        [Test]
        public void Should_Score_Gutter_Game()
        {
            Given(GutterGame().ToArray());
            Sut.Score();
            Expect(new GameScored(score: 0));
        }

        private static IEnumerable<IEvent> GutterGame()
        {
            for (int i = 0; i < 20; i++)
                yield return new BallRolled(pinsKnockedDown: 0);
        } 
    }

    public class GameScored : IEvent
    {
        public int Score { get; private set; }

        public GameScored(int score)
        {
            Score = score;
        }
    }

    public class BallRolled : IEvent
    {
        public int PinsKnockedDown { get; private set; }

        public BallRolled(int pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
        }
    }
}
