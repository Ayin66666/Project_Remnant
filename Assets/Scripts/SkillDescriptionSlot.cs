using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


public class SkillDescriptionSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private SkillBase data;


    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private RectTransform coinRect;
    [SerializeField] private GameObject coinPrefab;

    // 스킬 데이터 완성 후 제작할것!

    public void SetUp(SkillBase data)
    {

    }

    public void Clear()
    {
        icon.sprite = null;
        coinPowerText.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
