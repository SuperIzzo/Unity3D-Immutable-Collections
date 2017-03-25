using NUnit.Framework;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable.Test
{
    class CollectionAssert
    {
        internal static void Contents<T>( ImmutableList<T> list, 
                                          string listDescriptor, 
                                          params T[] items )
        {
            Assert.AreEqual( items.Length, list.Count, listDescriptor +" has wrong number of items." );
            for( int i = 0; i < list.Count; i++ )
            {
                Assert.AreEqual( items[i], list[i], listDescriptor + " has wrong " + i + " item." );
            }
        }

        internal static void Contents<T>( ImmutableList<T>.Builder list,
                                         string listDescriptor,
                                         params T[] items )
        {
            Assert.AreEqual( items.Length, list.Count, listDescriptor + " has wrong number of items." );
            for( int i = 0; i < list.Count; i++ )
            {
                Assert.AreEqual( items[i], list[i], listDescriptor + " has wrong " + i + " item." );
            }
        }

        internal static void Contents<TKey, TValue>( 
                        ImmutableDictionary<TKey, TValue> dict,
                        string dictDescriptor,
                        params KeyValuePair<TKey, TValue>[] items )
        {
            Assert.AreEqual( items.Length, dict.Count, dictDescriptor + " has wrong number of items." );
            for( int i = 0; i < dict.Count; i++ )
            {
                TKey key = items[i].Key;
                TValue value = items[i].Value;
                Assert.True( dict.ContainsKey( key ), dictDescriptor + " does not have key `" + key + "`.");
                Assert.AreEqual( value, dict[key], 
                                 dictDescriptor + " has wrong value for key `" + key + "`." );
            }
        }
    }
}
