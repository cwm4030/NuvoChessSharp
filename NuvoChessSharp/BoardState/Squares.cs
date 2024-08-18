using System.Collections.Frozen;
using NuvoChessSharp.Helpers;

namespace NuvoChessSharp.BoardState;

public static class Squares
{
    // square types
    public const ushort White = 0;
    public const ushort Black = 64;
    public const ushort Empty = 254;
    public const ushort OffBoard = 255;

    // Default Square types
    public static readonly Square OffBoardSquare = new() { SquareType = OffBoard, PieceType = Pieces.NoPiece, PieceListIndex = OffBoard };
    public static readonly Square EmptySquare = new() { SquareType = Empty, PieceType = Pieces.NoPiece, PieceListIndex = OffBoard };

    // square locations
    public const ushort A8 = 68;
    public const ushort B8 = 69;
    public const ushort C8 = 70;
    public const ushort D8 = 71;
    public const ushort E8 = 72;
    public const ushort F8 = 73;
    public const ushort G8 = 74;
    public const ushort H8 = 75;
    public const ushort A7 = 84;
    public const ushort B7 = 85;
    public const ushort C7 = 86;
    public const ushort D7 = 87;
    public const ushort E7 = 88;
    public const ushort F7 = 89;
    public const ushort G7 = 90;
    public const ushort H7 = 91;
    public const ushort A6 = 100;
    public const ushort B6 = 101;
    public const ushort C6 = 102;
    public const ushort D6 = 103;
    public const ushort E6 = 104;
    public const ushort F6 = 105;
    public const ushort G6 = 106;
    public const ushort H6 = 107;
    public const ushort A5 = 116;
    public const ushort B5 = 117;
    public const ushort C5 = 118;
    public const ushort D5 = 119;
    public const ushort E5 = 120;
    public const ushort F5 = 121;
    public const ushort G5 = 122;
    public const ushort H5 = 123;
    public const ushort A4 = 132;
    public const ushort B4 = 133;
    public const ushort C4 = 134;
    public const ushort D4 = 135;
    public const ushort E4 = 136;
    public const ushort F4 = 137;
    public const ushort G4 = 138;
    public const ushort H4 = 139;
    public const ushort A3 = 148;
    public const ushort B3 = 149;
    public const ushort C3 = 150;
    public const ushort D3 = 151;
    public const ushort E3 = 152;
    public const ushort F3 = 153;
    public const ushort G3 = 154;
    public const ushort H3 = 155;
    public const ushort A2 = 164;
    public const ushort B2 = 165;
    public const ushort C2 = 166;
    public const ushort D2 = 167;
    public const ushort E2 = 168;
    public const ushort F2 = 169;
    public const ushort G2 = 170;
    public const ushort H2 = 171;
    public const ushort A1 = 180;
    public const ushort B1 = 181;
    public const ushort C1 = 182;
    public const ushort D1 = 183;
    public const ushort E1 = 184;
    public const ushort F1 = 185;
    public const ushort G1 = 186;
    public const ushort H1 = 187;
    public static readonly ushort[] OnBoardSquares = [
        A8, B8, C8, D8, E8, F8, G8, H8,
        A7, B7, C7, D7, E7, F7, G7, H7,
        A6, B6, C6, D6, E6, F6, G6, H6,
        A5, B5, C5, D5, E5, F5, G5, H5,
        A4, B4, C4, D4, E4, F4, G4, H4,
        A3, B3, C3, D3, E3, F3, G3, H3,
        A2, B2, C2, D2, E2, F2, G2, H2,
        A1, B1, C1, D1, E1, F1, G1, H1,
    ];
    public static readonly string[] OnBoardSquareNames = [
        "a8", "b8", "c8", "d8", "e8", "f8", "g8", "h8",
        "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7",
        "a6", "b6", "c6", "d6", "e6", "f6", "g6", "h6",
        "a5", "b5", "c5", "d5", "e5", "f5", "g5", "h5",
        "a4", "b4", "c4", "d4", "e4", "f4", "g4", "h4",
        "a3", "b3", "c3", "d3", "e3", "f3", "g3", "h3",
        "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2",
        "a1", "b1", "c1", "d1", "e1", "f1", "g1", "h1",
    ];
    public static readonly FrozenSet<ushort> OnBoardSquaresSet = FrozenSet.ToFrozenSet(OnBoardSquares);
    public static readonly FrozenDictionary<string, ushort> NamesToSquares = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(OnBoardSquareNames, OnBoardSquares));
    public static readonly FrozenDictionary<ushort, string> SquaresToNames = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(OnBoardSquares, OnBoardSquareNames));

    public static readonly FrozenSet<ushort> WhitePawnStartRowSet = FrozenSet.ToFrozenSet([A2, B2, C2, D2, E2, F2, G2, H2]);
    public static readonly FrozenSet<ushort> BlackPawnStartRowSet = FrozenSet.ToFrozenSet([A7, B7, C7, D7, E7, F7, G7, H7]);
    public static readonly FrozenSet<ushort> PromotionSquaresSet = FrozenSet.ToFrozenSet([A1, B1, C1, D1, E1, F1, G1, H1, A8, B8, C8, D8, E8, F8, G8, H8]);
}