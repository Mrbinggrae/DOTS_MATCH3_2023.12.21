using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BoardStateChanger
{
    public static void ChangeToMoveState(EntityCommandBuffer ecb, Entity boardEntity)
    {
        ecb.AddComponent<BoardMoveStateTag>(boardEntity);
    }

    public static void ChangeToMatchState(EntityCommandBuffer ecb, Entity boardEntity)
    {
        ecb.RemoveComponent<BoardMoveStateTag>(boardEntity);
        ecb.AddComponent<BoardMatchStateTag>(boardEntity);
    }
}
