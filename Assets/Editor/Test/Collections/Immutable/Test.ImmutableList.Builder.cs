using NUnit.Framework;

namespace Izzo.Collections.Immutable.Test
{
    public class TEST_ImmutableList_Builder
    {
        [Test]
        public void CreateBuilder()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Act
            ImmutableList<int>.Builder builder = list.ToBuilder();

            //Assert            
            CollectionAssert.Contents( list, "Original list", 3, 5, 1, 85 );
            CollectionAssert.Contents( builder, "Resulting builder", 3, 5, 1, 85 );
        }

        [Test]
        public void Add_1()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Act
            ImmutableList<int>.Builder builder = list.ToBuilder();
            builder.Add( 100 );

            //Assert            
            CollectionAssert.Contents( list, "Original list", 3, 5, 1, 85 );
            CollectionAssert.Contents( builder, "Resulting list", 3, 5, 1, 85, 100 );
        }

        [Test( Description = "Tests how immutability holds up with Add()" )]
        public void Add_2()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Act
            ImmutableList<int> listA = list.Add( 149 );
            ImmutableList<int>.Builder builder = list.ToBuilder();
            builder.Add( 100 );
            builder.Add( 15 );
            ImmutableList<int> listB = builder.ToImmutable();
            ImmutableList<int> listC = listB.Add( 16 );
            builder.Add( 0 );
            builder.Add( 200 );
            ImmutableList<int> listD = builder.ToImmutable();

            //Assert            
            CollectionAssert.Contents( list, "Original list", 3, 5, 1, 85 );
            CollectionAssert.Contents( listA, "Resulting list A", 3, 5, 1, 85, 149 );
            CollectionAssert.Contents( listB, "Resulting list B", 3, 5, 1, 85, 100, 15 );
            CollectionAssert.Contents( listC, "Resulting list C", 3, 5, 1, 85, 100, 15, 16 );
            CollectionAssert.Contents( listD, "Resulting list D", 3, 5, 1, 85, 100, 15, 0, 200 );
            CollectionAssert.Contents( builder, "Resulting builder", 3, 5, 1, 85, 100, 15, 0, 200 );
        }

        [Test]
        public void Clear()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Act
            ImmutableList<int>.Builder builder = list.ToBuilder();
            builder.Clear();
            ImmutableList<int> list2 = builder.ToImmutable();

            //Assert            
            CollectionAssert.Contents( list, "Original list", 3, 5, 1, 85 );
            CollectionAssert.Contents( list2, "Resulting list" );
            CollectionAssert.Contents( builder, "Resulting builder" );
        }

        [Test]
        public void Insert()
        {
            //Arrange
            var list = ImmutableList.Create( 3, 5, 1, 85 );

            //Act
            ImmutableList<int>.Builder builder = list.ToBuilder();
            builder.Insert( 1, 10 );
            builder.Insert( 3, 20 );
            ImmutableList<int> list2 = builder.ToImmutable();

            //Assert            
            CollectionAssert.Contents( list, "Original list", 3, 5, 1, 85 );
            CollectionAssert.Contents( list2, "Resulting list", 3, 10, 5, 20, 1, 85 );
            CollectionAssert.Contents( builder, "Resulting builder", 3, 10, 5, 20, 1, 85 );
        }

        [Test]
        public void Immutability()
        {
            //Arrange
            var list = ImmutableList.Create( "apple", "orange", "pear", "HAMMER" );

            //Act
            ImmutableList<string>.Builder builderA = list.ToBuilder();
            builderA.RemoveAt( 2 );
            builderA.Add( "mellon" );
            var listA = builderA.ToImmutable();
            builderA.Insert( 0, "banana" );
            builderA.Remove( "apple" );
            var listB = builderA.ToImmutable();
            ImmutableList<string>.Builder builderB = listB.ToBuilder();
            builderA.Add( "A" );
            builderA.RemoveAt( 0 );
            builderB.Add( "B" );
            builderB.RemoveAt( 1 );

            //Assert            
            CollectionAssert.Contents( list, "Original list", "apple", "orange", "pear", "HAMMER" );
            CollectionAssert.Contents( listA, "Resulting list", "apple", "orange", "HAMMER", "mellon" );
            CollectionAssert.Contents( listB, "Resulting list", "banana", "orange", "HAMMER", "mellon" );
            CollectionAssert.Contents( builderA, "Resulting builder A", "orange", "HAMMER", "mellon", "A" );
            CollectionAssert.Contents( builderB, "Resulting builder B", "banana", "HAMMER", "mellon", "B" );
        }
    }
}
