using System;
using System.Diagnostics;

namespace BooksLibrary;

internal static class FileHelper
{
    public static string ReadFile(string path)
    {
        string data = File.ReadAllText(path);
        
        Debug.WriteLine($"FileHelper: ReadFile(...) -> Data:\r\n{data}");

        return data;
    }

    public static async Task<string> ReadFileAsync(string path) => await File.ReadAllTextAsync(path);

    public static void WriteFile(string path, string data)
    {
        File.WriteAllText(path, data);

        Debug.WriteLine($"FileHelper: WriteFile(...) -> Data:\r\n{data}");
    }

    public static async Task WriteFileAsync(string path, string data) => await File.WriteAllTextAsync(path, data);

}
