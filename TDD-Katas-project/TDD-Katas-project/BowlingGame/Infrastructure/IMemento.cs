using System;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    public interface IMemento
    {
        Guid Id { get; set; }
        int Version { get; set; }
    }
}