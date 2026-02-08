using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class EgoDescriptionSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private Rank slotRank;
    [SerializeField] private EgoData data;

    [Header("---UI---")]
    [SerializeField] private Image iconImage;
    [SerializeField] private Image syncImage;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI rankText;
    [SerializeField] private Sprite[] syncIcon;


    private void Start()
    {
        rankText.text = $"{slotRank}";
    }

    /// <summary>
    /// 슬롯 데이터 설정
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(EgoData data)
    {
        this.data = data;
        iconImage.sprite = data.master.egoSprite;
        syncImage.sprite = syncIcon[data.sync];
        nameText.text = data.master.egoName;
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        data = null;
        iconImage.sprite = null;
        syncImage.sprite = null;
        nameText.text = "";
    }


    #region Mouse Event
    public void OnPointerEnter(PointerEventData eventData)
    {
        // UI 표시
        if (data != null)
            CharacterDescription.instance.ShowEgoSlotDescription(true, data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // UI 종료
            CharacterDescription.instance.ShowEgoSlotDescription(false, data);
    }
    #endregion
}
