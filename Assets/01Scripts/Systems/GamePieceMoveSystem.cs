using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public partial struct GamePieceMoveSystem : ISystem
{
    ComponentLookup<MoveEnableable> m_MoveEnableableLookup;
    EntityQuery m_MoveQuery;
    

    public void OnCreate(ref SystemState state)
    {
        m_MoveEnableableLookup = SystemAPI.GetComponentLookup<MoveEnableable>();
        m_MoveQuery = new EntityQueryBuilder(Allocator.Temp)
            .WithAll<MoveEnableable>()
            .WithAll<GamePiece>()
            .Build(state.EntityManager);
        
        state.RequireForUpdate<BoardMoveStateTag>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        var boardEntity = SystemAPI.GetSingletonEntity<Board>();
        var moveSpeed = SystemAPI.GetSingleton<Board>().GamePieceMoveSpeed;
        var deltaTime = SystemAPI.Time.DeltaTime;

        m_MoveEnableableLookup.Update(ref state);

        foreach (var (move,entity) 
            in SystemAPI.Query<GamePieceMoveAspect>().WithEntityAccess().WithAll<GamePiece>() )
        {
            if (move.IsArrived())
                m_MoveEnableableLookup.SetComponentEnabled(entity, false);
            else
                move.MoveGamePiece(deltaTime, moveSpeed);
        }

        if (m_MoveQuery.CalculateEntityCount() == 0) 
            BoardStateChanger.ChangeToMatchState(ecb, boardEntity);
    }
}

