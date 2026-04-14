using System;

namespace BooksLibrary.Interfaces;

public interface IDataParser
{
    string GetExtension();
    List<Book> StringToObject(string data);
    string ObjectToString(List<Book> books);

}
