using Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ResultIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private ItemSO so;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;


    public void SetUp(ItemSO so, int count)
    {
        // Data
        this.so = so;

        // UI
        icon.sprite = so.ItemIcon;
        countText.text = count.ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 설명 UI On
        GameManager.instance.inventory.SummaryUI(so, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 설명 UI Off
        GameManager.instance.inventory.SummaryUI(so, false);
    }
}
