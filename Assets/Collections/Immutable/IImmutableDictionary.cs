using System;
using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public interface IImmutableDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>,
                                                          IReadOnlyCollection<KeyValuePair<TKey, TValue>>, 
                                                          IEnumerable<KeyValuePair<TKey, TValue>>,
                                                          IEnumerable
    {
        IImmutableDictionary<TKey, TValue> Clear();
        IImmutableDictionary<TKey, TValue> Add( TKey key, TValue value );
        IImmutableDictionary<TKey, TValue> Add( IEnumerable<KeyValuePair<TKey, TValue>> pairs );
        IImmutableDictionary<TKey, TValue> SetItem( TKey key, TValue value );
        IImmutableDictionary<TKey, TValue> Remove( TKey key );        
    }
}
