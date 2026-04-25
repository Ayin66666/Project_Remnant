using UnityEngine;


[CreateAssetMenu(fileName = "BGMStageEventSO", menuName = "Canto/Stage/StageEvent/BGMChangeEventSO", order = int.MaxValue)]
public class BGMChangeEvent : BattleStageEventSO
{
    [Header("---Setting---")]
    [SerializeField] private AudioClip bgmClip;


    public override void Execute()
    {
        // 스테이지 매니저의 사운드 조절 함수를 호출할 것!
    }
}
