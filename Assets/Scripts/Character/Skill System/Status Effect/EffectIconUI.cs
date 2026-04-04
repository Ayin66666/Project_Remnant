using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EffectIconUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Setting---")]
    [SerializeField] private StatusEffectInfo statusEffectInfo;
    [SerializeField] private CharacterUI ui;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI countText;


    /// <summary>
    /// 최초 1회 버프 & 디버프 추가 시 호출
    /// </summary>
    /// <param name="info"></param>
    public void SetUp(StatusEffectInfo info, CharacterUI ui)
    {
        statusEffectInfo = info;
        this.ui = ui;

        icon.sprite = info.effectSO.effectIcon;
        countText.text = $"{info.power} {info.count}";
    }

    /// <summary>
    /// 디버프 데이터 업데이트 - 위력 & 횟수 변경 및 키워드 전환 시 호출
    /// </summary>
    /// <param name="debuffInfo"></param>
    public void UpdateDebuff(StatusEffectInfo info)
    {
        statusEffectInfo = info;
        icon.sprite = info.effectSO.effectIcon;
        countText.text = $"{info.power} {info.count}";
    }

    /// <summary>
    /// 버프 & 디버프 제거 시 호출
    /// </summary>
    public void Remove()
    {
        Destroy(gameObject);
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        // 설명 UI On - UI의 동작 위치는 CharacterUI에서 / 자신의 데이터만 전달해줌
        ui.ShowStatusEffectTooltip(gameObject);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 설명 UI Off
        ui.HideEffectTooltip();
    }
}
