using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SkillSelectNode : MonoBehaviour, IPointerEnterHandler
{
    [Header("---Setting---")]
    [SerializeField] private SkillBase skill;
    [SerializeField] private SelectNodeGroup myGroup;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;

    /// <summary>
    /// 자신이 속한 그룹 데이터 전달
    /// </summary>
    /// <param name="group"></param>
    public void SetUp(SelectNodeGroup group)
    {
        myGroup = group;
    }

    /// <summary>
    /// 선택 노드에 스킬 세팅
    /// </summary>
    /// <param name="skill"></param>
    public void SetUp(SkillBase skill)
    {
        this.skill = skill;

        // UI 세팅
        icon.sprite = skill.Icon;
        border.color = SkillUIUtility.GetCrimeColor(skill.CrimeType);
    }

    /// <summary>
    /// 선택 노드 초기화
    /// </summary>
    public void Clear()
    {
        skill = null;

        // UI 초기화
        icon.sprite = null;
        border.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
