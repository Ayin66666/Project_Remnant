using Game.Canto;
using Game.Stage;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class BattleContentManager : MonoBehaviour
{
    public static BattleContentManager instance;

    [Header("---Runtime Data---")]
    [SerializeField] private CantoDatabaseSO cantoDatabaseSO;
    [SerializeField] private Dictionary<int, CantoRuntimeData> cantoRuntimeData;

    [Header("---UI---")]
    [SerializeField] private CantoSelectUI[] cantoSlot;
    [SerializeField] private CantoManager[] cantoManagers;


    // 지금 생각이 꼬이는 이유
    // 기존 로더 -> 런타임 로직에서 이건 바로 런타임임
    // 기존 베이스 런타임 데이터 -> 데이터 덮어쓰기에서 이건 바로 생성 & 덮어쓰기임


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

        CreateRuntimeData();
    }


    #region 시작 로직
    /// <summary>
    /// 베이스 런타임 데이터 생성하기
    /// </summary>
    private void CreateRuntimeData()
    {
        // 런타임 칸토 데이터 생성
        cantoRuntimeData = new Dictionary<int, CantoRuntimeData>(cantoDatabaseSO.CantoData.Count);
        foreach (var canto in cantoDatabaseSO.CantoData)
        {
            CantoRuntimeData cantoData = new CantoRuntimeData(canto);
            cantoRuntimeData.Add(cantoData.cantoData.CantoId, cantoData);
        }
    }
    #endregion


    #region 세이브 & 로드 로직
    /// <summary>
    /// 신규 칸토 데이터 생성 & 세이브용 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<CantoSaveData> CreateCantoData()
    {
        // 1장 - 1 스테이지 진입 가능하게 전환
        int id = cantoDatabaseSO.CantoData[0].CantoId;
        cantoRuntimeData[id].canEnter = true;
        cantoRuntimeData[id].stageData[0].canEnter = true;

        // UI 설정
        SetCantoUI();

        // 세이브 데이터로 전환 후 반환
        return GetCantoData();
    }

    /// <summary>
    /// 저장 데이터 로드 후 주입
    /// </summary>
    /// <param name="data"></param>
    public void ApplyCantoData(SaveData data)
    {
        foreach (var save in data.cantoData)
        {
            foreach (var stage in save.stageData)
            {
                // 칸토 & 스테이지 Id 세팅
                int cantoid = save.cantoId;
                int stageid = cantoRuntimeData[cantoid].stageData.FindIndex(x => x.stageSO.Stageid == stage.stageId);

                // 세이브 데이터 주입

                cantoRuntimeData[cantoid].stageData[stageid].canEnter = stage.canEnter;
                cantoRuntimeData[cantoid].stageData[stageid].stageClearType = stage.stageClearType;
            }
        }

        SetCantoUI();
    }

    /// <summary>
    /// 세이브 용 칸토 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<CantoSaveData> GetCantoData()
    {
        // 세이브용 데이터로 전환
        List<CantoSaveData> save = new List<CantoSaveData>();
        foreach (var runtime in cantoRuntimeData.Values)
        {
            CantoSaveData saveData = new CantoSaveData
            {
                cantoId = runtime.cantoData.CantoId,
                canEnter = runtime.canEnter,
                stageData = runtime.stageData
                .Select(x => new CantoSaveData.StageSaveData
                {
                    stageId = x.stageSO.Stageid,
                    canEnter = x.canEnter,
                    stageClearType = x.stageClearType,
                })
                .ToList(),
                rewardData = runtime.rewardData.Select(re =>
                new CantoSaveData.RewardSaveData
                {
                    rewardIndex = re.rewardIndex,
                    getReward = re.getReward,
                })
                .ToList(),
            };

            save.Add(saveData);
        }

        return save;
    }
    #endregion


    #region 런타임 데이터 업데이트
    /// <summary>
    /// 칸토 데이터 업데이트
    /// </summary>
    /// <param name="cantoIndex"></param>
    public void UpdataCantoData(int index, CantoRuntimeData data)
    {
        cantoRuntimeData[index] = data;
    }
    #endregion


    #region UI
    /// <summary>
    /// 칸토 슬롯에 데이터 주입
    /// </summary>
    private void SetCantoUI()
    {
        // 슬롯 활성화 -> 딕셔너리 버전
        for (int i = 0; i < cantoDatabaseSO.CantoData.Count; i++)
        {
            // 데이터 & 진입가능 여부 주입
            CantoRuntimeData data = cantoRuntimeData[cantoDatabaseSO.CantoData[i].CantoId];
            cantoSlot[i].SetUp(data, data.canEnter);
        }

        /* 구버전
        // 슬롯 활성화 -> 딕셔너리로 바꾸면서 문제 발생 (for문 대신 id 기반 접근이 필요함!)
        for (int i = 0; i < cantoRuntimeData.Count; i++)
        {
            // 데이터 & 진입가능 여부 주입
            cantoSlot[i].SetUp(cantoRuntimeData[i], cantoRuntimeData[i].canEnter);
            cantoManagers[i].SetUp(cantoRuntimeData[i]);
        }
        */
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
public class CantoRuntimeData
{
    [Header("---Data---")]
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
    public CantoRuntimeData(CantoMasterSO so)
    {
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


