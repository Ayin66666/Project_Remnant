using Game.Character;
using Game.Stage;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class StageDescriptionUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private GameObject enemyDataIcon;
    [SerializeField] private List<GameObject> attackTypeIcon;
    [SerializeField] private List<GameObject> crimeIcon;
    private Dictionary<AttackType, GameObject> attackTypeIconDic;
    private Dictionary<Crime, GameObject> crimeIconDic;
    private bool isSetUp = false;

    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI stageNameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI exClearText;
    [SerializeField] private RectTransform validAttributeRect;
    [SerializeField] private RectTransform enemyRect;
    [SerializeField] private RectTransform rewardRect;


    #region 시작 로직
    private void Awake()
    {
        AddIconToDic();
    }

    private void AddIconToDic()
    {
        Debug.Log("아이콘 딕셔너리 세팅");
        isSetUp = true;

        // 아이콘 데이터 딕셔너리에 배치
        attackTypeIconDic = new Dictionary<AttackType, GameObject>();
        crimeIconDic = new Dictionary<Crime, GameObject>();

        for (int i = 0; i < Enum.GetValues(typeof(AttackType)).Length; i++)
        {
            attackTypeIconDic.Add((AttackType)i, attackTypeIcon[i]);
        }
        for (int i = 0; i < Enum.GetValues(typeof(Crime)).Length; i++)
        {
            crimeIconDic.Add((Crime)i, crimeIcon[i]);
        }
    }
    #endregion


    #region 데이터 로직
    /// <summary>
    /// UI 데이터 세팅
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(StageData data)
    {
        if (!isSetUp) 
            AddIconToDic();

        // 초기화
        Clear();

        // UI 세팅
        stageNameText.text = data.stageSO.StageName;
        levelText.text = $"Lv.{data.stageSO.StageLevel}";
        switch (data.stageSO.ExClearCondition.ConditionType)
        {
            case ExClear.StageClear:
                exClearText.text = "스테이지 클리어";
                break;

            case ExClear.TurnLimit:
                exClearText.text = $"{data.stageSO.ExClearCondition.conditionCount}턴 이내에 스테이지 클리어";
                break;

            case ExClear.NoDied:
                exClearText.text = "아무도 죽지 않은 상태에서 스테이지 클리어";
                break;
        }

        // 유효 속성 UI

        foreach (AttackType type in data.stageSO.ValidAttack)
        {
            Debug.Log(attackTypeIconDic[type]);
            GameObject obj = Instantiate(attackTypeIconDic[type], validAttributeRect);
        }
        foreach (Crime type in data.stageSO.ValidCrimes)
        {
            GameObject obj = Instantiate(crimeIconDic[type], validAttributeRect);
        }

        // 등장 몬스터 UI
        foreach (EnemyMasterSO enemy in data.stageSO.EnemyData)
        {
            GameObject obj = Instantiate(enemyDataIcon, enemyRect);
            EnemyIconUI icon = obj.GetComponent<EnemyIconUI>();
            icon.SetUp(enemy);
        }

        // 보상 UI
        // -> 이거 아직 데이터 없음 (추가 여부 고민해볼것)
    }

    /// <summary>
    /// 초기화 로직
    /// </summary>
    public void Clear()
    {
        stageNameText.text = string.Empty;
        levelText.text = string.Empty;

        foreach (Transform child in validAttributeRect)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in enemyRect)
        {
            Destroy(child.gameObject);
        }
        /*
        foreach (Transform child in rewardRect)
        {
            Destroy(child.gameObject);
        }
        */
    }
    #endregion


    #region 버튼 로직
    /// <summary>
    /// 편성창으로 진입함!
    /// </summary>
    public void ClickEnter()
    {
        Debug.Log("편성창으로 진입");
        OrganizationManager.instance.OrganizationUI(true, true);
    }

    /// <summary>
    /// UI 영역을 제외한 다른 부분 클릭 시 동작함!
    /// </summary>
    public void ClickOut()
    {
        Debug.Log("설명 종료");
        BattleContentManager.instance.StageDescriptionUI(false);
    }
    #endregion
}
