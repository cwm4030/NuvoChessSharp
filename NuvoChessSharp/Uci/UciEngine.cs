using NuvoChessSharp.BoardState;

namespace NuvoChessSharp.Uci;

public class UciEngine
{
    public Board Board { get; set; } = new();

    public void Listen(string[] args)
    {
        var input = args.Length > 0 ? args : Input.GetInput();
        while (true)
        {
            if (Execute(input, 0)) break;
            input = Input.GetInput();
        }
    }

    private bool Execute(string[] input, int index)
    {
        if (index >= input.Length)
            return false;
        if (Quit(input, index))
            return true;
        if (Uci(input, index))
            return false;
        if (Isready(input, index))
            return false;
        if (Position(input, index))
            return false;
        else
            return Execute(input, index + 1);
    }

    private static bool Quit(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciInterfaceCommands.Quit)) return false;
        return true;
    }

    private static bool Uci(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciInterfaceCommands.Uci)) return false;
        Console.WriteLine(UciEngineCommands.IdName);
        Console.WriteLine(UciEngineCommands.IdAuthor);
        Console.WriteLine(UciEngineCommands.Uciok);
        return true;
    }

    private static bool Isready(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciInterfaceCommands.Isready)) return false;
        Console.WriteLine(UciEngineCommands.Readyok);
        return true;
    }

    private bool Position(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciInterfaceCommands.Position)) return false;
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
        if (!(index < input.Length && input[index] == UciInterfaceCommands.StartPosition)) return false;
        Board.SetFromFen(BoardState.Fen.StartPosition);
        return true;
    }

    private bool Print(string[] input, int index)
    {
        if (!(index < input.Length && input[index] == UciInterfaceCommands.Print)) return false;
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