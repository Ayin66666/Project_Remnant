using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EgoSlot : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    [Header("---Setting---")]
    [SerializeField] private SlotType slotType;
    [SerializeField] private Rank slotRank;
    [SerializeField] private EgoData egoData;
    private enum SlotType
    {
        Small,
        Big
    }
    

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
        egoData = info;

        nameText.text = info.master.egoName;
        egoImage.sprite = info.master.egoSprite;
        egoImage.color = new Color(1, 1, 1, 1);
        rankIcon.sprite = tierSprites[(int)info.master.egoRank];
        syncIcon.sprite = syncSprites[info.sync];
    }

    public void Clear()
    {
        egoData = null;

        nameText.text = "";
        egoImage.sprite = null;
        egoImage.color = new Color(1, 1, 1, 0);
        rankIcon.sprite = tierSprites[0];
        syncIcon.sprite = syncSprites[0];
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // 1초동안 꾹 누른다면 상세 정보 UI - 1회 클릭이라면 해당 티어의 에고 표시
        // OrganizationManager.instance.Show
    }
}

public enum Rank
{
    ZAYIN,
    TETH,
    HE,
    WAW,
    ALEPH
}


