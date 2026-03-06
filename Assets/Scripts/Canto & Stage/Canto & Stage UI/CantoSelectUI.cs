using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class CantoSelectUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        Debug.Log($"슬롯 데이터 주입 / {data} / {isOn}");

        this.data = data;
        cantoNameText.text = data.cantoData.CantoName;
        cantoCountText.text = $"{data.cantoData.CantoOrder} 장";
        cantoImage.sprite = data.cantoData.CantoSprte;
    }


    #region Mouse Event
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Canto Container");
        StageManager.instance.CantoUI(true, data.cantoData.CantoOrder);
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
