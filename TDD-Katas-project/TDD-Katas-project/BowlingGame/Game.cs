using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    public class Game : AggregateBase
    {                
        public void Score()
        {
            RaiseEvent(new GameScored(0));
        }
    }
}