using System.Collections.Frozen;
using NuvoChessSharp.Helpers;

namespace NuvoChessSharp.BoardState;

public static class Fen
{
    public static readonly (ushort, ushort)[] ColorPieceTypes = [
        (Pieces.White, Pieces.Pawn),
        (Pieces.White, Pieces.Knight),
        (Pieces.White, Pieces.Bishop),
        (Pieces.White, Pieces.Rook),
        (Pieces.White, Pieces.Queen),
        (Pieces.White, Pieces.King),
        (Pieces.Black, Pieces.Pawn),
        (Pieces.Black, Pieces.Knight),
        (Pieces.Black, Pieces.Bishop),
        (Pieces.Black, Pieces.Rook),
        (Pieces.Black, Pieces.Queen),
        (Pieces.Black, Pieces.King),
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
    public static readonly FrozenSet<char> FenPieceTypesSet = FrozenSet.ToFrozenSet(FenPieceTypes);
    public static readonly FrozenDictionary<char, (ushort, ushort)> FenToColorPieceTypes = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenPieceTypes, ColorPieceTypes));
    public static readonly FrozenDictionary<(ushort, ushort), char> ColorPieceTypesToFenFancy = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(ColorPieceTypes, FenFancyPieceTypes));
    public static readonly ushort[] Colors = [Pieces.White, Pieces.Black];
    public static readonly char[] FenColors = ['w', 'b'];
    public static readonly FrozenSet<char> FenColorsSet = FrozenSet.ToFrozenSet(FenColors);
    public static readonly FrozenDictionary<char, ushort> FenToColors = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenColors, Colors));
    public static readonly FrozenDictionary<ushort, char> ColorsToFen = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(Colors, FenColors));
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
    public static readonly FrozenSet<string> FenCastleRightsSet = FrozenSet.ToFrozenSet(FenCastleRights);
    public static readonly FrozenDictionary<string, ushort> FenToCastleRights = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(FenCastleRights, CastleRights));
    public static readonly FrozenDictionary<ushort, string> CastleRightsToFen = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(CastleRights, FenCastleRights));
}