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
    [SerializeField] private bool haveSkill;
    [SerializeField] private SkillBase skillBase;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image tunatImage;


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
        icon.sprite = skill.Icon;
    }

    /// <summary>
    /// 스킬 슬롯 데이터 & UI 초기화
    /// </summary>
    public void Clear()
    {
        // 데이터 초기화
        haveSkill = false;
        skillBase = null;

        // UI 초기화
        icon.sprite = null;
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
