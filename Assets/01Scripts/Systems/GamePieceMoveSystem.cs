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
        
        state.RequireForUpdate(m_MoveQuery);
    }

    public void OnUpdate(ref SystemState state)
    {
        m_MoveEnableableLookup.Update(ref state);

        foreach (var (gamePiece, entity) 
            in SystemAPI.Query<RefRO<GamePiece>>().WithEntityAccess() )
        {
            m_MoveEnableableLookup.SetComponentEnabled(entity, false);
        }

        Debug.Log(m_MoveQuery.CalculateEntityCount());
    }
}
