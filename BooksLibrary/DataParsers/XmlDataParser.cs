using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BooksLibrary.Interfaces;

namespace BooksLibrary.DataParsers;

internal class XmlDataParser : IDataParser
{
    private const string xmlExtension = ".xml";
    private readonly XmlWriterSettings xmlSettings = new() { Indent = true, Encoding = Encoding.UTF8 };

    private XmlSerializer CreateSerializer() => new XmlSerializer(typeof(List<Book>));

    public string GetExtension() => xmlExtension;

    public List<Book> StringToObject(string data)
    {
        List<Book> result = new();

        if (string.IsNullOrEmpty(data))
            return result;

        try
        {
            XmlSerializer xml = CreateSerializer();

            using (TextReader reader = new StringReader(data))
            {
                if (xml.Deserialize(reader) is List<Book> books)
                {
                    result = books;
                }
            }
        }
        catch (Exception ex) // It was a conscious choice to use Exception and not the specific one (like XmlException)
        {
            // Add logs for futher investigation.
            // I would keep try/catch for scenario when we need to update Model (Book) and in the same time we need to save the other logic.
        }

        return result;
    }

    public string ObjectToString(List<Book> books)
    {
        books = books ?? new();

        try
        {
            XmlSerializer xml = CreateSerializer();

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream, xmlSettings))
                {
                    xml.Serialize(writer, books);

                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
        }
        catch (Exception ex) // It was a conscious choice to use Exception and not the specific one (like InvalidOperationException)
        {
            // Add logs for futher investigation.
            // I would keep try/catch for scenario when we need to update Model (Book) and in the same time want to save the other logic.
        }

        return string.Empty;
    }

}
