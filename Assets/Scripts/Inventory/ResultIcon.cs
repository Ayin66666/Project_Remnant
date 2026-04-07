using Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ResultIcon : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;


    public void SetUp(ItemSO so, int count)
    {
        icon.sprite = so.ItemIcon;
        countText.text = count.ToString();
    }
}
