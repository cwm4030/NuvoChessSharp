using NuvoChessSharp.UniversalChessInterface;

namespace NuvoChessSharp;

public static class Program
{
    public static void Main(string[] args)
    {
        var uci = new Uci();
        uci.Listen(args);
    }
}