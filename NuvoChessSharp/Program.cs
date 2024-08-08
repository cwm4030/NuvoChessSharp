using NuvoChessSharp.Uci;

namespace NuvoChessSharp;

public static class Program
{
    public static void Main(string[] args)
    {
        var uciEngine = new UciEngine();
        uciEngine.Listen(args);
    }
}