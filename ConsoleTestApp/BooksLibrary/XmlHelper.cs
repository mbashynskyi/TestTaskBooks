using System;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BooksLibrary;

internal static class XmlHelper
{
    public static T StringToObject<T>(string xmlAsString) where T : class
    {
        var xml = new XmlSerializer(typeof(T));
        using TextReader reader = new StringReader(xmlAsString);

        return xml.Deserialize(reader) as T;
    }

    public static string ObjectToString<T>(T obj) where T : class
    {
        var xml = new XmlSerializer(typeof(T));
        var result = new StringBuilder();
        var settings = new XmlWriterSettings { Indent = true };
        using (var writer = XmlWriter.Create(result, settings))
        {
            xml.Serialize(writer, obj);
        }

        return result.ToString();
    }

}
