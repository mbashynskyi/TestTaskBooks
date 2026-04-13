using BooksLibrary.DataParsers;
using BooksLibrary.Interfaces;
using System;

namespace BooksLibrary;

public static class BooksStorageFactory
{
    public static IDataParser GetXmlDataParser() => new XmlDataParser();

    public static IDataParser GetJsonDataParser() => new JsonDataParser();

    public static IBooksStorage GetBooksStorage(IDataParser dataParser) => new BooksStorage(dataParser);

}
