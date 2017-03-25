using System;
using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public sealed partial class ImmutableList<T>
    {
        public Builder ToBuilder()
        {
            return new Builder( this );
        }

        public sealed class Builder : IList<T>, IList, IReadOnlyList<T>
        {
            private ImmutableList<T> immutableList;
            private List<T> items;
            private bool frozen;

            internal Builder( ImmutableList<T> source )
            {
                immutableList = source;
                items = immutableList.items;
                Freeze();
            }
            
            private void Mutate()
            {
                // Mutate must be called each time the 
                // contents of "items" is about to change

                if( frozen )
                {
                    frozen = false;
                    immutableList = new ImmutableList<T>( items );
                    items = immutableList.items;
                }
            }

            private void Freeze()
            {
                frozen = true;
            }

            public ImmutableList<T> ToImmutable()
            {
                Freeze();
                return immutableList;
            }

            public T this[int index]
            {
                get { return items[index]; }
                set { Mutate(); items[index] = value; }
            }

            object IList.this[int index]
            {
                get { return items[index]; }
                set { Mutate(); items[index] = (T) value; }
            }

            public int Count { get { return items.Count; } }

            bool IList.IsFixedSize
            {
                get { return false; }
            }

            bool ICollection<T>.IsReadOnly
            {
                get { return false; }
            }

            bool IList.IsReadOnly
            {
                get { return false; }
            }

            bool ICollection.IsSynchronized
            {
                get { return ( items as ICollection ).IsSynchronized; }
            }

            object ICollection.SyncRoot
            {
                get { return ( items as ICollection ).SyncRoot; }
            }

            public void Add( T item )
            {
                Mutate();
                items.Add( item );
            }

            public void AddRange( IEnumerable<T> range )
            {
                Mutate();
                items.AddRange( range );
            }

            public void Clear()
            {
                // Mutate the list manually
                // to avoid copying of elements

                if( frozen )
                {
                    frozen = false;
                    immutableList = new ImmutableList<T>();
                    items = immutableList.items;
                }
                else
                {
                    items.Clear();
                }
            }

            public bool Contains( T item )
            {
                return items.Contains( item );
            }

            public void CopyTo( T[] array, int arrayIndex )
            {
                items.CopyTo( array, arrayIndex );
            }

            public IEnumerator<T> GetEnumerator()
            {
                return items.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return items.GetEnumerator();
            }

            public int IndexOf( T item )
            {
                return items.IndexOf( item );
            }

            public void Insert( int index, T item )
            {
                Mutate();
                items.Insert( index, item );
            }

            public bool Remove( T item )
            {
                Mutate();
                return items.Remove( item );
            }

            public void RemoveAt( int index )
            {
                Mutate();
                items.RemoveAt( index );
            }

            int IList.Add( object value )
            {
                Mutate();
                return ( items as IList ).Add( value );
            }

            bool IList.Contains( object value )
            {
                return ( items as IList ).Contains( value );
            }

            void ICollection.CopyTo( Array array, int index )
            {
                ( items as IList ).CopyTo( array, index );
            }

            int IList.IndexOf( object value )
            {
                return ( items as IList ).IndexOf( value );
            }

            void IList.Insert( int index, object value )
            {
                Mutate();
                ( items as IList ).Insert( index, value );
            }

            void IList.Remove( object value )
            {
                Mutate();
                ( items as IList ).Remove( value );
            }
        }
    }
}
