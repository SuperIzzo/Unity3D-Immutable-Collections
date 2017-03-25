using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections
{
    public interface IReadOnlyCollection<T> : IEnumerable<T>, IEnumerable
    {
        int Count { get; }
    }
}