using System.Collections.Generic;
using System.Linq;
using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    public class Game : AggregateBase
    {
        private readonly IList<Roll> _rolls = new List<Roll>();
        private readonly IList<Spare> _spares = new List<Spare>();

        protected void When(BallRolled @event)
        {
            var newRoll = new Roll(@event.PinsKnockedDown);
            _rolls.Add(newRoll);

            var lastSpare = _spares.LastOrDefault();
            if (lastSpare != null && lastSpare.InFrame == _rolls.Count)
                lastSpare.NextRoll = newRoll;

            if (_rolls.Last().PinsKnockedDown == 5 && _rolls[_rolls.Count - 2].PinsKnockedDown == 5)
                RaiseEvent(new SpareRolled(inFrame: _rolls.Count));
        }

        protected void When(SpareRolled @event)
        {
            _spares.Add(new Spare(@event.InFrame));
        }

        public void Score()
        {
            int total = 0;

            foreach (var spare in _spares)
            {
                //???
            }

            RaiseEvent(new GameScored(_rolls.Sum(x => x.PinsKnockedDown)));
        }
    }

    public class SpareRolled : IEvent
    {
        public int InFrame { get; private set; }

        public SpareRolled(int inFrame)
        {
            InFrame = inFrame;
        }
    }

    public class Spare
    {
        public int InFrame { get; private set; }

        public Roll NextRoll { get; set; }

        public Spare(int inFrame)
        {
            InFrame = inFrame;
        }

        public int Score()
        {
            return 10 + (NextRoll != null ? NextRoll.PinsKnockedDown : 0);
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