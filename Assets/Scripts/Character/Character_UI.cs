using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Character_UI : MonoBehaviour
{
    [Header("---Character---")]
    [SerializeField] private Character_Base character;

    [Header("---UI---")]
    [SerializeField] private Slider hpSlider;
    [SerializeField] private GameObject groggyLinePrefab;
    [SerializeField] private RectTransform groggyContainer;
    [SerializeField] private Dictionary<int, GameObject> groggyLine;

    public void UI_Setting(Character_Base character)
    {
        this.character = character;
        hpSlider.maxValue = character.Hp;
        hpSlider.value = character.Hp;

        float sliderWidth = hpSlider.fillRect.rect.width;
        foreach (int gHp in character.Groggy)
        {
            GameObject marker = Instantiate(groggyLinePrefab, groggyContainer);
            RectTransform rt = marker.GetComponent<RectTransform>();
            groggyLine.Add(gHp, marker);

            // 체력 절대값 기준 X 위치
            float posX = sliderWidth * (gHp / (float)character.Hp);
            rt.anchoredPosition = new Vector2(posX, 0);
        }
    }

    public void UI_Update()
    {
        hpSlider.value = character.Hp;

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
}
