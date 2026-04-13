using System;
using BooksLibrary.Interfaces;

namespace BooksLibrary.DataParsers;

// Just to show that there is some reason to make DataParser. 
// If we do not have any other supported formats except XML -> i would use static xml helper.
internal class JsonDataParser : IDataParser
{
    private const string jsonExtension = ".json";

    public string GetExtension() => jsonExtension;

    public List<Book> StringToObject(string data) => null;

    public string ObjectToString(List<Book> books) => null;

}
