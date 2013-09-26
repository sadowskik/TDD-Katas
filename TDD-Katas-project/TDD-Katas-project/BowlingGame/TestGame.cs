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
            Given(
                RollMany(pinsKnockedDown: 0, times: 20));

            Sut.Score();

            Expect(new GameScored(score: 0));
        }

        [Test]
        public void Should_Score_Game_With_All_Ones()
        {
            Given(
                RollMany(pinsKnockedDown: 1, times: 20));

            Sut.Score();

            Expect(new GameScored(score: 20));
        }

        private static IEvent[] RollMany(int pinsKnockedDown, int times)
        {
            var rolls = new IEvent[times];

            for (int i = 0; i < times; i++)
                rolls[i] = new BallRolled(pinsKnockedDown);

            return rolls;
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