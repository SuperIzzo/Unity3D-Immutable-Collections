using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections
{
    public interface IReadOnlyList<T> : IReadOnlyCollection<T>,
                                        IEnumerable<T>, 
                                        IEnumerable
    {        
        T this[ int index ] { get; }
    }
}
