using System;

namespace BooksLibrary;

internal class BookComparer : IEqualityComparer<Book>
{
    public bool Equals(Book x, Book y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (x == null || y == null)
            return false;

        return string.Equals(x.Title, y.Title) && string.Equals(x.Author, y.Author) && x.NumberOfPages == y.NumberOfPages;
    }

    public int GetHashCode(Book obj)
    {
        if (obj != null)
        {
            return HashCode.Combine(obj.Title, obj.Author, obj.NumberOfPages);
        }

        return 0;
    }
}
