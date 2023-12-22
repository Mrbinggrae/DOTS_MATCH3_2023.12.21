using Unity.Entities;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using Random = Unity.Mathematics.Random;

public readonly partial struct GamePieceSpawnAspect : IAspect
{
    readonly RefRO<Board> Board;
    readonly DynamicBuffer<GamePieceBuffer> GamePieceBuffers;
   
    int2 BoardSize
    {
        get => Board.ValueRO.BoardSize;
    }

    public void SpawnInitialGamePiece(EntityCommandBuffer ecb)
    {
        var random = Random.CreateFromIndex((uint)UnityEngine.Random.Range(0, int.MaxValue));
        for (int x = 0; x < BoardSize.x; x++)
        {
            for (int y = 0; y < BoardSize.y; y++)
            {
                var rnd = random.NextInt(GamePieceBuffers.Length);
                var newCoord = new int2(x, y);
                InstantiateRandomGamePiece(newCoord, rnd, ecb);
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
    }
}
