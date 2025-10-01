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


    #region ���콺 �̺�Ʈ
    public void OnPointerEnter(PointerEventData eventData)
    {
        // �� ���¶�� �� UI, �ƴ϶�� �Ϲ� UI, ��ų�� ���ٸ� ǥ�� x
        // �Ǵ� ��ġ��? ���Կ���? �ƴϸ� turn_Manager��?
        // �� �����͸� turn_Manager�� ��� �ִٸ�?
        // �ڱ� ���� - üũ�ؼ� �� ���̸� ��� ���Ե�?

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
