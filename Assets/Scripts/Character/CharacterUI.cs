using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterUI : MonoBehaviour
{
    [Header("---Character---")]
    [SerializeField] private CharacterBase character;

    [Header("---Hp & Groggy---")]
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject groggyLinePrefab;
    [SerializeField] private RectTransform groggyContainer;
    [SerializeField] private Dictionary<int, GameObject> groggyLine = new Dictionary<int, GameObject>();

    [Header("---Buff & Debuff---")]
    [SerializeField] private RectTransform statusIconRect;
    [SerializeField] private GameObject statusIconPrefab;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            AddStatusEffectIcon(gameObject);
        }
    }

    /// <summary>
    /// 체력 & 그로기 UI 셋팅 (최초 1회)
    /// </summary>
    /// <param name="character"></param>
    public void UI_Setting(CharacterBase character)
    {
        this.character = character;
        hpSlider.maxValue = character.Hp;
        hpSlider.value = character.Hp;
        hpText.text = character.Hp.ToString();


        RectTransform sliderRect = hpSlider.GetComponent<RectTransform>();
        float sliderWidth = sliderRect.rect.width;

        foreach (int gHp in character.Groggy)
        {
            GameObject marker = Instantiate(groggyLinePrefab, groggyContainer);
            RectTransform rt = marker.GetComponent<RectTransform>();

            float ratio = gHp / (float)character.Hp;
            ratio = 1f - ratio;
            float posX = sliderWidth * ratio - sliderWidth * 0.5f;

            rt.anchoredPosition = new Vector2(posX, 0);
            groggyLine.Add(gHp, marker);
        }
    }

    /// <summary>
    /// 체력 & 그로기 UI 업데이트
    /// </summary>
    public void UI_Update()
    {
        hpSlider.value = character.Hp;
        hpText.text = character.Hp.ToString();

        List<int> toRemove = new List<int>();
        foreach (var kvp in groggyLine)
        {
            if (character.Hp <= kvp.Key)
            {
                Destroy(kvp.Value);
                toRemove.Add(kvp.Key);
            }
        }

        foreach (int key in toRemove)
        {
            groggyLine.Remove(key);
        }
    }


    /// <summary>
    /// 버프 & 디버프 추가
    /// </summary>
    /// <param name="debuffInfo"></param>
    public void AddStatusEffectIcon(GameObject debuffInfo)
    {
        Instantiate(statusIconPrefab, statusIconRect);
    }

    /// <summary>
    /// 버프 & 디버프 툴팁 On
    /// </summary>
    /// <param name="debuffInfo"></param>
    public void ShowStatusEffectTooltip(GameObject debuffInfo)
    {

    }

    /// <summary>
    /// 버프 & 디버프 툴팁 Off
    /// </summary>
    public void HideEffectTooltip()
    {

    }
}
