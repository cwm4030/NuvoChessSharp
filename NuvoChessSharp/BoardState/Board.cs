namespace NuvoChessSharp.BoardState;

public class Board
{
    private const ushort ListLength = 256;
    private ushort _pieceListWhiteIndex = Squares.White + 1;
    private ushort _pieceListBlackIndex = Squares.Black + 1;
    private readonly ushort[] _pieceList = new ushort[ListLength];
    private readonly Square[] _squareList = new Square[ListLength];
    private ushort _turn = Squares.White;
    private ushort _castleRights = Castling.NoCastle;
    private ushort _enPassant = Squares.OffBoard;
    private ushort _halfMove = 0;
    private ushort _fullMove = 1;
    private readonly ushort[] _attackMap = new ushort[ListLength];
    public ushort _checkCount = 0;

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
            if (boardIndex >= Squares.OnBoardSquares.Length) break;

            if (Fen.FenToIncrements.TryGetValue(c, out var increment))
            {
                boardIndex += increment;
                continue;
            }

            if (Fen.FenToSquareAndPieceTypes.TryGetValue(c, out var colorPieceType))
            {
                var squareType = colorPieceType.Item1;
                var pieceType = colorPieceType.Item2;
                var isWhite = squareType == Squares.White;
                var isBlack = squareType == Squares.Black;
                var isKing = pieceType == Pieces.King;
                var pieceListIndex = isWhite ? isKing ? Squares.White : _pieceListWhiteIndex : isBlack ? isKing ? Squares.Black : _pieceListBlackIndex : 0;
                var squaresListIndex = Squares.OnBoardSquares[boardIndex];
                if (isWhite || isBlack)
                {
                    _pieceList[pieceListIndex] = squaresListIndex;
                    _squareList[squaresListIndex] = new Square { SquareType = squareType, PieceType = pieceType, PieceListIndex = (ushort)pieceListIndex };
                    boardIndex += 1;
                    if (isWhite && !isKing)
                        _pieceListWhiteIndex += 1;
                    else if (isBlack && !isKing)
                        _pieceListBlackIndex += 1;
                }
            }
        }

        if (Fen.FenToColors.TryGetValue(fenColor, out var turnColor))
            _turn = turnColor;
        else
            _turn = Squares.White;

        if (Fen.FenToCastleRights.TryGetValue(fenCastleRights, out var castleRights))
            _castleRights = castleRights;
        else
            _castleRights = Castling.NoCastle;

        if (Squares.NamesToSquares.TryGetValue(fenEnPassant, out var enPassant))
            _enPassant = enPassant;
        else
            _enPassant = Squares.OffBoard;

        if (ushort.TryParse(fenHalfMove, out var halfMove))
            _halfMove = halfMove;
        else
            _halfMove = 0;

        if (ushort.TryParse(fenFullMove, out var fullMove))
            _fullMove = fullMove;
        else
            _fullMove = 1;
    }

    public void PrintFancyBoard()
    {
        if (!Fen.ColorsToFen.TryGetValue(_turn, out var turn)) turn = "-";
        if (!Fen.CastleRightsToFen.TryGetValue(_castleRights, out var castleRights)) castleRights = "-";
        if (!Squares.SquaresToNames.TryGetValue(_enPassant, out var enPassant)) enPassant = "-";

        Console.WriteLine($"      Turn: {turn}");
        Console.WriteLine($"      Castle Rights: {castleRights}");
        Console.WriteLine($"      En Passant: {enPassant}");
        Console.WriteLine($"      Half Move: {_halfMove}");
        Console.WriteLine($"      Full Move: {_fullMove}");

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
            var square = _squareList[Squares.OnBoardSquares[i]];
            if (square.SquareType >= Squares.Empty)
                Console.Write("   ");
            else
            {
                if (square.SquareType == Squares.White)
                    Console.ForegroundColor = ConsoleColor.White;
                else if (square.SquareType == Squares.Black)
                    Console.ForegroundColor = ConsoleColor.Black;
                if (!Fen.SquareAndPieceTypesToFenFancy.TryGetValue((square.SquareType, square.PieceType), out var fenFancy)) fenFancy = ' ';
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
        _pieceList[_squareList[move.FromSquare].PieceListIndex] = move.ToSquare;
        _squareList[move.ToSquare] = _squareList[move.FromSquare];
        _squareList[move.FromSquare] = Squares.EmptySquare;
    }

    private Move[] GenerateMoves()
    {
        var moves = new Move[ListLength];

        return moves;
    }

    private void SetDefaultPieceList()
    {
        _pieceListWhiteIndex = Squares.White + 1;
        _pieceListBlackIndex = Squares.Black + 1;
        for (ushort i = 0; i < _pieceList.Length; i++)
            _pieceList[i] = Squares.OffBoard;
    }

    private void SetDefaultSquareList()
    {
        for (ushort i = 0; i < _squareList.Length; i++)
            if (Squares.OnBoardSquaresSet.Contains(i))
                _squareList[i] = Squares.EmptySquare;
            else
                _squareList[i] = Squares.OffBoardSquare;
    }
}