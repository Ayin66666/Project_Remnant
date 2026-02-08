using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterDescription : MonoBehaviour
{
    public static CharacterDescription instance;

    [Header("---Test---")]
    [SerializeField] private OrganizationData tdata;

    [Header("---Component---")]
    [SerializeField] private CharacterSummation summation;
    [SerializeField] private DescriptionUI descriptionUI;

    [Header("---UI---")]
    [SerializeField] private GameObject[] uiSet;

    [Header("---Left Status UI---")]
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private TextMeshProUGUI[] resistText;

    [Header("---Right Status UI---")]
    [SerializeField] private List<GameObject> rankIcon;
    [SerializeField] private Image characterIcon;
    [SerializeField] private Sprite[] characterSprite;

    [SerializeField] private Image syncIcon;
    [SerializeField] private Sprite[] syncSprite;

    [SerializeField] private TextMeshProUGUI identityNameText;

    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Slider expSlider;

    [Header("---Prefab---")]
    [SerializeField] private GameObject skillSlotPrefab;


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
        StatusL(data);

        // 우상단 이름 & 레벨
        StatusR(data);

        // 요약
        summation.SetUp(data);

        // 에고

        // 스킬

        // 패시브

    }

    /// <summary>
    /// 좌측 능력치 설정
    /// </summary>
    /// <param name="data"></param>
    public void StatusL(OrganizationData data)
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
            resistText[i].text = data.identity.master.statData.AttackResistance[i].ToString("0.0");
        }
    }

    /// <summary>
    /// 우측 능력치 설정
    /// </summary>
    /// <param name="data"></param>
    public void StatusR(OrganizationData data)
    {
        // 인격 등급
        for (int i = 0; i < data.identity.master.identityRank; i++)
        {
            rankIcon[i].SetActive(true);
        }

        // 캐릭터 아이콘
        characterIcon.sprite = characterSprite[(int)data.sinner];

        // 동기화 아이콘
        syncIcon.sprite = syncSprite[data.identity.sync];

        // 이름
        identityNameText.text = data.identity.master.identityName;

        // 레벨 & 경험치 바
        levelText.text = $"Lv {data.identity.level}";
        expText.text = $"{data.identity.curExp} / {data.identity.maxExp[data.identity.level - 1]}";
        expSlider.maxValue = data.identity.maxExp[data.identity.level - 1];
        expSlider.value = data.identity.curExp;
    }

    /// <summary>
    /// 캐릭터 설명 UI Off
    /// </summary>
    public void Clear()
    {
        // 설명 UI
        descriptionUI.Clear();
        descriptionUI.gameObject.SetActive(false);

        // 요약 UI
        summation.Clear();

        // 에고 UI

        // 스킬 UI

        // 패시브
    }
    #endregion


    #region 버튼 이벤트
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

    /// <summary>
    /// 레벨업 - 레벨업 UI
    /// </summary>
    public void LevelUp()
    {

    }

    /// <summary>
    /// 동기화 - 동기화업 UI
    /// </summary>
    public void SyncUp()
    {

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
        descriptionUI.gameObject.SetActive(isOn);
        if (isOn) descriptionUI.SetUp(skill);
    }

    /// <summary>
    /// 인격 상세정보 - 에고 설명 슬롯 클릭 시 호출
    /// </summary>
    /// <param name="info"></param>
    public void ShowEgoSlotDescription(bool isOn, EgoData ego)
    {
        // 받은 정보를 기반으로 데이터 표시
        descriptionUI.gameObject.SetActive(isOn);
        if (isOn) descriptionUI.SetUp(ego);
    }
    #endregion
}
