using System;

namespace BooksLibrary;

public enum SearchPart
{
    Title,
    Author
}

public interface IBooksStorage
{
    void LoadFromXml(string filePath);
    Task LoadFromXmlAsync(string filePath);

    void SaveToXml(string filePath = null);
    Task SaveToXmlAsync(string filePath = null);

    List<Book> GetBooks(bool origin = false);
    Book CreateBook(string title, string author, int numberOfPages);
    int Count();

    void Add(Book book);
    void AddRange(IEnumerable<Book> booksToAdd);

    bool Remove(Book book);
    bool RemoveAt(int index);
    bool RemoveRange(int index, int count);

    void Sort();
    List<Book> SearchBy(string query, SearchPart searchPart);

    string GetFilePath();

}
