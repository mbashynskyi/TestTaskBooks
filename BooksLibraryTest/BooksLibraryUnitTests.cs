using BooksLibrary;
using BooksLibrary.Interfaces;

namespace BooksLibraryTest;

public class BooksLibraryUnitTests
{
    // add/remove - ?
    // sort
    // search
    // XML serialize/deserialize

    private static Book CreateTestBook() => new Book() { Title = "Title_1", Author = "Author_1", NumberOfPages = 100 };

    [Fact]
    public void Sort_MultipleBooks_SortedByAuthorThenByTitle()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());

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
    public void SearchBy_Title_FoundResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());

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
    public void SearchBy_Title_EmptyResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());

        storage.AddRange(new List<Book>()
        {
            storage.CreateBook("Title_1", "Author_1", 100),
            storage.CreateBook("Title_2", "Author_1", 100),
        });
        List<Book> result = storage.SearchBy("Title_3", SearchPart.Title);

        Assert.Empty(result);
    }

    [Fact]
    public void SearchBy_EmptyTitle_EmptyResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());

        storage.Add(CreateTestBook());
        List<Book> result = storage.SearchBy(string.Empty, SearchPart.Title);

        Assert.Empty(result);
    }

    [Fact]
    public void SearchBy_IsCaseInsensitive_FoundResult()
    {
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());

        storage.Add(CreateTestBook());
        List<Book> result = storage.SearchBy("ITLE", SearchPart.Title);

        Assert.Single(result);
    }

    [Fact]
    public void ParserDeserialize_ValidXml_FilledBooksList()
    {
        string xml = @"<ArrayOfBook>
                        <Book>
                            <Title>Title_1</Title>
                            <Author>Author_1</Author>
                            <NumberOfPages>100</NumberOfPages>
                        </Book>
                        <Book>
                            <Title>Title_2</Title>
                            <Author>Author_1</Author>
                            <NumberOfPages>100</NumberOfPages>
                        </Book>
                    </ArrayOfBook>";

        IDataParser dataParser = BooksStorageFactory.GetXmlDataParser();
        List<Book> books = dataParser.StringToObject(xml);
        
        Assert.Equal(2, books.Count);
    }

    [Fact]
    public void ParserDeserialize_InvalidXml_EmptyBooksList()
    {
        string xml = @"<ArrayOfB
                        Book>
                            <Title>Title_1</Title>
                            <Author>Author_1</Author>
                            <NumberOfPages>100</NumberOfPages>
                        </Book>
                    </ArrayOfBook";

        IDataParser dataParser = BooksStorageFactory.GetXmlDataParser();
        List<Book> books = dataParser.StringToObject(xml);

        Assert.Empty(books);
    }

    [Fact]
    public void ParserSerialize_Books_CreateValidXml()
    {
        IDataParser dataParser = BooksStorageFactory.GetXmlDataParser();
        IBooksStorage storage = BooksStorageFactory.GetBooksStorage(dataParser);

        storage.Add(CreateTestBook());
        List<Book> result = storage.GetBooks();
        string xml = dataParser.ObjectToString(result);

        Assert.Contains("<Title>Title_1</Title>", xml);
        Assert.Contains("<Author>Author_1</Author>", xml);
        Assert.Contains("<NumberOfPages>100</NumberOfPages>", xml);
    }

}