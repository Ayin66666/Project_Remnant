using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MentalityUI : MonoBehaviour
{
    [Header("---UI---")]
    [SerializeField] private Image mentalityIcon;
    [SerializeField] private TextMeshProUGUI mentalityNameText;
    [SerializeField] private TextMeshProUGUI mentalityDescriptionText;


    public void SetUp(IdentityData data)
    {
        Clear();

        mentalityIcon.sprite = data.master.mentalityIcon;
        mentalityNameText.text = data.master.mentalityName;
        mentalityDescriptionText.text = data.master.mentalityDescription;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    public void Clear()
    {
        mentalityIcon.sprite = null;
        mentalityNameText.text = string.Empty;
        mentalityDescriptionText.text = string.Empty;
    }
}
