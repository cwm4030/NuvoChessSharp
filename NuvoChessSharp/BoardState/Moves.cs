namespace NuvoChessSharp.BoardState;

public static class Moves
{
    public const ushort Unkown = 0;
    public const ushort ToEmptySquare = 1;
    public const ushort Capture = 2;
    public const ushort Promotion = 4;
    public const ushort Castle = 8;
    public const ushort EnPassant = 16;

    // whether each piece type is a sliding piece type or not
    public static readonly bool[] IsSlide = [false, false, false, true, true, true, false];

    // number of offsets for each piece type
    public static readonly ushort[] NumOffsets = [0, 4, 8, 4, 4, 8, 8];

    // 8 for each piece type, pawn - first 4 for white, next 4 for black
    public static readonly short[] Offsets = [
        0, 0, 0, 0, 0, 0, 0, 0, // no piece
        -16, -15, -17, -32, 16, 15, 17, 32, // pawn
        -18, -33, -31, -14, 18, 33, 31, 14, 0, // knight
        -17, -15, 15, 17, 0, 0, 0, 0, 0, // bishop
        -1, -16, 1, 16, 0, 0, 0, 0, 0, // rook
        -1, 16, 1, 16, -17, -15, 15, 17, 0, // queen
        -1, 16, 1, 16, -17, -15, 15, 17, 0, // king
    ];
}