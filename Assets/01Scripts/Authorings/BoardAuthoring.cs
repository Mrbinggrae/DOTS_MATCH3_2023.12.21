using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class BoardConfig
{
    public readonly static float Y_OFFSET = 3;
}


public class BoardAuthoring : MonoBehaviour
{
    public int2 boardSize;
    public float gamePieceMoveSpeed;

    public List<GamePieceSO> gamePieceSOs;

    class Baker : Baker<BoardAuthoring>
    {
        public override void Bake(BoardAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddBuffer<GamePieceBuffer>(entity);
  
            foreach (var pieceObject in authoring.gamePieceSOs)
            {
                AppendToBuffer(entity, new GamePieceBuffer
                {
                    Prefab = GetEntity(pieceObject.prefab, TransformUsageFlags.None),
                    MatchValue = pieceObject.match
                });
            }

            AddComponent(entity, new Board
            {
                BoardSize = authoring.boardSize,
                GamePieceMoveSpeed = authoring.gamePieceMoveSpeed,
            });
        }
    }
}

