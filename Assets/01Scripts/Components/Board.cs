using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Board : IComponentData
{
    public int2 BoardSize;
    public float GamePieceMoveSpeed;
}

[InternalBufferCapacity(8)]
public struct GamePieceBuffer : IBufferElementData
{
    public Entity Prefab;
    public Match MatchValue;
}

public struct BoardMatchStateTag : IComponentData { }
public struct BoardMoveStateTag : IComponentData { }
