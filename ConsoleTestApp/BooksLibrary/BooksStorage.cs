using System;
using System.Linq;
using System.Text;

namespace BooksLibrary;

internal class StorageBase
{
    private string filePath = string.Empty;


    public StorageBase()
    {
        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(Guid.NewGuid().ToString(), ".xml"));
    }


    protected void SetActualFilePath(string path)
    {
        filePath = !string.IsNullOrEmpty(path) ? path : filePath;
    }

    protected string GetActualFilePath(string path = null) => !string.IsNullOrEmpty(path) ? path : filePath;
}

internal class BooksStorage : StorageBase, IBooksStorage
{
    private List<Book> books = new();


    public BooksStorage()
    {
    }


    private void GetBooksFromString(string xmlData)
    {
        books = XmlHelper.StringToObject<List<Book>>(xmlData);
    }

    private string GetStringFromBooks(List<Book> booksList)
    {
        return XmlHelper.ObjectToString<List<Book>>(booksList);
    }

    #region Load
    public void LoadFromXml(string filePath)
    {
        SetActualFilePath(filePath);
        GetBooksFromString(FileHelper.ReadFile(GetActualFilePath(filePath)));
    }

    public async Task LoadFromXmlAsync(string filePath)
    {
        SetActualFilePath(filePath);
        GetBooksFromString(await FileHelper.ReadFileAsync(GetActualFilePath(filePath)));
    }
    #endregion

    #region Save
    public void SaveToXml(string filePath = null)
    {
        FileHelper.WriteFile(GetActualFilePath(filePath), GetStringFromBooks(GetBooks()));
    }

    public async Task SaveToXmlAsync(string filePath = null)
    {
        await FileHelper.WriteFileAsync(GetActualFilePath(filePath), GetStringFromBooks(GetBooks()));
    }
    #endregion

    public string GetFilePath() => GetActualFilePath(string.Empty);

    public List<Book> GetBooks(bool origin = false) => origin ? books : books.ToList();

    public Book CreateBook(string title, string author, uint numberOfPages)
    {
        return new Book()
        {
            Title = title,
            Author = author,
            NumberOfPages = numberOfPages
        };
    }

    #region Operations
    public void Add(Book book)
    {
        books.Add(book);
    }

    public void AddRange(IEnumerable<Book> booksToAdd)
    {
        books.AddRange(booksToAdd);
    }

    public bool Remove(Book book)
    {
        return books.Remove(book);
    }

    public bool RemoveAt(int index)
    {
        try
        {
            books.RemoveAt(index);

            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    public bool RemoveRange(int index, int count)
    {
        try
        {
            books.RemoveRange(index, count);

            return true;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }
    #endregion

}
