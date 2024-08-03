using NuvoChessSharp.UniversalChessInterface;

namespace NuvoChessSharp;

public static class Program
{
    public static void Main()
    {
        var uci = new Uci();
        uci.Listen();
    }
}