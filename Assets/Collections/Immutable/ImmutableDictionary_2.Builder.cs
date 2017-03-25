using System;
using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public sealed partial class ImmutableDictionary<TKey, TValue>
    {
        public Builder ToBuilder()
        {
            return new Builder( this );
        }

        public sealed class Builder : IDictionary<TKey, TValue>,
                                      IReadOnlyDictionary<TKey, TValue>,
                                      IDictionary
        {
            private ImmutableDictionary<TKey, TValue> immutableDictionary;
            private Dictionary<TKey, TValue> dictionary;
            private bool frozen;

            public Builder( ImmutableDictionary<TKey, TValue> immutableDictionary )
            {
                this.immutableDictionary = immutableDictionary;
                dictionary = immutableDictionary.dictionary;
                Freeze();
            }

            private void Mutate()
            {
                // Mutate must be called each time the 
                // contents of "items" is about to change

                if( frozen )
                {
                    frozen = false;
                    immutableDictionary = new ImmutableDictionary<TKey, TValue>( dictionary, dictionary.Comparer );
                    dictionary = immutableDictionary.dictionary;
                }
            }

            private void Freeze()
            {
                frozen = true;
            }

            public ImmutableDictionary<TKey,TValue> ToImmutable()
            {
                Freeze();
                return immutableDictionary;
            }

            public TValue this[TKey key]
            {
                get { return dictionary[key]; }
                set
                {
                    Mutate();
                    dictionary[key] = value;
                }
            }

            object IDictionary.this[object key]
            {
                get { return ( (IDictionary) dictionary )[key]; }
                set
                {
                    Mutate();
                    ( (IDictionary) dictionary )[key] = value;
                }
            }

            public int Count
            {
                get { return dictionary.Count; }
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly
            {
                get { return false; }
            }

            bool IDictionary.IsReadOnly
            {
                get { return false; }
            }

            public ICollection<TKey> Keys
            {
                get { return dictionary.Keys; }
            }

            public ICollection<TValue> Values
            {
                get { return dictionary.Values; }
            }

            bool IDictionary.IsFixedSize
            {
                get { return false; }
            }           

            ICollection IDictionary.Keys
            {
                get { return ( (IDictionary) dictionary ).Keys; }
            }

            ICollection IDictionary.Values
            {
                get { return ( (IDictionary) dictionary ).Values; }
            }

            bool ICollection.IsSynchronized
            {
                get { return ( (IDictionary) dictionary ).IsSynchronized; }
            }

            object ICollection.SyncRoot
            {
                get { return ( (IDictionary) dictionary ).SyncRoot; }
            }

            IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
            {
                get { return Keys; }
            }

            IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
            {
                get { return Values; }
            }            

            public void Add( TKey key, TValue value )
            {
                Mutate();
                dictionary.Add( key, value );
            }

            public void Clear()
            {
                // Mutate the dictionary manually
                // to avoid copying of elements

                if( frozen )
                {                    
                    frozen = false;
                    immutableDictionary = new ImmutableDictionary<TKey, TValue>( dictionary.Comparer );
                    dictionary = immutableDictionary.dictionary;
                }
                else
                {
                    dictionary.Clear();
                }
            }            

            public bool ContainsKey( TKey key )
            {
                return dictionary.ContainsKey( key );
            }           

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
            {
                return dictionary.GetEnumerator();
            }            

            public bool Remove( TKey key )
            {
                if( ContainsKey( key ) )
                {
                    Mutate();
                    return dictionary.Remove( key );
                }
                return false;
            }

            public bool TryGetValue( TKey key, out TValue value )
            {
                return dictionary.TryGetValue( key, out value );
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return dictionary.GetEnumerator();
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.Contains( KeyValuePair<TKey, TValue> item )
            {
                return ( (ICollection<KeyValuePair<TKey, TValue>>) dictionary ).Contains( item );
            }

            void ICollection<KeyValuePair<TKey, TValue>>.CopyTo( KeyValuePair<TKey, TValue>[] array, int arrayIndex )
            {
                ( (ICollection<KeyValuePair<TKey, TValue>>) dictionary ).CopyTo( array, arrayIndex );
            }

            void ICollection<KeyValuePair<TKey, TValue>>.Add( KeyValuePair<TKey, TValue> item )
            {
                Mutate();
                ( (ICollection<KeyValuePair<TKey, TValue>>) dictionary ).Add( item );
            }

            bool ICollection<KeyValuePair<TKey, TValue>>.Remove( KeyValuePair<TKey, TValue> item )
            {
                if( ContainsKey( item.Key ) )
                {
                    Mutate();
                    return ( (ICollection<KeyValuePair<TKey, TValue>>) dictionary ).Remove( item );
                }
                return false;
            }

            void IDictionary.Add( object key, object value )
            {
                Mutate();
                ( (IDictionary) dictionary ).Add( key, value );
            }

            bool IDictionary.Contains( object key )
            {
                return ( (IDictionary) dictionary ).Contains( key );
            }

            IDictionaryEnumerator IDictionary.GetEnumerator()
            {
                return ( (IDictionary) dictionary ).GetEnumerator();
            }

            void IDictionary.Remove( object key )
            {
                var iDictionary = dictionary as IDictionary;
                if( iDictionary.Contains( key ) )
                {
                    Mutate();
                    iDictionary.Remove( key );
                }
            }

            void ICollection.CopyTo( Array array, int index )
            {
                ( (ICollection) dictionary ).CopyTo( array, index );
            }            
        }
    }
}
