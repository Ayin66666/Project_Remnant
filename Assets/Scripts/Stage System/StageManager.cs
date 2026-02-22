using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("---UI---")]
    [SerializeField] private CantoSelectUI[] cantoSlot;
    [SerializeField] private GameObject materialUI; // 재화던전
    [SerializeField] private CantoManager[] cantoUI; // 스토리던전


    [Header("---StageEnterUI---")] // 이거 여기에 있어야 하나?
    [SerializeField] private GameObject stageEnterUI;

    [Header("---Test---")]
    [SerializeField] private List<CantoData> cantoDataList; // 테스트용 데이터 -> 나중에 데이터베이스에서 가져올 것


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
    }

    private void Start()
    {
        SetUp(cantoDataList);
    }


    /// <summary>
    /// 칸토 & 경험치 & 끈 던전 클리어 여부 데이터 가져오기
    /// </summary>
    public void SetUp(List<CantoData> data)
    {
        // 데이터 로드 -> 지금은 없음

        // 각 칸토에 데이터 주입 -> 지금은 테스트용 데이터 주입중
        for (int i = 0; i < cantoDataList.Count; i++)
        {
            // 외부 선택 슬롯
            cantoSlot[i].SetUp(data[i], data[i].canEnter);

            // 칸토 매니저
            cantoUI[i].SetUp(data[i]);
        }
    }

    /// <summary>
    /// 경험치 & 끈 던전 UI OnOff
    /// </summary>
    /// <param name="isOn"></param>
    public void MaterialStageUI(bool isOn)
    {
        materialUI.SetActive(isOn);
    }

    /// <summary>
    /// 메인 스토리(Canto) OnOff
    /// </summary>
    /// <param name="index"></param>
    public void CantoUI(bool isOn, int index)
    {
        foreach (CantoManager ui in cantoUI)
        {
            ui.gameObject.SetActive(false);
        }

        cantoUI[index].gameObject.SetActive(isOn);
    }


    #region 스테이지 진입 로직 -> 이거 여기에 있어야하나?
    public void StageUI(StageData data)
    {
        // 데이터를 받아와서 UI 활성화
        // 설명하는 UI 관련 스크립트 따로 분할해도 될듯?
        stageEnterUI.SetActive(true);
    }

    /// <summary>
    /// 스테이지 진입
    /// </summary>
    /// <param name="data"></param>
    public void StageIn(StageData data)
    {
        SceneLoadManager.LoadScene(data.sceneName, data.stageCount.ToString());
    }
    #endregion
}


[System.Serializable]
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
    /// (N장) 칸토 순서
    /// </summary>
    public int cantoCount;
    /// <summary>
    /// 스테이지 클리어 & ex클리어 여부
    /// </summary>
    public List<StageData> stageData;
    /// <summary>
    ///스테이지 클리어에 대한 보상 리스트
    /// </summary>
    public List<RewardData> rewardData;

    [Header("---UI---")]
    /// <summary>
    /// 칸토 이름
    /// </summary>
    public string CantoName;
    /// <summary>
    /// 칸토 이미지
    /// </summary>
    public Sprite cantoSprite;
}

[System.Serializable]
public class RewardData
{
    public bool isGet;
    public int value;
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
    /// N-NN 스테이지 순서
    /// </summary>
    public int stageCount;
    /// <summary>
    /// 스테이지의 씬 이름
    /// </summary>
    public string sceneName;
    /// <summary>
    /// 출현 적 데이터 리스트
    /// </summary>
    public List<IdentityMasterSO> enemyData;

    [Header("---UI---")]
    /// <summary>
    /// 스테이지 이름
    /// </summary>
    public string stageName;
    /// <summary>
    /// 스테이지 이미지
    /// </summary>
    public Sprite stageSprite;
}


/// <summary>
/// 공용 enum / 스테이지 클리어 타입 체크 (클리어X, 일반, Ex)
/// </summary>
public enum StageClearType
{
    None,
    Clear,
    ExClear
}

