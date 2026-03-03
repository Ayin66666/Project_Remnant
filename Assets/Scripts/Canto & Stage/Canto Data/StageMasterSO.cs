using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageData_Canto00_Stage00", menuName = "Canto/Stage/StageManager", order = int.MaxValue)]
public class StageMasterSO : ScriptableObject
{
    [Header("---Setting---")]
    /// <summary>
    /// 스테이지 이름
    /// </summary>
    [SerializeField] private string stageName;
    /// <summary>
    /// 스테이지 번호 N-NN
    /// </summary>
    [SerializeField] private string stageOrder;
    /// <summary>
    /// 전투 씬 이름
    /// </summary>
    [SerializeField] private string scenePath;
    /// <summary>
    /// Ex 클리어 조건 (데이터 추가 필요)
    /// </summary>
    [SerializeField] private GameObject exClearCondition;
    /// <summary>
    /// 등장 Enenmy 데이터
    /// </summary>
    [SerializeField] private List<IdentityMasterSO> enemyData;

    public string StageName {  get { return stageName; } set { stageName = value; } }
    public string StageOrder { get { return stageOrder; } set { stageOrder = value; } }
    public string ScenePath { get { return scenePath; } set { scenePath = value; } }
    public GameObject ExClearCondition { get { return exClearCondition; } set { exClearCondition = value; } }
    public List<IdentityMasterSO> EnemyData {  get { return enemyData; } set { enemyData = value; } }
}

/// <summary>
/// 고민 필요
/// </summary>
public enum ExClear { StageClear, TurnLimit, NoDied }
