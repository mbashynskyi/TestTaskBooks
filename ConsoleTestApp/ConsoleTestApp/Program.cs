using BooksLibrary;
using BooksLibrary.Interfaces;

namespace ConsoleTestApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App started");

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.xml");
            
            IBooksStorage storage = BooksStorageFactory.GetBooksStorage(BooksStorageFactory.GetXmlDataParser());
            storage.Load(path);
            
            storage.AddRange(new List<Book>()
            {
                storage.CreateBook("Title_1", "Author_1", 100),
                storage.CreateBook("Title_2", "Author_1", 100),
                storage.CreateBook("Title_1", "Author_3", 10),
                storage.CreateBook("Title_1", "Author_4", 1020),
                storage.CreateBook("Title_1", "Author_2", 100)
            });

            storage.Sort();

            List<Book> searchByTitleList = storage.SearchBy("tle_1", SearchPart.Title);
            List<Book> searchByAuthorList = storage.SearchBy("AUThor_2", SearchPart.Author);

            List<Book> booksCopy = storage.GetBooks();
            List<Book> booksOrigin = storage.GetBooks(true);

            storage.Remove(booksCopy[0]);
            storage.RemoveAt(storage.Count() - 1);
            storage.RemoveRange(0, 2);

            storage.Save();

            Console.WriteLine("App completed");

            Console.ReadLine();
        }
    }
}
