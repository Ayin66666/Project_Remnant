using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class SkillDescriptionSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private SkillSO data;


    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private RectTransform coinRect;
    [SerializeField] private GameObject coinPrefab;


    /// <summary>
    /// 데이터를 받아와서 슬롯에 설정
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(SkillSO data)
    {
        // 보더 컬러는 어떻게? - 이거 속성에 따라서 변경 예정
        // 분노 = 빨강
        // 오만 = 파랑
        // 우울 = 하늘
        // 탐식 = 초록
        // 나태 = 노랑
        // 질투 = 보라
        // 색욕 = 주황

        // 초기화
        Clear();

        // UI 배치
        this.data = data;
        icon.sprite = data.ui.icon;
        coinPowerText.text = $"+ {data.coinPower}";
        for (int i = 0; i < data.coins.Count; i++)
        {
            GameObject obj = Instantiate(coinPrefab, coinRect);
        }
    }

    /// <summary>
    /// 슬롯 초기화 로직 - 혹시 모를 상황 대비
    /// </summary>
    public void Clear()
    {
        data = null;
        icon.sprite = null;
        coinPowerText.text = "";
    }


    #region Mouse Event
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Call MouseA");
        CharacterDescriptionUI.instance.ShowSkillSlotDescription(true, data);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Call MouseB");
        CharacterDescriptionUI.instance.ShowSkillSlotDescription(false, null);
    }
    #endregion
}
