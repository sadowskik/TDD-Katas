using System.Collections.Generic;
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

        [Test]
        public void Should_Score_One_Spare()
        {
            Given(
                Spare(),                 
                RollMany(pinsKnockedDown: 0, times: 17));

            Sut.Score();

            Expect(new GameScored(16));
        }

        private static IEnumerable<BallRolled> RollMany(int pinsKnockedDown, int times)
        {            
            for (int i = 0; i < times; i++)
                yield return new BallRolled(pinsKnockedDown);            
        }

        private static IEnumerable<IEvent> Spare()
        {
            return new IEvent[]
            {
                new BallRolled(pinsKnockedDown: 5),
                new BallRolled(pinsKnockedDown: 5)
            };
        }
    }    
}