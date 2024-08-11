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

    private void SetAttackMap()
    {
        Array.Clear(_attackMap);
        var sideToMove = _turn ^ Squares.Black;
        var sideNotToMove = _turn;
        var sideNotToMoveKing = _pieceList[sideNotToMove];
        var pawnEnPassantOffset = Moves.Offsets[sideToMove == Squares.White ? 4 : 0];

        for (var i = sideToMove; _pieceList[i] != Squares.OffBoard; i++)
        {
            var startSquareIndex = _pieceList[i];
            var pieceType = _squareList[startSquareIndex].PieceType;
            var isSlider = Moves.IsSlide[pieceType];
            var offsetStart = pieceType * 8;
            var offsetEnd = offsetStart + Moves.NumOffsets[pieceType];
            var directions = Moves.Offsets[offsetStart..offsetEnd];
            if (pieceType == Pieces.Pawn)
            {
                var pawnAttackLeft = startSquareIndex + Moves.Offsets[sideToMove == Squares.White ? 1 : 5];
                var pawnAttackRight = startSquareIndex + Moves.Offsets[sideToMove == Squares.White ? 2 : 6];
                var attackLeftSquareType = _squareList[pawnAttackLeft].SquareType;
                var attackRightSquareType = _squareList[pawnAttackRight].SquareType;
                if (attackLeftSquareType == Squares.Empty || attackLeftSquareType == sideNotToMove)
                    _attackMap[pawnAttackLeft] += 1;
                if (attackRightSquareType == Squares.Empty || attackRightSquareType == sideNotToMove)
                    _attackMap[pawnAttackRight] += 1;
            }
            else
            {
                foreach (var direction in directions)
                {
                    var nonSliderSquareType = _squareList[startSquareIndex + direction].SquareType;
                    if (isSlider)
                    {
                        var currentSquareIndex = startSquareIndex + direction;
                        while (isSlider)
                        {
                            var squareType = _squareList[currentSquareIndex].SquareType;
                            if (squareType == Squares.Empty)
                                _attackMap[currentSquareIndex] += 1;
                            else if (currentSquareIndex == sideNotToMoveKing)
                            {
                                currentSquareIndex -= direction;
                                while (currentSquareIndex != startSquareIndex)
                                {
                                    _attackMap[currentSquareIndex] = (ushort)(_attackMap[currentSquareIndex] | AttackMap.PotentialPin);
                                    currentSquareIndex -= direction;
                                }
                                break;
                            }
                            else if (squareType == sideNotToMove)
                            {
                                _attackMap[currentSquareIndex] += 1;
                                var pinIndex = currentSquareIndex;
                                currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                                while (_squareList[currentSquareIndex].SquareType == Squares.Empty)
                                    currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                                if (currentSquareIndex == sideNotToMoveKing)
                                    _attackMap[currentSquareIndex] = (ushort)(_attackMap[currentSquareIndex] | AttackMap.Pin);
                                break;
                            }
                            else if (currentSquareIndex + pawnEnPassantOffset == _enPassant && squareType == sideToMove
                                && _squareList[currentSquareIndex].PieceType == Pieces.Pawn)
                            {
                                var enPassantPinIndex = currentSquareIndex;
                                currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                                while (_squareList[currentSquareIndex].SquareType == Squares.Empty)
                                    currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                                if (currentSquareIndex == sideNotToMoveKing)
                                    _attackMap[currentSquareIndex] = (ushort)(_attackMap[currentSquareIndex] | AttackMap.EnPassantPin);
                                break;
                            }
                            else
                                break;
                            currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                        }
                    }
                    else if (nonSliderSquareType == Squares.Empty || nonSliderSquareType == sideNotToMove)
                        _attackMap[nonSliderSquareType] += 1;
                }
            }
        }
    }

    private (Move[], int) GenerateMoves()
    {
        SetAttackMap();
        var moves = new Move[ListLength];
        var moveIndex = 0;
        var sideToMove = _turn;
        var sideNotToMove = _turn ^ Squares.Black;
        var checks = _attackMap[_pieceList[sideToMove]] & AttackMap.Attack;
        var startRowSet = sideToMove == Squares.White ? Squares.WhitePawnStartRowSet : Squares.BlackPawnStartRowSet;

        for (var i = sideToMove; _pieceList[i] != Squares.OffBoard; i++)
        {
            var startSquareIndex = _pieceList[i];
            var pieceType = _squareList[startSquareIndex].PieceType;
            if (pieceType != Pieces.King && checks >= 2) break;
            var isSlider = Moves.IsSlide[pieceType];
            var offsetStart = pieceType * 8;
            var offsetEnd = offsetStart + Moves.NumOffsets[pieceType];
            var directions = Moves.Offsets[offsetStart..offsetEnd];
            if (pieceType == Pieces.Pawn)
            {
                var pawnOffsetIndex = sideToMove == Squares.White ? 8 : 12;
                var pawnUpOne = startSquareIndex + Moves.Offsets[pawnOffsetIndex];
                var pawnLeftAttack = startSquareIndex + Moves.Offsets[pawnOffsetIndex + 1];
                var pawnRightAttack = startSquareIndex + Moves.Offsets[pawnOffsetIndex + 2];
                var pawnUpTwo = startSquareIndex + Moves.Offsets[pawnOffsetIndex + 3];

                if (_squareList[pawnUpOne].SquareType == Squares.Empty)
                {
                    var moveType = Squares.PromotionSquaresSet.Contains((ushort)pawnUpOne) ? Moves.Promotion : Moves.ToEmptySquare;
                    if (moveType == Moves.Promotion)
                    {
                        for (var promotionPieceType = Pieces.Knight; promotionPieceType <= Pieces.Queen; promotionPieceType++)
                        {
                            moves[moveIndex] = new Move
                            {
                                MoveType = Moves.Promotion,
                                FromSquare = startSquareIndex,
                                ToSquare = (ushort)pawnUpOne,
                                PieceType = promotionPieceType
                            };
                            moveIndex += 1;
                        }
                    }
                    else
                    {
                        moves[moveIndex] = new Move
                        {
                            MoveType = Moves.ToEmptySquare,
                            FromSquare = startSquareIndex,
                            ToSquare = (ushort)pawnUpOne,
                            PieceType = Pieces.NoPiece
                        };
                        moveIndex += 1;
                    }

                    if (_squareList[pawnUpTwo].SquareType == Squares.Empty && startRowSet.Contains(startSquareIndex))
                    {
                        moves[moveIndex] = new Move
                        {
                            MoveType = Moves.ToEmptySquare,
                            FromSquare = startSquareIndex,
                            ToSquare = (ushort)pawnUpTwo,
                            PieceType = Pieces.NoPiece,
                        };
                        moveIndex += 1;
                    }
                }

                if (_squareList[pawnLeftAttack].SquareType == sideNotToMove)
                {
                    moves[moveIndex] = new Move
                    {
                        MoveType = Moves.Capture,
                        FromSquare = startSquareIndex,
                        ToSquare = (ushort)pawnLeftAttack,
                        PieceType = Pieces.NoPiece,
                    };
                    moveIndex += 1;
                }
                else if (pawnLeftAttack == _enPassant && (_attackMap[pawnLeftAttack] & AttackMap.EnPassantPin) != AttackMap.EnPassantPin)
                {
                    moves[moveIndex] = new Move
                    {
                        MoveType = Moves.EnPassant,
                        FromSquare = startSquareIndex,
                        ToSquare = (ushort)pawnLeftAttack,
                        PieceType = Pieces.NoPiece
                    };
                    moveIndex += 1;
                }

                if (_squareList[pawnRightAttack].SquareType == sideNotToMove)
                {
                    moves[moveIndex] = new Move
                    {
                        MoveType = Moves.Capture,
                        FromSquare = startSquareIndex,
                        ToSquare = (ushort)pawnRightAttack,
                        PieceType = Pieces.NoPiece,
                    };
                    moveIndex += 1;
                }
                else if (pawnRightAttack == _enPassant && (_attackMap[pawnRightAttack] & AttackMap.EnPassantPin) != AttackMap.EnPassantPin)
                {
                    moves[moveIndex] = new Move
                    {
                        MoveType = Moves.EnPassant,
                        FromSquare = startSquareIndex,
                        ToSquare = (ushort)pawnRightAttack,
                        PieceType = Pieces.NoPiece
                    };
                    moveIndex += 1;
                }
            }
            else
            {
                foreach (var direction in directions)
                {
                    var nonSliderSquareType = _squareList[startSquareIndex + direction].SquareType;
                    if (isSlider)
                    {
                        var currentSquareIndex = startSquareIndex + direction;
                        while (isSlider)
                        {
                            var squareType = _squareList[currentSquareIndex].SquareType;
                            if (squareType == Squares.Empty)
                            {
                                if (checks == 1 && (_attackMap[currentSquareIndex] & AttackMap.PotentialPin) != AttackMap.PotentialPin) continue;
                                moves[moveIndex] = new Move
                                {
                                    MoveType = Moves.ToEmptySquare,
                                    FromSquare = startSquareIndex,
                                    ToSquare = (ushort)currentSquareIndex,
                                    PieceType = Pieces.NoPiece
                                };
                                moveIndex += 1;
                            }
                            else if (squareType == sideNotToMove)
                            {
                                moves[moveIndex] = new Move
                                {
                                    MoveType = Moves.Capture,
                                    FromSquare = startSquareIndex,
                                    ToSquare = (ushort)currentSquareIndex,
                                    PieceType = Pieces.NoPiece
                                };
                                moveIndex += 1;
                                break;
                            }
                            else
                                break;
                            currentSquareIndex = (ushort)((short)currentSquareIndex + direction);
                        }
                    }
                    else if (nonSliderSquareType == Squares.Empty)
                    {
                        moves[moveIndex] = new Move
                        {
                            MoveType = Moves.ToEmptySquare,
                            FromSquare = startSquareIndex,
                            ToSquare = (ushort)(startSquareIndex + direction),
                            PieceType = Pieces.NoPiece
                        };
                        moveIndex += 1;
                    }
                    else if (nonSliderSquareType == sideNotToMove)
                    {
                        moves[moveIndex] = new Move
                        {
                            MoveType = Moves.Capture,
                            FromSquare = startSquareIndex,
                            ToSquare = (ushort)(startSquareIndex + direction),
                            PieceType = Pieces.NoPiece
                        };
                        moveIndex += 1;
                    }
                }
            }
        }

        return (moves, moveIndex);
    }

    public void MakeMove(Move move)
    {
        _pieceList[_squareList[move.FromSquare].PieceListIndex] = move.ToSquare;
        _squareList[move.ToSquare] = _squareList[move.FromSquare];
        _squareList[move.FromSquare] = Squares.EmptySquare;
    }
}