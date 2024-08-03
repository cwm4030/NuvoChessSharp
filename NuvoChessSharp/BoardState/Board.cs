namespace NuvoChessSharp.BoardState;

public class Board
{
    public const ushort PieceListLength = 130;
    public const ushort SquareListLength = 256;
    public Piece[] PieceList { get; } = new Piece[PieceListLength];
    public ushort[] SquareList { get; } = new ushort[SquareListLength];
    public ushort Turn { get; } = Pieces.White;
    public ushort CastleRights { get; }
    public ushort EnPassant { get; }
    public ushort HalfMove { get; } = 0;
    public ushort FullMove { get; } = 1;
}