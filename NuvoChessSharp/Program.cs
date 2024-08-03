using NuvoChessSharp.BoardState;

namespace NuvoChessSharp;

public static class Program
{
    public static void Main()
    {
        var board = new Board();
        board.PrintFancyBoard();
    }
}