using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="GamePiece", menuName ="ScriptableObject/GamePiece")]
public class GamePieceSO : ScriptableObject
{
    public Match match;
    public GameObject prefab;
}
