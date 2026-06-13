using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class SkillSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 슬롯 역할
    // 1. 스킬 표시
    // 2. 도발치 표시
    // 3. 마우스 오버 시 스킬 & 합 설명 On/Off 이벤트 전달

    [Header("---state---")]
    [SerializeField] private CharacterBase onwer;
    [SerializeField] private bool haveSkill;
    [SerializeField] private float tunatValue;
    [SerializeField] private SkillBase skillBase;
    public CharacterBase Onwer => onwer;
    public float TunatValue => tunatValue;
    public SkillBase Skill => skillBase;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject tunatUI;
    [SerializeField] private TextMeshProUGUI tunatText;


    #region 데이터 세팅
    /// <summary>
    /// 스킬 슬롯에 데이터 & UI 세팅
    /// </summary>
    /// <param name="skill"></param>
    public void SetUp(SkillBase skill)
    {
        // 데이터 삽입
        haveSkill = true;
        skillBase = skill;

        // UI 세팅
        icon.sprite = skill.SkillSO.icon;
    }

    /// <summary>
    /// 도발치 UI 세팅
    /// </summary>
    /// <param name="isOn"></param>
    /// <param name="value"></param>
    public void TunatSetUp(bool isOn, int value)
    {
        if (isOn)
        {
            tunatValue = value;
            tunatText.text = $"! {tunatValue}";
        }
        tunatUI.SetActive(isOn);
    }

    /// <summary>
    /// 스킬 슬롯 데이터 & UI 초기화
    /// </summary>
    public void Clear()
    {
        // 데이터 초기화
        haveSkill = false;
        skillBase = null;
        tunatValue = 0f;

        // UI 초기화
        icon.sprite = null;
        tunatUI.SetActive(false);
        tunatText.text = string.Empty;
    }
    #endregion


    #region 마우스 이벤트
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (haveSkill)
        {
            // UI 표시
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (haveSkill)
        {
            // UI 종료
        }
    }
    #endregion
}
