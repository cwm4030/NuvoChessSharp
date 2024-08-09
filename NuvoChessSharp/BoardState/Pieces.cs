namespace NuvoChessSharp.BoardState;

public static class Pieces
{
    // color types
    public const ushort OffBoard = 0;
    public const ushort Empty = 1;
    public const ushort White = 2;
    public const ushort Black = 66;

    // strictly piece types
    public const ushort Pawn = 1;
    public const ushort Knight = 2;
    public const ushort Bishop = 3;
    public const ushort Rook = 4;
    public const ushort Queen = 5;
    public const ushort King = 6;

    // Default Piece types
    public static readonly Piece OffboardPiece = new() { Color = OffBoard, PieceType = OffBoard, PieceListIndex = OffBoard };
    public static readonly Piece EmptyPiece = new() { Color = Empty, PieceType = OffBoard, PieceListIndex = OffBoard };
}