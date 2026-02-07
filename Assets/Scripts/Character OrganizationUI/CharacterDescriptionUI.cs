using TMPro;
using UnityEngine;


public class CharacterDescriptionUI : MonoBehaviour
{
    public static CharacterDescriptionUI instance;

    [Header("---Test---")]
    [SerializeField] private OrganizationData tdata;

    [Header("---Setting---")]
    [SerializeField] private CharacterSummation summation;


    [Header("---UI---")]
    [SerializeField] private GameObject[] uiSet;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI[] resistText;


    [Header("---Prefab---")]
    [SerializeField] private GameObject skillSlotPrefab;
    [SerializeField] private GameObject egoSlotPrefab;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetUp(tdata);
        }
    }

    #region 최초 & 종료 시 실행 
    public void SetUp(OrganizationData data)
    {
        // 최초 1회 실행 - 캐릭터의 편성 데이터를 받아와서 UI에 전달

        // 데이터 초기화
        Clear();

        // 캐릭터 능력치 (1번 = hp / 2번 = 속도 / 3번 = 공격 & 방어 포인트)
        Status(data);

        // 요약
        summation.SetUp(data);

        // 에고 UI

        // 스킬 UI

        // 패시브

    }

    public void Clear()
    {

    }

    public void Status(OrganizationData data)
    {
        StatusDataSO sd = data.identity.master.statData;
        int level = data.identity.level;
        int sync = data.identity.sync;

        int hp = sd.BaseHp // 기본 체력
            + sd.SyncUpData[sync].hp // 동기화 체력 
            + Mathf.RoundToInt(sd.LevelUpData.hp * level * sd.GrowthFactorData.hpFactor); // 레벨업 체력

        int attack = sd.BaseAttackPoint
            + sd.SyncUpData[sync].attack
            + Mathf.RoundToInt(sd.LevelUpData.attack * level * sd.GrowthFactorData.attackFactor);

        int defence = sd.BaseDefencePoint
            + sd.SyncUpData[sync].defence
            + Mathf.RoundToInt(sd.LevelUpData.defence * level * sd.GrowthFactorData.defenceFactor);

        Vector2Int speed = sd.SyncUpData[sync].attackSpeed;


        statusText.text = $"<sprite=1> {hp}  <sprite=2> {speed.x} - {speed.y}  <sprite=3> {attack}/{defence}";
        for (int i = 0; i < resistText.Length; i++)
        {
            resistText[i].text = $"{data.identity.master.statData.AttackResistance[i]}";
        }
    }
    #endregion


    #region 클릭 이벤트
    /// <summary>
    /// 0 : Summation
    /// 1 : Skill
    /// 2 : Ego
    /// 3 : Mentality
    /// </summary>
    /// <param name="index"></param>
    public void UISetting(int index)
    {
        foreach (GameObject obj in uiSet)
        {
            obj.SetActive(false);
        }

        uiSet[index].SetActive(true);
    }
    #endregion


    #region 스킬 & 에고 슬롯 이벤트
    /// <summary>
    /// 스킬 상세정보 - 스킬 설명 슬롯 클릭 시 호출
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="skill"></param>
    public void ShowSkillSlotDescription(bool isOn, SkillSO skill)
    {
        // 받은 정보를 기반으로 데이터 표시
        // - 이거 UI가 슬롯 옆에 있어야 하는데?
        Debug.Log("스킬슬롯 마우스 UI표시");
    }

    /// <summary>
    /// 인격 상세정보 - 에고 설명 슬롯 클릭 시 호출
    /// </summary>
    /// <param name="info"></param>
    public void ShowEgoSlotDescription(bool isOn, EgoData info)
    {
        // 받은 정보를 기반으로 데이터 표시
        // - 이거 UI가 슬롯 옆에 있어야 하는데?
        Debug.Log("에고슬롯 마우스 UI표시");
    }
    #endregion
}
