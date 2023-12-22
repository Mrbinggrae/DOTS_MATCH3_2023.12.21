using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Board : IComponentData
{
    
}

[InternalBufferCapacity(8)]
public struct GamePieceBuffer : IBufferElementData
{
    public Entity Prefab;
    public Match MatchValue;
}
