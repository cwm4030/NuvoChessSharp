namespace NuvoChessSharp.BoardState;

public class Board
{
    public const ushort PieceListLength = 130;
    public const ushort SquareListLength = 256;
    public ushort PieceListWhiteIndex { get; set; } = Pieces.White + 1;
    public ushort PieceListBlackIndex { get; set; } = Pieces.Black + 1;
    public ushort[] PieceList { get; } = new ushort[PieceListLength];
    public Piece[] SquareList { get; } = new Piece[SquareListLength];
    public ushort Turn { get; set; } = Pieces.White;
    public ushort CastleRights { get; set; } = Castling.NoCastle;
    public ushort EnPassant { get; set; } = Pieces.OffBoard;
    public ushort HalfMove { get; set; } = 0;
    public ushort FullMove { get; set; } = 1;

    public Board() => SetFromFen(Fen.StartPosition);

    public void SetFromFen(string[] fen)
    {
        if (fen.Length != 6) return;
        var fenPieceLocations = fen[0];
        var fenColor = fen[1];
        var fenCastleRights = fen[2];
        var fenEnPassant = fen[3];
        var fenHalfMove = fen[4];
        var fenFullMove = fen[5];

        SetDefaultPieceList();
        SetDefaultSquareList();
        ushort boardIndex = 0;
        foreach (var c in fenPieceLocations)
        {
            if (boardIndex == Squares.OnBoardSquares.Length) break;

            if (Fen.FenToIncrements.TryGetValue(c, out var increment))
            {
                boardIndex += increment;
                continue;
            }

            if (Fen.FenToColorPieceTypes.TryGetValue(c, out var colorPieceType))
            {
                var pieceColor = colorPieceType.Item1;
                var pieceType = colorPieceType.Item2;
                var isWhite = pieceColor == Pieces.White;
                var isBlack = pieceColor == Pieces.Black;
                var isKing = pieceType == Pieces.King;
                var pieceListIndex = isWhite ? isKing ? Pieces.White : PieceListWhiteIndex : isBlack ? isKing ? Pieces.Black : PieceListBlackIndex : 0;
                var squaresListIndex = Squares.OnBoardSquares[boardIndex];
                if (isWhite || isBlack)
                {
                    PieceList[pieceListIndex] = squaresListIndex;
                    SquareList[squaresListIndex].Color = pieceColor;
                    SquareList[squaresListIndex].PieceType = pieceType;
                    SquareList[squaresListIndex].PieceListIndex = (ushort)pieceListIndex;
                    boardIndex += 1;
                    if (isWhite && !isKing)
                        PieceListWhiteIndex += 1;
                    else if (isBlack && !isKing)
                        PieceListBlackIndex += 1;
                }
            }
        }

        if (Fen.FenToColors.TryGetValue(fenColor, out var turnColor))
            Turn = turnColor;
        else
            Turn = Pieces.White;

        if (Fen.FenToCastleRights.TryGetValue(fenCastleRights, out var castleRights))
            CastleRights = castleRights;
        else
            CastleRights = Castling.NoCastle;

        if (Squares.NamesToSquares.TryGetValue(fenEnPassant, out var enPassant))
            EnPassant = enPassant;
        else
            EnPassant = Pieces.OffBoard;

        if (ushort.TryParse(fenHalfMove, out var halfMove))
            HalfMove = halfMove;
        else
            HalfMove = 0;

        if (ushort.TryParse(fenFullMove, out var fullMove))
            FullMove = fullMove;
        else
            FullMove = 1;
    }

    public void PrintFancyBoard()
    {
        if (!Fen.ColorsToFen.TryGetValue(Turn, out var turn)) turn = "-";
        if (!Fen.CastleRightsToFen.TryGetValue(CastleRights, out var castleRights)) castleRights = "-";
        if (!Squares.SquaresToNames.TryGetValue(EnPassant, out var enPassant)) enPassant = "-";

        Console.WriteLine($"      Turn: {turn}");
        Console.WriteLine($"      Castle Rights: {castleRights}");
        Console.WriteLine($"      En Passant: {enPassant}");

        var darkBackgroundColor = ConsoleColor.DarkBlue;
        var lightBackgroundColor = ConsoleColor.Blue;
        var rowStartColor = darkBackgroundColor;
        var currentColor = rowStartColor;
        var rowNumber = 8;
        for (ushort i = 0; i < Squares.OnBoardSquares.Length; i++)
        {
            if (i % 8 == 0)
            {
                Console.ResetColor();
                Console.Write($"\n   {rowNumber} |");
                rowNumber -= 1;
                if (rowStartColor == lightBackgroundColor)
                    rowStartColor = darkBackgroundColor;
                else
                    rowStartColor = lightBackgroundColor;
                currentColor = rowStartColor;
            }

            Console.BackgroundColor = currentColor;
            var piece = SquareList[Squares.OnBoardSquares[i]];
            if (piece.PieceListIndex == Pieces.Empty)
                Console.Write("   ");
            else
            {
                if (piece.Color == Pieces.White)
                    Console.ForegroundColor = ConsoleColor.White;
                else if (piece.Color == Pieces.Black)
                    Console.ForegroundColor = ConsoleColor.Black;
                if (!Fen.ColorPieceTypesToFenFancy.TryGetValue((piece.Color, piece.PieceType), out var fenFancy)) fenFancy = ' ';
                Console.Write($" {fenFancy} ");
            }
            Console.ResetColor();
            if (currentColor == lightBackgroundColor)
                currentColor = darkBackgroundColor;
            else
                currentColor = lightBackgroundColor;
        }
        Console.WriteLine();
        Console.WriteLine("      ------------------------");
        Console.WriteLine("       a  b  c  d  e  f  g  h ");
        Console.WriteLine();
    }

    public void MakeMove(Move move)
    {
        PieceList[SquareList[move.FromSquare].PieceListIndex] = move.ToSquare;
        SquareList[move.ToSquare] = SquareList[move.FromSquare];
        SquareList[move.FromSquare] = Pieces.EmptyPiece;
    }

    private void SetDefaultPieceList()
    {
        PieceListWhiteIndex = Pieces.White + 1;
        PieceListBlackIndex = Pieces.Black + 1;
        for (ushort i = 0; i < PieceList.Length; i++)
            PieceList[i] = Pieces.OffBoard;
    }

    private void SetDefaultSquareList()
    {
        for (ushort i = 0; i < SquareList.Length; i++)
            if (Squares.OnBoardSquaresSet.Contains(i))
                SquareList[i] = Pieces.EmptyPiece;
            else
                SquareList[i] = Pieces.OffboardPiece;
    }
}