using System.Collections.Frozen;
using NuvoChessSharp.Helpers;

namespace NuvoChessSharp.BoardState;

public static class Fen
{
    public static readonly string[] StartPosition = ["rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR", "w", "KQkq", "-", "0", "1"];
    public static readonly (ushort, ushort)[] SquareAndPieceTypes = [
        (Squares.White, Pieces.Pawn),
        (Squares.White, Pieces.Knight),
        (Squares.White, Pieces.Bishop),
        (Squares.White, Pieces.Rook),
        (Squares.White, Pieces.Queen),
        (Squares.White, Pieces.King),
        (Squares.Black, Pieces.Pawn),
        (Squares.Black, Pieces.Knight),
        (Squares.Black, Pieces.Bishop),
        (Squares.Black, Pieces.Rook),
        (Squares.Black, Pieces.Queen),
        (Squares.Black, Pieces.King),
    ];
    public static readonly char[] FenPieceTypes = [
        'P', 'N', 'B', 'R', 'Q', 'K', 'p', 'n', 'b', 'r', 'q', 'k',
    ];
    public static readonly char[] FenFancyPieceTypes = [
        '\u265F',
        '\u265E',
        '\u265D',
        '\u265C',
        '\u265B',
        '\u265A',
        '\u265F',
        '\u265E',
        '\u265D',
        '\u265C',
        '\u265B',
        '\u265A',
    ];
    public static readonly FrozenDictionary<char, (ushort, ushort)> FenToSquareAndPieceTypes = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenPieceTypes, SquareAndPieceTypes));
    public static readonly FrozenDictionary<(ushort, ushort), char> SquareAndPieceTypesToFenFancy = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(SquareAndPieceTypes, FenFancyPieceTypes));
    public static readonly ushort[] Colors = [Squares.White, Squares.Black];
    public static readonly string[] FenColors = ["w", "b"];
    public static readonly FrozenDictionary<string, ushort> FenToColors = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenColors, Colors));
    public static readonly FrozenDictionary<ushort, string> ColorsToFen = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(Colors, FenColors));
    public static readonly ushort[] CastleRights = [
        Castling.NoCastle,
        Castling.WhiteKing,
        Castling.WhiteQueen,
        Castling.BlackKing,
        Castling.BlackQueen,
        Castling.WhiteKing + Castling.WhiteQueen,
        Castling.WhiteKing + Castling.BlackKing,
        Castling.WhiteKing + Castling.BlackQueen,
        Castling.WhiteQueen + Castling.BlackKing,
        Castling.WhiteQueen + Castling.BlackQueen,
        Castling.BlackKing + Castling.BlackQueen,
        Castling.WhiteKing + Castling.WhiteQueen + Castling.BlackKing,
        Castling.WhiteKing + Castling.WhiteQueen + Castling.BlackQueen,
        Castling.WhiteKing + Castling.BlackKing + Castling.BlackQueen,
        Castling.WhiteQueen + Castling.BlackKing + Castling.BlackQueen,
        Castling.WhiteKing + Castling.WhiteQueen + Castling.BlackKing + Castling.BlackQueen,
    ];
    public static readonly string[] FenCastleRights = [
        "-",
        "K",
        "Q",
        "k",
        "q",
        "KQ",
        "Kk",
        "Kq",
        "Qk",
        "Qq",
        "kq",
        "KQk",
        "KQq",
        "Kkq",
        "Qkq",
        "KQkq"
    ];
    public static readonly FrozenDictionary<string, ushort> FenToCastleRights = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenCastleRights, CastleRights));
    public static readonly FrozenDictionary<ushort, string> CastleRightsToFen = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(CastleRights, FenCastleRights));
    public static readonly ushort[] Increments = [
        0, 1, 2, 3, 4, 5, 6, 7, 8,
    ];
    public static readonly char[] FenIncrements = [
        '/', '1', '2', '3', '4', '5', '6', '7', '8',
    ];
    public static readonly FrozenDictionary<char, ushort> FenToIncrements = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenIncrements, Increments));
}