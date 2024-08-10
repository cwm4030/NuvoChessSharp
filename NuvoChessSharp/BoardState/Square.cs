namespace NuvoChessSharp.BoardState;

public struct Square
{
    public ushort SquareType { get; set; }
    public ushort PieceType { get; set; }
    public ushort PieceListIndex { get; set; }
}