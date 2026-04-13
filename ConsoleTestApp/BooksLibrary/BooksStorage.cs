using System;
using BooksLibrary.Interfaces;

namespace BooksLibrary;

internal class StorageBase
{
    private readonly IDataParser dataParser;
    private string filePath = string.Empty;


    public StorageBase(IDataParser dataParser)
    {
        this.dataParser = dataParser;

        filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Concat(Guid.NewGuid().ToString(), dataParser.GetExtension()));
    }


    protected List<Book> GetBooksFromString(string data) => dataParser.StringToObject(data);

    protected string GetStringFromBooks(List<Book> booksList) => dataParser.ObjectToString(booksList);

    protected void SetActualFilePath(string path)
    {
        filePath = !string.IsNullOrEmpty(path) ? path : filePath;

        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, string.Empty);
        }
    }

    protected string GetActualFilePath(string path = null) => !string.IsNullOrEmpty(path) ? path : filePath;

    protected List<Book> LoadBase(string filePath)
    {
        SetActualFilePath(filePath);

        return GetBooksFromString(FileHelper.ReadFile(GetActualFilePath(filePath)));
    }

    protected async Task<List<Book>> LoadBaseAsync(string filePath)
    {
        SetActualFilePath(filePath);

        return GetBooksFromString(await FileHelper.ReadFileAsync(GetActualFilePath(filePath)));
    }

    public void SaveBase(List<Book> books, string filePath = null)
    {
        FileHelper.WriteFile(GetActualFilePath(filePath), GetStringFromBooks(books));
    }

    public async Task SaveBaseAsync(List<Book> books, string filePath = null)
    {
        await FileHelper.WriteFileAsync(GetActualFilePath(filePath), GetStringFromBooks(books));
    }

}

internal class BooksStorage : StorageBase, IBooksStorage
{
    private List<Book> books = new();


    public BooksStorage(IDataParser dataParser)
        : base(dataParser)
    {
    }


    #region Load/Save
    public void Load(string filePath)
    {
        books = LoadBase(filePath);
    }

    public async Task LoadAsync(string filePath)
    {
        books = await LoadBaseAsync(filePath);
    }

    public void Save(string filePath = null)
    {
        SaveBase(GetBooks(true), filePath);
    }

    public async Task SaveAsync(string filePath = null)
    {
        await SaveBaseAsync(GetBooks(true), filePath);
    }
    #endregion

    public string GetFilePath() => GetActualFilePath(string.Empty);

    public List<Book> GetBooks(bool origin = false) => origin ? books : books.ToList();

    public Book CreateBook(string title, string author, int numberOfPages)
    {
        return new Book()
        {
            Title = title,
            Author = author,
            NumberOfPages = numberOfPages
        };
    }

    public int Count() => books.Count;

    #region Operations
    // TODO: Avoid to add same object? Depends on usage.
    public void Add(Book book)
    {
        books.Add(book);
    }

    // TODO: Avoid to add same objects? Depends on usage.
    // Get unique from input list and in the same time that does not exists in actual list.
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

    public void Sort()
    {
        books = books.OrderBy(b => b.Author).ThenBy(b => b.Title).ToList();
    }

    public List<Book> SearchBy(string query, SearchPart searchPart)
    {
        if (!string.IsNullOrEmpty(query))
        {
            return searchPart switch
            {
                SearchPart.Title => books.Where(b => b.Title?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false).ToList(),
                SearchPart.Author => books.Where(b => b.Author?.Contains(query, StringComparison.OrdinalIgnoreCase) ?? false).ToList(),
                _ => new()
            };
        }

        return new();
    }

    #endregion

}
