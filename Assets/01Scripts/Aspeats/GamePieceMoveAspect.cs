using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


// board Entity
public readonly partial struct GamePieceMoveAspect : IAspect
{
    readonly RefRO<GamePiece> PieceData;
    readonly RefRW<LocalTransform> Transform;


    int2 coord
    {
        get => PieceData.ValueRO.Coord;
    }

    float3 position
    {
        get => Transform.ValueRO.Position;
        set => Transform.ValueRW.Position = value;
    }

    public void MoveGamePiece(float deltaTime, float moveSpeed)
    {
        var destination = new float3(coord.x, coord.y, 0);
        var direction = destination - position;

        position += math.normalize(direction) * deltaTime * moveSpeed;
    }

    public bool IsArrived()
    {
        var destination = new float3(coord.x, coord.y, 0);

        float distanceSquared = math.distancesq(destination, position);

        if (distanceSquared < 0.1f * 0.1f)
        {
            position = destination;
            return true;
        }

        return false;
    }

}
