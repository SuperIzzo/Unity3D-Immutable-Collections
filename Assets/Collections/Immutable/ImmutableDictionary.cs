using System;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public static class ImmutableDictionary
    {
        public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>()
        {
            return ImmutableDictionary<TKey, TValue>.Empty;
        }
        
        public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>( IEqualityComparer<TKey> comparer )
        {
            return ImmutableDictionary<TKey, TValue>.Empty.WithComparer( comparer );
        }       

        public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>( 
                                                                IEnumerable<KeyValuePair<TKey, TValue>> items )
        {
            return ImmutableDictionary<TKey, TValue>.Empty.AddRange( items );
        }

        public static ImmutableDictionary<TKey, TValue>  CreateRange<TKey, TValue>( 
                                                                IEqualityComparer<TKey> comparer, 
                                                                IEnumerable<KeyValuePair<TKey, TValue>> items )
        {
            return ImmutableDictionary<TKey, TValue>.Empty.WithComparer( comparer ).AddRange( items );
        }

        public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
        {
            return Create<TKey, TValue>().ToBuilder();
        }

        public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>( 
                                                                IEqualityComparer<TKey> comparer )
        {
            return Create<TKey, TValue>( comparer ).ToBuilder();
        }

        
        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>( 
                                                                this IEnumerable<KeyValuePair<TKey, TValue>> source,
                                                                IEqualityComparer<TKey> comparer )
        {            
            var existingDictionary = source as ImmutableDictionary<TKey, TValue>;
            if( existingDictionary != null )
            {
                return existingDictionary.WithComparer( comparer );
            }

            return ImmutableDictionary<TKey, TValue>.Empty.WithComparer( comparer ).AddRange( source );
        }

        public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>( 
                                                                this IEnumerable<KeyValuePair<TKey, TValue>> source )
        {
            return ToImmutableDictionary( source, null );
        }
    }
}
