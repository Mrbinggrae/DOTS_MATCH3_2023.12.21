using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public readonly partial struct GamePieceSpawnAspect : IAspect
{
    readonly DynamicBuffer<GamePieceBuffer> GamePieceBuffers;

    public void SpawnInitialGamePiece(EntityCommandBuffer ecb)
    {
        var random = Random.CreateFromIndex((uint)UnityEngine.Random.Range(0, int.MaxValue));
        int2 boardSize = BoardConfig.BOARD_SIZE;
        
        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                Debug.Log("SpawnInitialGamePiece");
                var coord = new int2(x, y);
                var rnd = random.NextInt(GamePieceBuffers.Length);
                InstantiateRandomGamePiece(coord, rnd, ecb);
            }
        }
    }

    private void InstantiateRandomGamePiece(int2 coord, int rnd, EntityCommandBuffer ecb)
    {
        GamePieceBuffer gamePieceBuffer = GamePieceBuffers[rnd];
        Entity pieceEntity = ecb.Instantiate(gamePieceBuffer.Prefab);

        ecb.AddComponent(pieceEntity, new GamePiece
        {
            Coord = coord,
            MatchValue = gamePieceBuffer.MatchValue
        });

        ecb.SetComponent(pieceEntity, new LocalTransform
        {
            Position = new float3(coord.x, coord.y + BoardConfig.Y_OFFSET, 0),
            Scale = 1
        });
    }
}
