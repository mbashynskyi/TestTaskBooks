using System;

namespace BooksLibrary;

internal static class FileHelper
{
    public static string ReadFile(string path) => File.ReadAllText(path);

    public static async Task<string> ReadFileAsync(string path) => await File.ReadAllTextAsync(path);

    public static void WriteFile(string path, string data) => File.WriteAllText(path, data);

    public static async Task WriteFileAsync(string path, string data) => await File.WriteAllTextAsync(path, data);

}
