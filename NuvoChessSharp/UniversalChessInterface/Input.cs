using System.Text;

namespace NuvoChessSharp.UniversalChessInterface;

public static class Input
{
    public static string[] GetInput()
    {
        string buffer = Console.ReadLine() ?? string.Empty;
        int index = 0;
        List<string> input = new();
        while (index < buffer.Length)
        {
            index = SkipWhiteSpace(buffer, index);
            string command;
            (command, index) = CollectNonWhiteSpace(buffer, index);
            input.Add(command);
        }
        return [.. input];
    }

    private static (string, int) CollectNonWhiteSpace(string buffer, int index)
    {
        var sb = new StringBuilder();
        while (index < buffer.Length && buffer[index] != ' ')
        {
            sb.Append(buffer[index]);
            index += 1;
        }
        return (sb.ToString(), index);
    }

    private static int SkipWhiteSpace(string buffer, int index)
    {
        while (index < buffer.Length && buffer[index] == ' ')
            index += 1;
        return index;
    }
}