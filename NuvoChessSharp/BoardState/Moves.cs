namespace NuvoChessSharp.BoardState;

public static class Moves
{
    public const ushort Unkown = 0;
    public const ushort ToEmptySquare = 1;
    public const ushort Capture = 2;
    public const ushort Promotion = 4;
    public const ushort Castle = 8;
    public const ushort EnPassant = 16;
}