using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class UI_Ego_Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---UI---")]
    [SerializeField] private Image egoImage;
    [SerializeField] private TextMeshProUGUI egoText;

    public void Setting(Sprite icon, string description, bool isOn)
    {
        egoImage.sprite = isOn ? icon : null;
        egoText.text = isOn ? description : "";
    }


    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
