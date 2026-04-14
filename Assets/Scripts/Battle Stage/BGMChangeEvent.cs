using UnityEngine;


[CreateAssetMenu(fileName = "BGMStageEventSO", menuName = "Canto/Stage/StageEvent/BGMChangeEventSO", order = int.MaxValue)]
public class BGMChangeEvent : BattleStageEventSO
{
    [Header("---Setting---")]
    [SerializeField] private AudioClip bgmClip;


    public override void Execute()
    {

    }
}
