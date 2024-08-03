using NuvoChessSharp.BoardState;

namespace NuvoChessSharp.UniversalChessInterface;

public class Uci
{
    public Board Board { get; set; } = new();

    public void Listen(string[] args)
    {
        var input = args.Length > 0 ? args : Input.GetInput();
        while (true)
        {
            if (Execute(input)) break;
            input = Input.GetInput();
        }
    }

    private bool Execute(string[] input)
    {
        int index = 0;
        if (Quit(input, index))
            return true;
        if (Position(input, index))
            return false;
        else
            return false;
    }

    private static bool Quit(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciCommands.Quit)) return false;
        return true;
    }

    private bool Position(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciCommands.Position)) return false;
        if (StartPosition(input, index + 1))
            return true;
        else if (Print(input, index + 1))
            return true;
        else if (Fen(input, index + 1))
            return true;
        else
            return false;
    }

    private bool StartPosition(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciCommands.StartPosition)) return false;
        Board.SetFromFen(BoardState.Fen.StartPosition);
        return true;
    }

    private bool Print(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciCommands.Print)) return false;
        Board.PrintFancyBoard();
        return true;
    }

    private bool Fen(string[] input, int index)
    {
        var fen = input.Skip(index).Take(6).ToArray();
        if (!(fen.Length == 6)) return false;
        Board.SetFromFen(fen);
        return true;
    }
}