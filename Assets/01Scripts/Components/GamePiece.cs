using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


public enum Match
{
    Red,
    Blue,
    Green,
    Yellow,
    Teal,
    Orange,
    Purple,
}

public struct GamePiece : IComponentData, ICleanupComponentData
{
    public Match MatchValue;
    public int2 Coord;
}

