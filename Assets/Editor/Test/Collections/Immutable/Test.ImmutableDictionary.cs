using NUnit.Framework;
using System.Collections.Generic;

namespace Izzo.Collections.Immutable.Test
{
    using System;
    using ssKV = KeyValuePair<string, string>;

    public class TEST_ImmutableDictionary
    {
        ssKV appleRed = new ssKV( "apple", "red" );
        ssKV appleGreen = new ssKV( "apple", "green" );
        ssKV bananaYellow = new ssKV( "banana", "yellow" );
        ssKV orangeOrange = new ssKV( "orange", "orange" );

        [Test]
        public void Add1()
        {
            //Arrange
            var dict = ImmutableDictionary.Create<string, string>();

            //Act
            var dictA = dict.Add( appleRed.Key, appleRed.Value );
            var dictB = dictA.Add( bananaYellow.Key, bananaYellow.Value );
            var dictC = dictB.Add( orangeOrange.Key, orangeOrange.Value );

            //Assert
            CollectionAssert.Contents( dict, "Original dictionary" );
            CollectionAssert.Contents( dictA, "Result dictionary A", appleRed );
            CollectionAssert.Contents( dictB, "Result dictionary B", appleRed, bananaYellow );
            CollectionAssert.Contents( dictC, "Result dictionary C", appleRed, bananaYellow, orangeOrange );
        }

        [Test( Description = "Adding the same item twice throws ArgumentException" )]
        public void Add2()
        {
            //Arrange
            var dict = ImmutableDictionary.CreateRange( new ssKV[] { appleRed } );

            //Assert
            Assert.Catch<ArgumentException>( () =>
            {
                dict.Add( appleRed.Key, appleRed.Value );
            } );
        }

        [Test]
        public void SetItem()
        {
            //Arrange
            var dict = ImmutableDictionary.Create<string, string>();

            //Act
            var dictA = dict.SetItem( appleRed.Key, appleRed.Value );
            var dictB = dictA.SetItem( bananaYellow.Key, bananaYellow.Value );
            var dictC = dictB.SetItem( orangeOrange.Key, orangeOrange.Value );
            var dictD = dictB.SetItem( appleGreen.Key, appleGreen.Value );

            //Assert
            CollectionAssert.Contents( dict, "Original dictionary" );
            CollectionAssert.Contents( dictA, "Result dictionary A", appleRed );
            CollectionAssert.Contents( dictB, "Result dictionary B", appleRed, bananaYellow );
            CollectionAssert.Contents( dictC, "Result dictionary C", appleRed, bananaYellow, orangeOrange );
            CollectionAssert.Contents( dictD, "Result dictionary D", appleGreen, bananaYellow );
        }

        [Test]
        public void Remove()
        {
            //Arrange
            var dict = CreateDictionary( appleRed, bananaYellow, orangeOrange );

            //Act
            var dictA = dict.Remove( bananaYellow.Key );
            var dictB = dict.Remove( orangeOrange.Key );
            var dictC = dictA.Remove( appleRed.Key );
            var dictD = dictB.Remove( orangeOrange.Key );

            //Assert
            CollectionAssert.Contents( dict, "Original dictionary", appleRed, bananaYellow, orangeOrange );
            CollectionAssert.Contents( dictA, "Result dictionary A", appleRed, orangeOrange );
            CollectionAssert.Contents( dictB, "Result dictionary B", appleRed, bananaYellow );
            CollectionAssert.Contents( dictC, "Result dictionary C", orangeOrange );
            CollectionAssert.Contents( dictD, "Result dictionary D", appleRed, bananaYellow );
        }

        [Test]
        public void Clear()
        {
            //Arrange
            var dict = CreateDictionary( appleRed, bananaYellow, orangeOrange );

            //Act
            var dictA = dict.Clear();
            var dictB = dictA.Add( appleGreen.Key, appleGreen.Value );
            var Empty = ImmutableDictionary<string, string>.Empty;

            //Assert
            CollectionAssert.Contents( dict, "Original dictionary", appleRed, bananaYellow, orangeOrange );
            CollectionAssert.Contents( dictA, "Result dictionary A" );
            CollectionAssert.Contents( dictB, "Result dictionary B", appleGreen );
            CollectionAssert.Contents( Empty, "Empty dictionary" );
        }        

        private ImmutableDictionary<TKey, TValue> CreateDictionary<TKey, TValue>( params KeyValuePair<TKey, TValue>[] pairs )
        {
            return ImmutableDictionary.CreateRange( pairs );
        }
    }
}