using System;
using System.Collections;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    public interface IAggregate
    {
        Guid Id { get; }
        
        int Version { get; }

        void ApplyEvent(object @event);
        
        ICollection GetUncommittedEvents();
        
        void ClearUncommittedEvents();
        
        IMemento GetSnapshot();
    }
}