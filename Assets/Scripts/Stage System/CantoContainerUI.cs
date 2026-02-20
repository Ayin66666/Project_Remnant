using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class CantoContainerUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private CantoData data;

    [Header("---UI---")]
    [SerializeField] private Image cantoImage;
    [SerializeField] private TextMeshProUGUI cantoNameText;
    [SerializeField] private TextMeshProUGUI cantoCountText;
    [SerializeField] private Image borderImage;


    public void SetUp(CantoData data, bool isOn)
    {
        this.data = data;
        cantoNameText.text = data.CantoName;
        cantoCountText.text = $"{data.cantoCount} 장";
        cantoImage.sprite = data.cantoSprite;
    }


    #region Mouse Event
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Canto Container");
        StageManager.instance.CantoUI(true, data.cantoCount);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        borderImage.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 테스트 이미지 컬러임! -> 원래는 1,1,1,1
        borderImage.color = new Color(0.1333333f, 0.1333333f, 0.1333333f, 1); 
    }
    #endregion
}
