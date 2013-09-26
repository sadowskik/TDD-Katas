using System;
using System.Collections;
using System.Collections.Generic;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    public abstract class AggregateBase : IAggregate, IEquatable<IAggregate>
    {
        private readonly ICollection<object> _uncommittedEvents = new LinkedList<object>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        protected void RaiseEvent(object @event)
        {
            ((IAggregate) this).ApplyEvent(@event);
            _uncommittedEvents.Add(@event);
        }

        void IAggregate.ApplyEvent(object @event)
        {
            RedirectToWhen.TryInvokeEvent(this, @event);            
            Version++;
        }

        ICollection IAggregate.GetUncommittedEvents()
        {
            return (ICollection) _uncommittedEvents;
        }

        void IAggregate.ClearUncommittedEvents()
        {
            _uncommittedEvents.Clear();
        }

        IMemento IAggregate.GetSnapshot()
        {
            var snapshot = GetSnapshot();
            snapshot.Id = Id;
            snapshot.Version = Version;
            return snapshot;
        }

        protected virtual IMemento GetSnapshot()
        {
            return null;
        }

        public bool Equals(IAggregate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AggregateBase) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}