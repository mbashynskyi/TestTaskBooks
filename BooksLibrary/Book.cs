using System;

namespace BooksLibrary;

public class Book
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int NumberOfPages { get; set; }
}