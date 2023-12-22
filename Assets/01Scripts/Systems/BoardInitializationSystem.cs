using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial struct BoardInitializationSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<Board>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var boardEntity = SystemAPI.GetSingletonEntity<Board>();      
        var ecb = 
            SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(state.WorldUnmanaged);

        var spawnAspect = SystemAPI.GetAspect<GamePieceSpawnAspect>(boardEntity);
        
        spawnAspect.SpawnInitialGamePiece(ecb);
  
        state.Enabled = false;
    }
}