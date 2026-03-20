using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class CantoSelectUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private CantoRuntimeData data;

    [Header("---UI---")]
    [SerializeField] private Image cantoImage;
    [SerializeField] private TextMeshProUGUI cantoNameText;
    [SerializeField] private TextMeshProUGUI cantoCountText;
    [SerializeField] private Image borderImage;


    /// <summary>
    /// 칸토 데이터 주입
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isOn"></param>
    public void SetUp(CantoRuntimeData data, bool isOn)
    {
        this.data = data;
        cantoNameText.text = data.cantoData.CantoName;
        cantoCountText.text = $"{data.cantoData.CantoOrder} 장";
        cantoImage.sprite = data.cantoData.CantoSprte;
    }


    #region Mouse Event
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click Canto Container");
        BattleContentManager.instance.CantoSelect(true, data.cantoData.CantoOrder);
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
