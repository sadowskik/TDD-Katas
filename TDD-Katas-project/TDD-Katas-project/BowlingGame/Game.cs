using System.Collections.Generic;
using System.Linq;
using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    public class Game : AggregateBase
    {
        private const int TypicalNumberOfFrames = 10;

        private readonly IList<Roll> _rolls = new List<Roll>();
        private readonly IList<Frame> _frames = new List<Frame>();

        protected void When(BallRolled @event)
        {
            var newRoll = new Roll(@event.PinsKnockedDown);
            _rolls.Add(newRoll);

            if (IsLastRollThePartOfPreviousFrame())
            {
                _frames.Last().SecondRoll = newRoll;
                return;
            }

            _frames.Add(new Frame(newRoll));

            if (newRoll.IsStrike())
                When(new Striked());
        }

        protected void When(Striked @event)
        {
            var newRoll = new Roll(@event.PinsKnockedDown);
            _frames.Last().SecondRoll = newRoll;
            _rolls.Add(newRoll);
        }

        private bool IsLastRollThePartOfPreviousFrame()
        {
            return (_rolls.Count - 1)%2 != 0;
        }

        public void Score()
        {
            int total = 0;

            for (int i = 0; i < TypicalNumberOfFrames; i++)
            {
                total += _frames[i].Score();

                if (_frames[i].IsStrike())
                    total += StrikeBonus(i);
                else if (_frames[i].IsSpare())
                    total += SpareBonus(i);
            }

            RaiseEvent(new GameScored(total));
        }

        private int SpareBonus(int i)
        {
            return _frames[i + 1].FirstRoll.PinsKnockedDown;
        }

        private int StrikeBonus(int i)
        {
            return _frames[i + 1].Score() + _frames[i + 2].Score();
        }
    }

    public class Frame
    {
        public Frame(Roll firstRoll)
        {
            FirstRoll = firstRoll;
        }

        public Roll FirstRoll { get; private set; }

        public Roll SecondRoll { get; set; }

        public bool IsSpare()
        {
            return FirstRoll.PinsKnockedDown == 5
                   && SecondRoll != null
                   && SecondRoll.PinsKnockedDown == 5;
        }

        public bool IsStrike()
        {
            return FirstRoll.IsStrike();
        }

        public int Score()
        {
            return FirstRoll.PinsKnockedDown + SecondRoll.PinsKnockedDown;
        }
    }

    public class Roll
    {
        public int PinsKnockedDown { get; private set; }

        public Roll(int pinsKnockedDown)
        {
            PinsKnockedDown = pinsKnockedDown;
        }

        public bool IsStrike()
        {
            return PinsKnockedDown == 10;
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

    public class Striked : BallRolled
    {
        public Striked() : base(0)
        {
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
}