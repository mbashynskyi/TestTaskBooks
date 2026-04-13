using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksLibrary.Interfaces;

public enum SearchPart
{
    Title,
    Author
}

public interface IBooksStorage
{
    void Load(string filePath);
    Task LoadAsync(string filePath);

    void Save(string filePath = null);
    Task SaveAsync(string filePath = null);

    List<Book> GetBooks(bool origin = false);
    Book CreateBook(string title, string author, int numberOfPages);
    int Count();

    void Add(Book book);
    void AddRange(List<Book> booksToAdd);

    bool Remove(Book book);
    bool RemoveAt(int index);
    bool RemoveRange(int index, int count);

    void Sort();
    List<Book> SearchBy(string query, SearchPart searchPart);

    string GetFilePath();

}
