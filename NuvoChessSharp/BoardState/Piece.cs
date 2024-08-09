namespace NuvoChessSharp.BoardState;

public struct Piece
{
    public ushort Color { get; set; }
    public ushort PieceType { get; set; }
    public ushort PieceListIndex { get; set; }
}