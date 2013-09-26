using System.Collections.Generic;
using System.Linq;
using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    public class Game : AggregateBase
    {
        private readonly IList<Roll> _rolls = new List<Roll>();

        protected void When(BallRolled @event)
        {
            _rolls.Add(new Roll(@event.PinsKnockedDown));
        }

        public void Score()
        {
            RaiseEvent(new GameScored(_rolls.Sum(x => x.PinsKnockedDown)));
        }
    }

    public class Roll
    {
        public int PinsKnockedDown { get; private set; }

        public Roll(int pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
        }
    }
}