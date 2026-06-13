using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SkillSelectNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("---Setting---")]
    [SerializeField] private Select nodeType;
    [SerializeField] private SkillBase skill;
    [SerializeField] private SkillSlot slot;
    [SerializeField] private SelectNodeGroup myGroup;
    public Select NodeType => nodeType;
    public SkillSlot Slot => slot;
    public SelectNodeGroup Group => myGroup;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;
    [SerializeField] private GameObject highlight;


    #region 데이터 세팅 & 초기화 로직
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
        icon.sprite = skill.SkillSO.icon;
        border.color = SkillUIUtility.GetCrimeColor(skill.SkillSO.crimeType);
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
    #endregion


    /// <summary>
    /// 슬롯 가장자리 하이라이트 효과 On/Off
    /// </summary>
    /// <param name="isOn"></param>
    private void HighlightEffect(bool isOn)
    {
        highlight.SetActive(isOn);
    }


    #region 마우스 이벤트
    /// <summary>
    /// 마우스 접근 시 동작 - 스킬 하이라이트 연출
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        HighlightEffect(true);
    }

    /// <summary>
    /// 마우스 이탈 시 동작 - 스킬 하이라이트 연출 종료
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerExit(PointerEventData eventData)
    {
        HighlightEffect(false);
    }

    /// <summary>
    /// 누르기 시작 시 동작 - 스킬 체인 생성 & 조준
    /// </summary>
    /// <param name="eventData"></param>
    /// <exception cref="System.NotImplementedException"></exception>
    public void OnPointerDown(PointerEventData eventData)
    {
        SkillSelectManager.instance.Selecting(this);
    }
    #endregion
}
