using System.Collections.Generic;
using System.Linq;
using TDD_Katas_project.BowlingGame.Infrastructure;

namespace TDD_Katas_project.BowlingGame
{
    public class Game : AggregateBase
    {
        private readonly IList<Roll> _rolls = new List<Roll>();
        private readonly IList<Frame> _frames = new List<Frame>();

        protected void When(BallRolled @event)
        {
            var newRoll = new Roll(@event.PinsKnockedDown);

            if (_rolls.Count % 2 == 0)
            {
                _frames.Add(new Frame(newRoll));

                if (newRoll.PinsKnockedDown == 10)
                {
                    _rolls.Add(newRoll);
                    When(new Striked());
                    return;
                }                                    
            }
            else
                _frames.Last().SecondRoll = newRoll;

            _rolls.Add(newRoll);
        }
        
        public void Score()
        {
            int total = 0;

            for (int i = 0; i < _frames.Count; i++)
            {
                total += _frames[i].Score();

                if (_frames[i].IsStrike())
                    total += _frames[i + 1].Score();
                else if (_frames[i].IsSpare())
                    total += _frames[i + 1].FirstRoll.PinsKnockedDown;
            }

            RaiseEvent(new GameScored(total));
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