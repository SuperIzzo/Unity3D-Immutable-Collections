using NUnit.Framework;

namespace Izzo.Collections.Immutable.Test
{
    public class TEST_ImmutableList
    {
        [Test]
        public void Constructor()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Assert            
            CollectionAssert.Contents( list, "Resulting list", 3, 5, 1, 85 );
        }

        [Test]
        public void Add_1()
        {
            //Arrange            
            var list = ImmutableList.Create<int>();
            int item = 4;

            //Act
            ImmutableList<int> newList = list.Add( item );

            //Assert
            CollectionAssert.Contents( list, "Original list" );
            CollectionAssert.Contents( newList, "Resulting list", item );
        }

        [Test]
        public void Add_2()
        {
            //Arrange
            var list = ImmutableList.Create<float>();
            float item0 = 4.3f;
            float item1 = -9.23f;

            //Act
            ImmutableList<float> listA = list.Add( item0 );
            ImmutableList<float> listB = listA.Add( item1 );

            //Assert
            CollectionAssert.Contents( list, "Original list" );
            CollectionAssert.Contents( listA, "Resulting list A", item0 );
            CollectionAssert.Contents( listB, "Resulting list B", item0, item1 );
        }

        [Test]
        public void Clear()
        {
            //Arrange
            var list = ImmutableList.Create( 65, -2, 11, 23 );

            //Act
            ImmutableList<int> newList = list.Clear();

            //Assert
            CollectionAssert.Contents( list, "Original list", 65, -2, 11, 23 );
            CollectionAssert.Contents( newList, "Resulting list" );
        }

        [Test]
        public void Find_1()
        {
            //Arrange
            var list = ImmutableList.Create( 65, -2, 11, 23 );

            //Act
            int result1 = list.Find( ( item ) => { return item == 23; } );
            int result2 = list.Find( ( item ) => { return false; } );

            //Assert
            CollectionAssert.Contents( list, "Original list", 65, -2, 11, 23 );
            Assert.AreEqual( 23, result1, "Failed to find item with value 23." );
            Assert.AreEqual( 0, result2, "Wrong result for find fail case." );
        }

        [Test]
        public void Find_2()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act
            string result1 = list.Find( ( item ) => { return item.Contains( "ME" ); } );
            string result2 = list.Find( ( item ) => { return item.Contains( "YOU" ); } );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            Assert.AreEqual( "HAMMER", result1, "Failed to find item by valid predicate." );
            Assert.AreEqual( null, result2, "Wrong result for find fail case." );
        }

        [Test]
        public void FindIndex()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act            
            int result1 = list.FindIndex( ( item ) => item.Contains( "ME" ) );
            int result2 = list.FindIndex( ( item ) => item.Contains( "le" ) );
            int result3 = list.FindIndex( ( item ) => item == "orange" );
            int result4 = list.FindIndex( ( item ) => item.Contains( "YOU" ) );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            Assert.AreEqual( 3, result1, "Failed to find item by valid predicate." );
            Assert.AreEqual( 0, result2, "Failed to find item by valid predicate." );
            Assert.AreEqual( 1, result3, "Failed to find item by valid predicate." );
            Assert.AreEqual( -1, result4, "Wrong result for find fail case." );
        }

        [Test]
        public void Remove_1()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act
            ImmutableList<string> newList = list.Remove( "orange" );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            CollectionAssert.Contents( newList, "Resulting list", "apple", "pear", "HAMMER" );
        }

        [Test]
        public void Remove_2()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act
            ImmutableList<string> newList = list.Remove( "banana" );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            CollectionAssert.Contents( newList, "Resulting list", "apple", "orange", "pear", "HAMMER" );
        }

        [Test]
        public void RemoveAt()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act
            ImmutableList<string> newList = list.RemoveAt( 2 );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            CollectionAssert.Contents( newList, "Resulting list", "apple", "orange", "HAMMER" );
        }

        [Test]
        public void Set()
        {
            //Arrange
            var list = ImmutableList.Create( 15, 23, 65, 11, 13, 43 );

            //Act
            ImmutableList<int> newList = list.Set( 4, -3 );

            //Assert
            CollectionAssert.Contents( list, "Original list", 15, 23, 65, 11, 13, 43 );
            CollectionAssert.Contents( newList, "Resulting list", 15, 23, 65, 11, -3, 43 );
        }

        [Test]
        public void Insert()
        {
            //Arrange
            var list = ImmutableList.Create( 15, 23, 65, 13, 43 );

            //Act
            ImmutableList<int> newList = list.Insert( 3, 11 );

            //Assert
            CollectionAssert.Contents( list, "Original list", 15, 23, 65, 13, 43 );
            CollectionAssert.Contents( newList, "Resulting list", 15, 23, 65, 11, 13, 43 );
        }

        [Test]
        public void Contains()
        {
            //Arrange
            var list = ImmutableList.Create( 15, 23, 65, 13, 43 );

            //Act
            bool result1 = list.Contains( 65 );
            bool result2 = list.Contains( 101 );

            //Assert
            CollectionAssert.Contents( list, "Original list", 15, 23, 65, 13, 43 );
            Assert.True( result1, "Failed to detect list contains item '65'" );
            Assert.False( result2, "Failed to detect list does not contain item '101'" );
        }

        [Test]
        public void IndexOf()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act            
            int result1 = list.IndexOf( "HAMMER" );
            int result2 = list.IndexOf( "apple", 0, 2 );
            int result3 = list.IndexOf( "pear" );
            int result4 = list.IndexOf( "apple", 1, 2 );
            int result5 = list.IndexOf( "Apple" );

            //Assert
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            Assert.AreEqual( 3, result1, "Failed to find item by valid predicate." );
            Assert.AreEqual( 0, result2, "Failed to find item by valid predicate." );
            Assert.AreEqual( 2, result3, "Failed to find item by valid predicate." );
            Assert.AreEqual( -1, result4, "Wrong result for find fail case." );
            Assert.AreEqual( -1, result5, "Wrong result for find fail case." );
        }
    }
}