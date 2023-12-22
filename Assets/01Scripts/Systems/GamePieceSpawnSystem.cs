using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;


public partial struct GamePieceSpawnSystem : ISystem
{
    private Random random;

    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Board>();
        random = Random.CreateFromIndex((uint)12354);
    }

    public void OnUpdate(ref SystemState state)
    {
        //var boardEntity = SystemAPI.GetSingletonEntity<Board>();

        //SystemAPI.GetAspect<GamePieceSpawnAspect>(boardEntity)
        //    .SpawnInitialGamePieces(random);
    }
}
