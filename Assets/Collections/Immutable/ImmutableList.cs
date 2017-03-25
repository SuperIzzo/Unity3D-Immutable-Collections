using System.Collections.Generic;

namespace Izzo.Collections.Immutable
{    
    public static class ImmutableList
    {
        public static ImmutableList<T> Create<T>()
        {
            return ImmutableList<T>.Empty;
        }

        public static ImmutableList<T> Create<T>( T item )
        {
            return ImmutableList<T>.Empty.Add( item );
        }

        public static ImmutableList<T> CreateRange<T>( IEnumerable<T> items )
        {
            return ImmutableList<T>.Empty.AddRange( items );
        }

        public static ImmutableList<T> Create<T>( params T[] items )
        {
            return ImmutableList<T>.Empty.AddRange( items );
        }

        public static ImmutableList<T>.Builder CreateBuilder<T>()
        {
            return ImmutableList<T>.Empty.ToBuilder();
        }

        public static int IndexOf<T>( this IImmutableList<T> list, T item )
        {
            return list.IndexOf( item, 0, list.Count, EqualityComparer<T>.Default );
        }

        public static int IndexOf<T>( this IImmutableList<T> list,
                                      T item,
                                      IEqualityComparer<T> equalityComparer )
        {
            return list.IndexOf( item, 0, list.Count, equalityComparer );
        }

        public static int IndexOf<T>( this IImmutableList<T> list,
                                      T item,
                                      int startIndex )
        {
            return list.IndexOf( item,
                                 startIndex,
                                 list.Count - startIndex,
                                 EqualityComparer<T>.Default );
        }

        public static int IndexOf<T>( this IImmutableList<T> list,
                                      T item,
                                      int startIndex,
                                      int count )
        {
            return list.IndexOf( item, startIndex, count, EqualityComparer<T>.Default );
        }

        public static IImmutableList<T> Remove<T>( this IImmutableList<T> list, T item )
        {
            return list.Remove( item, EqualityComparer<T>.Default );
        }

        public static ImmutableList<TSource> ToImmutableList<TSource>( this IEnumerable<TSource> source )
        {
            return CreateRange( source );
        }
    }
}
