using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GamePieceAuthoring : MonoBehaviour
{
    public Match match;

    class Baker : Baker<GamePieceAuthoring>
    {
        public override void Bake(GamePieceAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);

            AddComponent(entity, 
                new GamePiece { 
                MatchValue = authoring.match
            });

            AddComponent(entity, new MoveEnableable{ });
        }
    }
}


