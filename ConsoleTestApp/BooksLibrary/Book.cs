using System;

namespace BooksLibrary;

public class Book
{
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public uint NumberOfPages { get; set; }
}