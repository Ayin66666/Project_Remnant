using System.Collections.Generic;
using UnityEngine;


public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [Header("---UI---")]
    [SerializeField] private CantoContainerUI[] cantoSlot;
    [SerializeField] private GameObject materialUI;
    [SerializeField] private GameObject[] cantoUI;


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

    /// <summary>
    /// 칸토 & 경험치 & 끈 던전 클리어 여부 데이터 가져오기
    /// </summary>
    public void SetUp(List<CantoData> data)
    {
        // 클리어데이터 체크 -> 지금은 무조건 false
        if(false)
        {

        }
        else
        {
            for(int i = 0; i < data.Count; i++)
            {
                cantoSlot[i].SetUp(data[i], data[i].isClear);
            }
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
        foreach (GameObject ui in cantoUI)
        {
            ui.SetActive(false);
        }

        cantoUI[index].SetActive(isOn);
    }
}

/*
 * 칸토 데이터 구현했고, 실 기능과 연계 필요함
 * 기존 컨테이너에 바로 넣었던 UI 전부 제거하고 데이터에 UI 이동할 것!
 * 데이터 관리는 CantoDataBase 스크립트 구현하는게 맞을듯?
*/

[System.Serializable]
public class CantoData
{
    [Header("---Data---")]
    /// <summary>
    /// 칸토 클리어 여부
    /// </summary>
    public bool isClear;
    /// <summary>
    /// (N장) 칸토 순서
    /// </summary>
    public int CantoCount;
    /// <summary>
    /// 스테이지 클리어 & ex클리어 여부
    /// </summary>
    public List<StageData> stageData;

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
public class StageData
{
    [Header("---Data---")]
    /// <summary>
    /// 스테이지 클리어 여부
    /// </summary>
    public bool isClear;
    /// <summary>
    /// N-NN 스테이지 순서
    /// </summary>
    public int stageCount;
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

