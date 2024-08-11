namespace NuvoChessSharp.BoardState;

public struct Move
{
    public ushort MoveType { get; set; }
    public ushort FromSquare { get; set; }
    public ushort ToSquare { get; set; }
    public ushort PieceType { get; set; }
}