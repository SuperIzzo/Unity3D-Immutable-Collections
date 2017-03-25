using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace Izzo.Collections.Immutable
{
    [DebuggerDisplay( "Count = {Count}" )]
    public sealed partial class ImmutableList<T> : IImmutableList<T>
    {
        public static readonly ImmutableList<T> Empty = new ImmutableList<T>();

        private readonly List<T> items;

        private ImmutableList()
        {
            this.items = new List<T>();
        }

        private ImmutableList( IEnumerable<T> items )
        {
            if( items is ImmutableList<T> )
            {
                var otherList = items as ImmutableList<T>;
                this.items = otherList.items;
            }
            else
            {
                this.items = new List<T>( items );
            }
        }

        private ImmutableList( List<T> items, bool noAlloc )
        {
            if( noAlloc )
            {
                this.items = items;
            }
            else
            {
                this.items = new List<T>( items );
            }
        }

        public T this[int index]
        {
            get { return items[index]; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsEmpty
        {
            get { return Count == 0; }
        }

        public ImmutableList<T> Add( T item )
        {
            var newList = new ImmutableList<T>( items );
            newList.items.Add( item );
            return newList;
        }

        public ImmutableList<T> AddRange( IEnumerable<T> range )
        {
            var newList = new ImmutableList<T>( items );
            newList.items.AddRange( range );
            return newList;
        }

        public ImmutableList<T> Set( int index, T item )
        {
            var newList = new ImmutableList<T>( items );
            newList.items[index] = item;
            return newList;
        }

        public ImmutableList<T> Clear()
        {
            return Empty;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public int IndexOf( T item, int startIndex, int count, IEqualityComparer<T> equalityComparer )
        {
            for( int i = startIndex; i < startIndex + count; i++ )
            {
                if( equalityComparer.Equals( item, items[i] ) )
                {
                    return i;
                }
            }
            return -1;
        }

        public ImmutableList<T> Insert( int index, T item )
        {
            var newList = new ImmutableList<T>( items );
            newList.items.Insert( index, item );
            return newList;
        }

        public ImmutableList<T> Remove( T item, IEqualityComparer<T> equalityComparer )
        {
            int idx = IndexOf( item, 0, Count, equalityComparer );
            if( idx >= 0 )
            {
                return RemoveAt( idx );
            }
            return this;
        }

        public ImmutableList<T> Remove( T item )
        {
            return Remove( item, EqualityComparer<T>.Default );
        }

        public ImmutableList<T> RemoveAt( int index )
        {
            var newList = new ImmutableList<T>( items );
            newList.items.RemoveAt( index );
            return newList;
        }

        public bool Contains( T item )
        {
            return items.Contains( item );
        }

        public T Find( Predicate<T> match )
        {
            return items.Find( match );
        }

        public ImmutableList<T> FindAll( Predicate<T> match )
        {
            return new ImmutableList<T>( items.FindAll( match ), true );
        }

        public int FindIndex( Predicate<T> match )
        {
            return items.FindIndex( match );
        }

        IImmutableList<T> IImmutableList<T>.Add( T item )
        {
            return Add( item );
        }

        IImmutableList<T> IImmutableList<T>.AddRange( IEnumerable<T> items )
        {
            return AddRange( items );
        }

        IImmutableList<T> IImmutableList<T>.SetItem( int index, T item )
        {
            return Set( index, item );
        }

        IImmutableList<T> IImmutableList<T>.Clear()
        {
            return Clear();
        }

        IImmutableList<T> IImmutableList<T>.Insert( int index, T item )
        {
            return Insert( index, item );
        }

        IImmutableList<T> IImmutableList<T>.RemoveAt( int index )
        {
            return RemoveAt( index );
        }

        IImmutableList<T> IImmutableList<T>.FindAll( Predicate<T> match )
        {
            return FindAll( match );
        }

        IImmutableList<T> IImmutableList<T>.Remove( T value, IEqualityComparer<T> equalityComparer )
        {
            return Remove( value, equalityComparer );
        }
    }
}
