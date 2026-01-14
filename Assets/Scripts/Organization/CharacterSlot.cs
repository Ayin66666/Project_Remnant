using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CharacterSlot : MonoBehaviour, IPointerClickHandler
{
    [Header("---Setting---")]
    [SerializeField] private SlotType type;
    [SerializeField] private IdentityData identityInfo;
    public enum SlotType { Organization, Select }


    [Header("---UI---")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;

    [SerializeField] private GameObject[] rankIcon;
    [SerializeField] private Sprite[] borderSprite;


    public void SetUp(IdentityData info)
    {
        characterImage.sprite = info.master.portrait;
        border.sprite = borderSprite[info.sync >= 3 ? 1 : 0]; // 3동기화 이상일 경우 테두리 변경
        levelText.text = info.level.ToString();
        nameText.text = info.master.identityName;

        foreach (GameObject icon in rankIcon)
        {
            icon.SetActive(false);
        }
        for (int i = 0; i < info.sync; i++)
        {
            rankIcon[i].SetActive(true);
        }
    }

    public void Clear()
    {
        characterImage.sprite = null;
        border.sprite = borderSprite[0];
        levelText.text = "";
        nameText.text = "";

        foreach (GameObject icon in rankIcon)
        {
            icon.SetActive(false);
        }
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        switch (type)
        {
            // 편성창 슬롯이면
            case SlotType.Organization: 
                ClickOrganization();
                break;

            // 인격 & 에고 선택창 슬롯이면
            case SlotType.Select: 
                ClickSelect();
                break;
        }
    }

    private void ClickOrganization()
    {
        // 편성창에서 클릭했을 때 동작
        // 인격 & 에고 선택창 표시
    }

    private void ClickSelect()
    {
        // 선택창에서 클릭했을 때 동작
        // 인격 장착 후 편성창으로 돌아감
    }
}
