using BooksLibrary;
using BooksLibrary.Interfaces;

namespace BooksLibraryTest;

public class BooksLibraryUnitTests
{
    // add/remove - ?
    // XML serialize/deserialize
    // sort
    // search

    private static Book GetTestBook() => new Book() { Title = "Title_1", Author = "Author_1", NumberOfPages = 100 };

    [Fact]
    public void Sort_MultipleBooks_SortedByAuthorThenByTitle()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage();

        storage.Add(storage.CreateBook("Title_2", "Author_1", 100));
        storage.Add(storage.CreateBook("Title_1", "Author_2", 100));
        storage.Add(storage.CreateBook("Title_2", "Author_2", 100));
        storage.Add(storage.CreateBook("Title_1", "Author_1", 100));
        storage.Sort();

        List<Book> books = storage.GetBooks();
        Assert.Collection(books,
            b =>
            {
                Assert.Equal("Author_1", b.Author);
                Assert.Equal("Title_1", b.Title);
            },
            b =>
            {
                Assert.Equal("Author_1", b.Author);
                Assert.Equal("Title_2", b.Title);
            },
            b =>
            {
                Assert.Equal("Author_2", b.Author);
                Assert.Equal("Title_1", b.Title);
            },
            b =>
            {
                Assert.Equal("Author_2", b.Author);
                Assert.Equal("Title_2", b.Title);
            }
        );
    }

    [Fact]
    public void SearchBy_Title_ReturnFoundResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage();

        storage.AddRange(new List<Book>()
        {
            storage.CreateBook("Title_1", "Author_1", 100),
            storage.CreateBook("Title_2", "Author_1", 100),
            storage.CreateBook("Title_1", "Author_2", 100)
        });
        List<Book> result = storage.SearchBy("itle_1", SearchPart.Title);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void SearchBy_Title_ReturnEmptyResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage();

        storage.AddRange(new List<Book>()
        {
            storage.CreateBook("Title_1", "Author_1", 100),
            storage.CreateBook("Title_2", "Author_1", 100),
        });
        List<Book> result = storage.SearchBy("Title_3", SearchPart.Title);

        Assert.Empty(result);
    }

    [Fact]
    public void SearchBy_EmptyTitle_ReturnEmptyResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage();

        storage.Add(GetTestBook());
        List<Book> result = storage.SearchBy(string.Empty, SearchPart.Title);

        Assert.Empty(result);
    }

    [Fact]
    public void SearchBy_IsCaseInsensitive_ReturnFoundResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage();

        storage.Add(GetTestBook());
        List<Book> result = storage.SearchBy("ITLE", SearchPart.Title);

        Assert.Single(result);
    }

}