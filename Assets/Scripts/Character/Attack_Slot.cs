using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Attack_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private Skill_Base skill;
    public bool haveSkill;


    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private GameObject tauntImage;
    [SerializeField] private TextMeshProUGUI speedText;


    public void Slot_Setting(Skill_Base skill, int index)
    {
        haveSkill = true;
        this.skill = skill;
        icon.sprite = skill.skillData.SkillDatas[index].icon;
    }

    public void Slot_Reset()
    {
        haveSkill = false;
        skill = null;
        icon.sprite = null;
    }


    #region 마우스 이벤트
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 합 상태라면 합 UI, 아니라면 일반 UI, 스킬이 없다면 표시 x
        // 판단 위치는? 슬롯에서? 아니면 turn_Manager가?
        // 합 데이터를 turn_Manager가 들고 있다면?
        // 자기 슬롯 - 체크해서 합 중이면 상대 슬롯도?

        if (haveSkill)
        {
            // UI_Manager.instance.Skill_UI(skill, true); 
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (haveSkill)
        {
            // UI_Manager.instance.Skill_UI(skill, false); 
        }
    }
    #endregion
}
