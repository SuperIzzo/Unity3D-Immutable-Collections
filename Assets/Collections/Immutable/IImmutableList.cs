using System;
using System.Collections;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{
    public interface IImmutableList<T> : IReadOnlyList<T>, 
                                         IReadOnlyCollection<T>,
                                         IEnumerable<T>, 
                                         IEnumerable
    {
        IImmutableList<T> Add( T value );
        IImmutableList<T> AddRange( IEnumerable<T> items );
        IImmutableList<T> SetItem( int index, T value );
        IImmutableList<T> Remove( T value, IEqualityComparer<T> equalityComparer );
        IImmutableList<T> Clear();
        IImmutableList<T> Insert( int index, T item );
        IImmutableList<T> RemoveAt( int index );
        T Find( Predicate<T> match );
        int FindIndex( Predicate<T> match);
        IImmutableList<T> FindAll( Predicate<T> match );
        int IndexOf( T item, int index, int count, IEqualityComparer<T> equalityComparer );
    }
}
