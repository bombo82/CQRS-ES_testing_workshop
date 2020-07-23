using System;
using System.Collections.Generic;

namespace CqrsMovie.Seats.Domain.Tests.Helpers
{
    public static class Extensions
    {
        //Stolen from Castle.Core.Internal
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
                return;
            foreach (T obj in items)
                action(obj);
        }
    }
}