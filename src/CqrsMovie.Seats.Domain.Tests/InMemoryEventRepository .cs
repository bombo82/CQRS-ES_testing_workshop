﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqrsMovie.Seats.Domain.Tests.Helpers;
using Muflone;
using Muflone.Core;
using Muflone.Messages.Events;
using Muflone.Persistence;

namespace CqrsMovie.Seats.Domain.Tests
{
    public class InMemoryEventRepository_
    {
        /// <summary>
        /// https://github.com/luizdamim/NEventStoreExample/tree/master/NEventStoreExample.Test
        /// </summary>
        public class InMemoryEventRepository : IRepository
        {
            private IEnumerable<DomainEvent> givenEvents;
            public IEnumerable<DomainEvent> Events { get; private set; }

            private static TAggregate ConstructAggregate<TAggregate>()
            {
                return (TAggregate)Activator.CreateInstance(typeof(TAggregate), true);
            }

            public void Dispose()
            {
                // no op
            }

            public virtual void ApplyGivenEvents(IList<DomainEvent> events)
            {
                givenEvents = events;
            }

            public async Task<TAggregate> GetById<TAggregate>(IDomainId id) where TAggregate : class, IAggregate
            {
                return await GetById<TAggregate>(id, 0);
            }

            public Task<TAggregate> GetById<TAggregate>(IDomainId id, int version) where TAggregate : class, IAggregate
            {
                var aggregate = ConstructAggregate<TAggregate>();
                givenEvents.ForEach(aggregate.ApplyEvent);
                return Task.FromResult(aggregate);
            }

            public Task Save(IAggregate aggregate, Guid commitId, Action<IDictionary<string, object>> updateHeaders)
            {
                Events = aggregate.GetUncommittedEvents().Cast<DomainEvent>();
                return Task.CompletedTask;
            }
        }
    }
}