using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Item;


public class InventorySlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private ItemSO item;
    public ItemSO Item => item;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;


    #region Setting
    /// <summary>
    /// 데이터 세팅
    /// </summary>
    /// <param name="item"></param>
    /// <param name="count"></param>
    public void SetUp(ItemSO item, int count)
    {
        // 데이터
        this.item = item;

        // UI
        icon.sprite = item.ItemIcon;
        countText.text = count.ToString();
    }

    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void Clear()
    {
        // 데이터
        item = null;

        // UI
        icon.sprite = null;
        countText.text = "";
    }
    #endregion


    #region Click Event
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 설명 UI On
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 설명 UI Off
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 아이템 사용 (소모품이라면)
    }
    #endregion
}
