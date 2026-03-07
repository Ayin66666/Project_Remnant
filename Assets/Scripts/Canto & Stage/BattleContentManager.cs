using Game.Canto;
using Game.Stage;
using System.Collections.Generic;
using UnityEngine;


public class BattleContentManager : MonoBehaviour
{
    public static BattleContentManager instance;

    [Header("---Runtime Data---")]
    [SerializeField] private CantoDatabaseSO cantoDatabaseSO;
    [SerializeField] private List<CantoData> cantoDataList;

    [Header("---UI---")]
    [SerializeField] private CantoSelectUI[] cantoSlot;
    [SerializeField] private CantoManager[] cantoManagers;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        SetUp();
    }


    #region 시작 로직
    /// <summary>
    /// 칸토 클리어 여부 데이터 가져오기
    /// </summary>
    public void SetUp()
    {
        LoadCantoData();
    }

    /// <summary>
    /// 파일에서 칸토 & 스테이지 so 읽어오기
    /// </summary>
    private void LoadCantoData()
    {
        // - 기존의 파일에서 ReadAll 방식에서 so를 묶은 파일 1개만 로드하는 방식으로 전환함
        CantoDatabaseSO data = Resources.Load<CantoDatabaseSO>("Canto/CantoDatabase");
        if (data == null)
        {
            Debug.LogError("칸토 데이터 so 로드 실패!");
            return;
        }
        cantoDatabaseSO = data;

        // 런타임 칸토 데이터 생성
        cantoDataList = new List<CantoData>(cantoDatabaseSO.CantoData.Count);
        foreach(var canto in cantoDatabaseSO.CantoData)
        {
            CantoData cantoData = new CantoData(canto);
            cantoDataList.Add(cantoData);
        }
    }
    #endregion


    #region 세이브 & 로드 로직
    /// <summary>
    /// 신규 칸토 데이터 생성
    /// </summary>
    /// <returns></returns>
    public List<CantoData> CreateCantoData()
    {
        // 런타임 데이터 생성
        cantoDataList = new List<CantoData>(cantoDatabaseSO.CantoData.Count);
        for (int i = 0; i < cantoDatabaseSO.CantoData.Count; i++)
        {
            // 칸토 데이터 삽입
            CantoData cantoData = new CantoData(cantoDatabaseSO.CantoData[i]);

            // 런타임 데이터 리스트에 추가
            cantoDataList.Add(cantoData);
        }

        // 1장 - 1 스테이지 진입 가능하게 전환
        cantoDataList[0].canEnter = true;
        cantoDataList[0].stageData[0].canEnter = true;

        SetCantoUI();
        return cantoDataList;
    }

    /// <summary>
    /// 저장 데이터 로드 후 주입
    /// </summary>
    /// <param name="data"></param>
    public void ApplyCantoData(SaveData data)
    {
        // 런타임 데이터 생성
        cantoDataList = data.cantoData;
        SetCantoUI();
    }

    /// <summary>
    /// 세이브 용 칸토 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<CantoData> GetCantoData()
    {
        return cantoDataList;
    }
    #endregion


    #region 런타임 데이터 업데이트
    /// <summary>
    /// 칸토 데이터 업데이트
    /// </summary>
    /// <param name="cantoIndex"></param>
    public void UpdataCantoData(int index, CantoData data)
    {
        cantoDataList[index] = data;
    }
    #endregion


    #region UI
    /// <summary>
    /// 칸토 슬롯에 데이터 주입
    /// </summary>
    private void SetCantoUI()
    {
        // 슬롯 활성화
        for (int i = 0; i < cantoDataList.Count; i++)
        {
            // 데이터 & 진입가능 여부 주입
            cantoSlot[i].SetUp(cantoDataList[i], cantoDataList[i].canEnter);
            cantoManagers[i].SetUp(cantoDataList[i]);
        }
    }

    /// <summary>
    /// 메인 스토리(Canto) OnOff
    /// </summary>
    /// <param name="index"></param>
    public void CantoSelect(bool isOn, int index)
    {
        foreach (CantoManager ui in cantoManagers)
        {
            ui.gameObject.SetActive(false);
        }

        if (isOn) cantoManagers[index].gameObject.SetActive(isOn);
    }
    #endregion
}


[System.Serializable]
/// <summary>
/// 칸토 런타임 데이터
/// </summary>
public class CantoData
{
    [Header("---Data---")]
    /// <summary>
    /// 칸토 클리어 여부
    /// </summary>
    public bool isClear;
    /// <summary>
    /// 칸토 진입가능 여부
    /// </summary>
    public bool canEnter;
    /// <summary>
    /// 칸토의 정적 데이터
    /// </summary>
    public CantoMasterSO cantoData;
    /// <summary>
    /// 스테이지 클리어 & ex클리어 여부
    /// </summary>
    public List<StageData> stageData;
    /// <summary>
    ///스테이지 클리어에 대한 보상 리스트
    /// </summary>
    public List<RewardData> rewardData;



    /// <summary>
    /// 생성자 - 초기화 로직
    /// </summary>
    /// <param name="so"></param>
    public CantoData(CantoMasterSO so)
    {
        isClear = false;
        canEnter = false;
        cantoData = so;

        rewardData = so.RewardData.ConvertAll(reward => new RewardData(reward));
        stageData = so.StageData.ConvertAll(stage => new StageData(stage));
    }
}

[System.Serializable]
public class StageData
{
    [Header("---Data---")]
    /// <summary>
    /// 스테이지 클리어 타입 체크 (클리어X, 일반, Ex)
    /// </summary>
    public StageClearType stageClearType;
    /// <summary>
    /// 스테이지 입장가능 여부
    /// </summary>
    public bool canEnter;
    /// <summary>
    /// 스테이지의 정적 데이터
    /// </summary>
    public StageMasterSO stageSO;


    /// <summary>
    /// 생성자 - 초기화 로직
    /// </summary>
    /// <param name="data"></param>
    public StageData(StageMasterSO so)
    {
        stageClearType = StageClearType.None;
        canEnter = false;
        stageSO = so;
    }
}

[System.Serializable]
/// <summary>
/// 칸토 리워드 데이터
/// </summary>
public class RewardData
{
    [Header("---Data---")]
    public GetReward getReward;
    public int rewardIndex;
    public CantoRewardSO rewardSO;


    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="so"></param>
    public RewardData(CantoRewardSO so)
    {
        getReward = GetReward.Disabled;
        rewardIndex = 0;
        rewardSO = so;
    }
}


