using System.Collections.Generic;
using UnityEngine;

/*
 * 구조는 칸토 매니저 -> 스테이지 매니저 -> 스테이지 슬롯
 * 데이터 전달도 칸토 매니저 -> 스테이지 매니저 -> 스테이지 슬롯
 * 클리어 후 데이터 전달은 스테이지 슬롯 -> 칸토 매니저 -> 스테이지 매니저 -> 스테이지 슬롯
*/

public class StageManager : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private CantoData data;

    [SerializeField] private List<StageSlot> stageSlots;
    [SerializeField] private int clearCount = 0;
    [SerializeField] private int exCount = 0;


    public void SetUp(CantoData data)
    {
        this.data = data;

        // 스테이지 슬롯에 데이터 전달
        for (int i = 0; i < data.stageData.Count; i++)
        {
            stageSlots[i].SetUp(data.stageData[i]);
        }

        // 보상 데이터 체크
        CheckClearReward();
    }
    
    public void CheckClearReward()
    {
        // 일단은 기능 다 만들고 만들 것
    }
}
