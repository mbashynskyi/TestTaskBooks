using BooksLibrary.Interfaces;
using System;

namespace BooksLibrary;

public class BooksStorageFactory
{
    public IBooksStorage GetBooksStorage() => new BooksStorage();
}
