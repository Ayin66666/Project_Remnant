using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class AttackSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---state---")]
    [SerializeField] private bool haveSkill;
    [SerializeField] private Skill_Base skill;
    [SerializeField] private AttackSlot targetSlot;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image tunatImage;
    [SerializeField] private TextMeshProUGUI speedText;


    public void Skill_Setting(bool isOn, GameObject skillInfo)
    {
        // 스킬 데이터를 받은 뒤 UI 표시
        // - 일단은 Gameobject라 해뒀지만 나중에 Skill_Base 만들면 그거로 변경
        haveSkill = isOn;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(haveSkill)
        {
            // UI 표시
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(haveSkill)
        {
            // UI 종료
        }
    }

    // 슬롯 역할
    // 1. 스킬 & 속도 표시
    // 2. 도발치 표시
    // 3. 마우스 오버 시 스킬 & 합 설명 On/Off
}
