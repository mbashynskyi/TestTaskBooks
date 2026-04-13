using System;

namespace BooksLibrary;

public interface IBooksStorage
{
    void LoadFromXml(string filePath);
    Task LoadFromXmlAsync(string filePath);

    void SaveToXml(string filePath = null);
    Task SaveToXmlAsync(string filePath = null);

    List<Book> GetBooks(bool origin = false);
    Book CreateBook(string title, string author, uint numberOfPages);

    void Add(Book book);
    void AddRange(IEnumerable<Book> booksToAdd);

    bool Remove(Book book);
    bool RemoveAt(int index);
    bool RemoveRange(int index, int count);

    string GetFilePath();

}
