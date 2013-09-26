using System;
using FluentAssertions;
using NUnit.Framework;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    [TestFixture]
    public class Specification<TAggregate> where TAggregate : IAggregate
    {
        protected IEvent[] Nothing = new IEvent[0];

        protected TAggregate Sut;

        [SetUp]
        public void SetUp()
        {
            Sut = CreateAggregate();
        }

        protected virtual TAggregate CreateAggregate()
        {
            return Activator.CreateInstance<TAggregate>();
        }

        protected void Given(params IEvent[] events)
        {
            foreach (var @event in events)
                Sut.ApplyEvent(@event);
        }

        protected void Expect(params IEvent[] events)
        {
            Sut.GetUncommittedEvents().ShouldBeEquivalentTo(events);            
        }
    }
}