using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleStageEventSO", menuName = "Canto/Stage/BattleStageEventSO", order = int.MaxValue)]
public class BattleStageEventSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private string eventName;
    [SerializeField] private int id;
}
