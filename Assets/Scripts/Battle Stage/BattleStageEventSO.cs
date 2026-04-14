using UnityEngine;

public abstract class BattleStageEventSO : ScriptableObject
{
    [Header("---Description---")]
    [SerializeField] private string eventName;
    [SerializeField, TextArea] private string eventDescription;

    [Header("---Base Setting---")]
    [SerializeField] private string eventTitle;
    public enum TriggerTiming { WaveStart, WaveEnd, TurnReached, HpBelow }


    public abstract void Execute();
}
