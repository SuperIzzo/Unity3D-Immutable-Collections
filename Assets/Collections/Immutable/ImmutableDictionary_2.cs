using System;
using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public sealed partial class ImmutableDictionary<TKey, TValue> : IImmutableDictionary<TKey, TValue>
    {
        public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>();

        Dictionary<TKey, TValue> dictionary;

        public TValue this[TKey key]
        {
            get { return dictionary[key]; }
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        public IEnumerable<TKey> Keys
        {
            get { return dictionary.Keys; }
        }

        public IEnumerable<TValue> Values
        {
            get { return dictionary.Values; }
        }

        public IEqualityComparer<TKey> Comparer
        {
            get { return dictionary.Comparer; }
        }

        private ImmutableDictionary()
        {
            dictionary = new Dictionary<TKey, TValue>();
        }

        private ImmutableDictionary( IEqualityComparer<TKey> comparer )
        {
            dictionary = new Dictionary<TKey, TValue>( comparer );
        }

        private ImmutableDictionary( IDictionary<TKey, TValue> items,
                                     IEqualityComparer<TKey> comparer )
        {
            if( items is ImmutableDictionary<TKey, TValue> )
            {
                var otherDictionary = items as ImmutableDictionary<TKey, TValue>;
                var otherComparer = otherDictionary.Comparer;                
                if( comparer == otherComparer )
                {
                    dictionary = otherDictionary.dictionary;
                    return;
                }
                items = otherDictionary.dictionary;
            }            
            dictionary = new Dictionary<TKey, TValue>( items, comparer );
        }

        public ImmutableDictionary<TKey, TValue> WithComparer( IEqualityComparer<TKey> comparer )
        {
            if( comparer == null )
            {
                comparer = EqualityComparer<TKey>.Default;
            }

            if( dictionary.Comparer == comparer )
            {
                return this;
            }

            return new ImmutableDictionary<TKey, TValue>( dictionary, comparer );
        }

        public bool Contains( KeyValuePair<TKey, TValue> item )
        {
            TValue value;

            if( dictionary.TryGetValue( item.Key, out value ) )
            {
                return value.Equals( item.Value );
            }

            return false;
        }

        public bool ContainsKey( TKey key )
        {
            return dictionary.ContainsKey( key );
        }

        public bool TryGetValue( TKey key, out TValue value )
        {
            return dictionary.TryGetValue( key, out value );
        }

        public ImmutableDictionary<TKey, TValue> Add( TKey key, TValue value )
        {
            var newDictionary = new ImmutableDictionary<TKey, TValue>( dictionary, Comparer );
            newDictionary.dictionary.Add( key, value );
            return newDictionary;
        }

        public ImmutableDictionary<TKey, TValue> AddRange( IEnumerable<KeyValuePair<TKey, TValue>> pairs )
        {
            var newDictionary = new ImmutableDictionary<TKey, TValue>( dictionary, Comparer );
            foreach( var pair in pairs )
            {
                newDictionary.dictionary.Add( pair.Key, pair.Value );
            }
            return newDictionary;
        }

        public ImmutableDictionary<TKey, TValue> Clear()
        {
            return Empty.WithComparer( Comparer );
        }

        public ImmutableDictionary<TKey, TValue> Remove( TKey key )
        {
            if( dictionary.ContainsKey( key ) )
            {
                var newDictionary = new ImmutableDictionary<TKey, TValue>( dictionary, Comparer );
                newDictionary.dictionary.Remove( key );
                return newDictionary;
            }
            return this;
        }

        public ImmutableDictionary<TKey, TValue> SetItem( TKey key, TValue value )
        {
            var newDictionary = new ImmutableDictionary<TKey, TValue>( dictionary, Comparer );
            newDictionary.dictionary[key] = value;
            return newDictionary;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add( TKey key, TValue value )
        {
            return Add( key, value );
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add( IEnumerable<KeyValuePair<TKey, TValue>> pairs )
        {
            return AddRange( pairs );
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
        {
            return Clear();
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove( TKey key )
        {
            return Remove( key );
        }

        IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem( TKey key, TValue value )
        {
            return SetItem( key, value );
        }
    }
}
