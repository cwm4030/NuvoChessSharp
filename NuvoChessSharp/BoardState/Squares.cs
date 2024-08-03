using System.Collections.Frozen;
using NuvoChessSharp.Helpers;

namespace NuvoChessSharp.BoardState;

public static class BoardState
{
    public const ushort A1 = 68;
    public const ushort B1 = 69;
    public const ushort C1 = 70;
    public const ushort D1 = 71;
    public const ushort E1 = 72;
    public const ushort F1 = 73;
    public const ushort G1 = 74;
    public const ushort H1 = 75;
    public const ushort A2 = 84;
    public const ushort B2 = 85;
    public const ushort C2 = 86;
    public const ushort D2 = 87;
    public const ushort E2 = 88;
    public const ushort F2 = 89;
    public const ushort G2 = 90;
    public const ushort H2 = 91;
    public const ushort A3 = 100;
    public const ushort B3 = 101;
    public const ushort C3 = 102;
    public const ushort D3 = 103;
    public const ushort E3 = 104;
    public const ushort F3 = 105;
    public const ushort G3 = 106;
    public const ushort H3 = 107;
    public const ushort A4 = 116;
    public const ushort B4 = 117;
    public const ushort C4 = 118;
    public const ushort D4 = 119;
    public const ushort E4 = 120;
    public const ushort F4 = 121;
    public const ushort G4 = 122;
    public const ushort H4 = 123;
    public const ushort A5 = 132;
    public const ushort B5 = 133;
    public const ushort C5 = 134;
    public const ushort D5 = 136;
    public const ushort E5 = 137;
    public const ushort F5 = 138;
    public const ushort G5 = 139;
    public const ushort H5 = 140;
    public const ushort A6 = 148;
    public const ushort B6 = 149;
    public const ushort C6 = 150;
    public const ushort D6 = 151;
    public const ushort E6 = 152;
    public const ushort F6 = 153;
    public const ushort G6 = 154;
    public const ushort H6 = 155;
    public const ushort A7 = 164;
    public const ushort B7 = 165;
    public const ushort C7 = 166;
    public const ushort D7 = 167;
    public const ushort E7 = 168;
    public const ushort F7 = 169;
    public const ushort G7 = 170;
    public const ushort H7 = 171;
    public const ushort A8 = 180;
    public const ushort B8 = 181;
    public const ushort C8 = 182;
    public const ushort D8 = 183;
    public const ushort E8 = 184;
    public const ushort F8 = 185;
    public const ushort G8 = 186;
    public const ushort H8 = 187;
    public static readonly ushort[] Squares = [
        A1, B1, C1, D1, E1, F1, G1, H1,
        A2, B2, C2, D2, E2, F2, G2, H2,
        A3, B3, C3, D3, E3, F3, G3, H3,
        A4, B4, C4, D4, E4, F4, G4, H4,
        A5, B5, C5, D5, E5, F5, G5, H5,
        A6, B6, C6, D6, E6, F6, G6, H6,
        A7, B7, C7, D7, E7, F7, G7, H7,
        A8, B8, C8, D8, E8, F8, G8, H8,
    ];
    public static readonly string[] SquareNames = [
        "a1", "b1", "c1", "d1", "e1", "f1", "g1", "h1",
        "a2", "b2", "c2", "d2", "e2", "f2", "g2", "h2",
        "a3", "b3", "c3", "d3", "e3", "f3", "g3", "h3",
        "a4", "b4", "c4", "d4", "e4", "f4", "g4", "h4",
        "a5", "b5", "c5", "d5", "e5", "f5", "g5", "h5",
        "a6", "b6", "c6", "d6", "e6", "f6", "g6", "h6",
        "a7", "b7", "c7", "d7", "e7", "f7", "g7", "h7",
        "a8", "b8", "c8", "d8", "e8", "f8", "g8", "h8",
    ];
    public static readonly FrozenSet<ushort> SquaresSet = FrozenSet.ToFrozenSet(Squares);
    public static readonly FrozenDictionary<string, ushort> NamesToSquares = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(SquareNames, Squares));
    public static readonly FrozenDictionary<ushort, string> SquaresToNames = FrozenDictionary.ToFrozenDictionary(CollectionHelper.ToKeyValuePairs(Squares, SquareNames));
}