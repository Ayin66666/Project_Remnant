using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EgoSlot : MonoBehaviour
{
    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image egoImage;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Image syncIcon;

    [Header("---Rank & Sync Image---")]
    [SerializeField] private Sprite[] tierSprites;
    [SerializeField] private Sprite[] syncSprites;


    public void SetUp(EgoData info)
    {
        nameText.text = info.master.egoName;
        egoImage.sprite = info.master.egoSprite;
        egoImage.color = new Color(1, 1, 1, 1);
        rankIcon.sprite = tierSprites[info.master.egoRank];
        syncIcon.sprite = syncSprites[info.sync];
    }

    public void Clear()
    {
        nameText.text = "";
        egoImage.sprite = null;
        egoImage.color = new Color(1, 1, 1, 0);
        rankIcon.sprite = tierSprites[0];
        syncIcon.sprite = syncSprites[0];
    }
}


