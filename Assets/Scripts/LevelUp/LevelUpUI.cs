using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Item;


public class LevelUpUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private OrganizationData sinnerData;
    private Dictionary<int, int> addExpData = new Dictionary<int, int>();
    private Dictionary<int, int> ticketData = new Dictionary<int, int>()
    {
        { 90100, 100 },
        { 90500, 500 },
        { 91000, 1000 },
        { 95000, 5000 },
    };


    [Header("---Left UI---")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI defenceText;
    [SerializeField] private TextMeshProUGUI[] skillText;

    [Header("---Right UI---")]
    [SerializeField] private LevelUpInputField[] inputField_Exp;




    #region 데이터 세팅 & 초기화
    /// <summary>
    /// UI를 오픈한 인격의 데이터 전달받기
    /// </summary>
    public void SetUp(OrganizationData data)
    {
        // 데이터 입력
        sinnerData = data;
        IdentityData iden = sinnerData.identity;

        // 레벨 UI
        int maxExp = GameManager.instance.levelManager.GetNeedExp(sinnerData.identity.level);
        levelText.text = $"Lv.{iden.level} ▷ {iden.level}";
        expText.text = $"{iden.curExp} ▷ {maxExp}";
        expSlider.maxValue = maxExp;
        expSlider.value = iden.curExp;

        // 능력치 계산
        StatusDataSO statData = iden.master.statData;
        int maxHp = CalHp(iden, iden.level);
        int attack = CalAttack(iden, iden.level);
        int defence = CalDefence(iden, iden.level);

        // 스킬 UI
        hpText.text = $"{maxHp} ▷ {maxHp}";
        defenceText.text = $"{defence} ▷ {defence}";
        for (int i = 0; i < skillText.Length; i++)
        {
            skillText[i].text = $"스킬1\n<sprite=1> {attack}";
        }

        // 재료 UI
        foreach (var slot in inputField_Exp)
        {
            slot.SetUp(this);
        }
    }

    public void Clear()
    {

    }

    public int CalHp(IdentityData iden, int level)
    {
        StatusDataSO statData = iden.master.statData;

        int maxHp = statData.BaseHp // 기본 체력
            + statData.SyncUpData[iden.sync].hp // 동기화 체력 
            + Mathf.RoundToInt(statData.LevelUpData.hp * iden.level * statData.GrowthFactorData.hpFactor);

        return maxHp;
    }

    public int CalDefence(IdentityData iden, int level)
    {
        StatusDataSO statData = iden.master.statData;

        int defence = statData.BaseDefencePoint
            + statData.SyncUpData[iden.sync].defence
            + Mathf.RoundToInt(statData.LevelUpData.defence * iden.level * statData.GrowthFactorData.defenceFactor);

        return defence;
    }

    public int CalAttack(IdentityData iden, int level)
    {
        StatusDataSO statData = iden.master.statData;

        int attack = statData.BaseAttackPoint
            + statData.SyncUpData[iden.sync].attack
            + Mathf.RoundToInt(statData.LevelUpData.attack * iden.level * statData.GrowthFactorData.attackFactor);

        return attack;
    }
    #endregion


    #region
    /// <summary>
    /// 현재 투입중인 경험치 티켓 개수 반환
    /// </summary>
    /// <param name="type">티켓 타입</param>
    /// <returns></returns>
    public int GetUsedTicketCount(int id)
    {
        if(addExpData.ContainsKey(id))
            return addExpData[id];
        else
            return 0;
    }

    /// <summary>
    /// 투입 예정인 경험치 티켓 데이터 세팅
    /// </summary>
    /// <param name="id">티켓 종류</param>
    /// <param name="count">투입 개수</param>
    public void SetAddExp(int id, int count)
    {
        if (addExpData.ContainsKey(id))
            addExpData[id] += count;
        else
            addExpData.Add(id, count);
    }

    /// <summary>
    /// 현재 선택된 티켓 데이터를 기반으로 최대 레벨업까지 남은 경험치 반환
    /// </summary>
    /// <returns></returns>
    public int CalRemainingExp()
    {
        // 남은 총 경험치 계산
        int remainingExp = 0;
        for (int i = sinnerData.identity.level; i < GameManager.instance.levelManager.MaxLevel; i++)
        {
            remainingExp += GameManager.instance.levelManager.GetNeedExp(i);
        }

        // 추가 예정인 총 경험치 계산
        int addExp = 0;
        foreach (var data in addExpData)
        {
            addExp += ticketData[data.Key] * data.Value;
        }

        // 필요한 경험치 반환
        int needExp = remainingExp - addExp;
        return needExp;
    }
    #endregion
}
