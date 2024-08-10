namespace NuvoChessSharp.BoardState;

public static class Moves
{
    public const ushort Unkown = 0;
    public const ushort ToEmptySquare = 1;
    public const ushort Capture = 2;
    public const ushort Promotion = 4;
    public const ushort Castle = 8;
    public const ushort EnPassant = 16;

    // 18 for each piece type, first 8 for white, second 8 for black, zero ended
    public static readonly sbyte[] Offsets = [
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        -16, -15, -17, -32, 0, 0, 0, 0, 0, 16, 15, 17, 32, 0, 0, 0, 0, 0 // pawn
        -18, -33, -31, -14, 18, 33, 31, 14, 0, -18, -33, -31, -14, 18, 33, 31, 14, 0 // knight
        -17, -15, 15, 17, 0, 0, 0, 0, 0, -17, -15, 15, 17, 0, 0, 0, 0, 0, // bishop
        -1, -16, 1, 16, 0, 0, 0, 0, 0, -1, -16, 1, 16, 0, 0, 0, 0, 0, // rook
        -1, 16, 1, 16, -17, -15, 15, 17, 0, -1, 16, 1, 16, -17, -15, 15, 17, 0, // queen
        -1, 16, 1, 16, -17, -15, 15, 17, 0, -1, 16, 1, 16, -17, -15, 15, 17, 0, // king
    ];
}