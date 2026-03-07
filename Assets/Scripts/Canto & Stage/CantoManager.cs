using System.Collections.Generic;
using UnityEngine;


public class CantoManager : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private CantoData data;
    [SerializeField] private List<StageNode> stageNodes;
    [SerializeField] private int clearCount = 0;
    [SerializeField] private int exCount = 0;

    [Header("---Data---")]
    [SerializeField] private List<RewardData> rewardDatas;
    [SerializeField] private List<StageData> stageDatas;


    [Header("---Reward---")]
    [SerializeField] private List<RewardButton> rewardButtons;


    /// <summary>
    /// 데이터 입력 & 스테이지 노드 세팅
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(CantoData data)
    {
        this.data = data;

        // 스테이지 슬롯에 데이터 전달
        Debug.Log($"데이터 사이즈 {data.stageData.Count}");
        for (int i = 0; i < data.stageData.Count; i++)
        {
            stageNodes[i].SetUp(data.stageData[i]);
        }

        // 보상 데이터 체크
        CheckClearReward();
    }


    #region Reward
    /// <summary>
    /// 스테이지의 클리어 진행도 & EX 클리어에 따른 보상 체크
    /// </summary>
    public void CheckClearReward()
    {
        // 일단은 기능 다 만들고 만들 것
    }

    /// <summary>
    /// 클리어 & EX 클리어 보상 획득하기
    /// </summary>
    /// <param name="rewardIndex"></param>
    public void GetCantoReward(int rewardIndex)
    {
        // (0~3 일반) (4 = Ex 클리어)
        // 지정된 인덱스의 보상 전달 & 데이터 최신화
    }
    #endregion


    #region UI
    /// <summary>
    /// 스테이지 나가기 버튼 -> 운전대로 돌아감
    /// </summary>
    public void ClickExit()
    {
        gameObject.SetActive(false);
    }
    #endregion
}
