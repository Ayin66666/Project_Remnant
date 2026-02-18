using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static IdentityMasterSO;


public class PassiveContainerUI : MonoBehaviour
{
    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    // 이쪽은 아직 미사용 - 향후 패시브의 중요도에 따른 표시 기능 예정
    [SerializeField] private Image borderImage;
    [SerializeField] private Sprite[] borderSprites;

    public void SetUp(PassiveUIData data)
    {
        nameText.text = data.passiveName;
        descriptionText.text = data.passiveDescription;

        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }
}
