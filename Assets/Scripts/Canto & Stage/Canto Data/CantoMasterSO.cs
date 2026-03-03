using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Canto00_CantoName", menuName = "Canto/CantoMaster", order = int.MaxValue)]
public class CantoMasterSO : ScriptableObject
{
    [Header("---Canto Data---")]
    /// <summary>
    /// 칸토 이름
    /// </summary>
    [SerializeField] private string cantoName;
    /// <summary>
    /// 칸토 순서 (N장)
    /// </summary>
    [SerializeField] private int cantoOrder;
    /// <summary>
    /// 스테이지 데이터
    /// </summary>
    [SerializeField] private List<StageMasterSO> stageData;
    /// <summary>
    /// 리워드 데이터
    /// </summary>
    [SerializeField] private List<CantoRewardSO> rewardData;

    [Header("---Select UI---")]
    /// <summary>
    /// 운전대에서 보여지는 칸토 선택 슬롯용 배경 UI
    /// </summary>
    [SerializeField] private Sprite cantoSprte;


    public string CantoName { get { return cantoName; } set { cantoName = value; } }
    public int CantoOrder { get { return cantoOrder; } set { cantoOrder = value; } }
    public List<StageMasterSO> StageData { get { return stageData; } set { stageData = value; } }
    public List<CantoRewardSO> RewardCount { get { return rewardData; } private set { rewardData = value; } }
    public Sprite CantoSprte { get { return cantoSprte; } set { cantoSprte = value; } }
}



