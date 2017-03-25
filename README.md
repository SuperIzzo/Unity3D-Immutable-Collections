# Unity3D Immutable Collections
A poor man's C# immutable collections in Unity.

This is an (incomplete) implementation of the standard immutable collections 
library using the existing collections found in .NET 2.0. Some of the API is
changed to fit in the old framework. For instance ImmutableDictionarie has
only a (key) compararer as dictionaries do not support value compararers.

Some code borrowed from .NET Core foundational libraries:
https://github.com/dotnet/corefx/blob/master/src/System.Collections.Immutable

#### Notes on performance:
Currently changing an immutable collection naively creates a complete copy
of the original collection, none of the internal structure is reused, 
which means each mutating operation is at least O(n).
