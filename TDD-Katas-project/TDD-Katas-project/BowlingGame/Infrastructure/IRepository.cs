using System;
using System.Collections.Generic;

namespace TDD_Katas_project.BowlingGame.Infrastructure
{
    public interface IRepository
    {
        TAggregate GetById<TAggregate>(Guid id) where TAggregate : class, IAggregate;
        TAggregate GetById<TAggregate>(Guid id, int version) where TAggregate : class, IAggregate;
        void Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders);
    }
}