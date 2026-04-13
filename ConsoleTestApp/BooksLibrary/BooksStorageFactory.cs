using BooksLibrary.Interfaces;
using System;

namespace BooksLibrary;

public static class BooksStorageFactory
{
    public static IBooksStorage GetBooksStorage() => new BooksStorage();
}
