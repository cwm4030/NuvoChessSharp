namespace NuvoChessSharp.BoardState;

public class Move
{
    public ushort FromSquare { get; set; } = Pieces.OffBoard;
    public ushort ToSquare { get; set; } = Pieces.OffBoard;
    public ushort PieceType { get; set; } = Pieces.Empty;
}