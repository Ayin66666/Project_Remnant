using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSlot : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private CharacterId slotOnwer;
    [SerializeField] private IdentityData identityInfo;
    public CharacterId SlotOnwer { get { return slotOnwer; } private set { slotOnwer = value; } }

    [Header("---UI---")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject buttonSet;
    [SerializeField] private TextMeshProUGUI organizationCountText;


    [SerializeField] private GameObject[] rankIcon;
    [SerializeField] private Sprite[] borderSprite;


    #region 데이터 설정
    /// <summary>
    /// 캐릭터 슬롯 데이터 설정
    /// </summary>
    /// <param name="info"></param>
    public void SetUp(IdentityData info)
    {
        slotOnwer = info.master.sinner;
        characterImage.sprite = info.master.portrait;
        identityInfo = info;
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

    /// <summary>
    /// 데이터 제거
    /// </summary>
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
    #endregion


    #region 클릭 이벤트
    /// <summary>
    /// 편성창에서 클릭 (인격 선택창 표시)
    /// </summary>
    public void ShowIdentityList()
    {
        OrganizationManager.instance.OpenCharacterList(slotOnwer);
    }

    /// <summary>
    /// 편성하기 (순서)
    /// </summary>
    public void OrderSetting()
    {
        if (identityInfo == null) return;
        OrganizationDatabase.instance.OrganizationOrderSetting(slotOnwer);
    }

    /// <summary>
    /// 인격 상세정보 표시
    /// </summary>
    public void ShowIdentityUI()
    {
        if(identityInfo == null) return;

        OrganizationData data = OrganizationDatabase.instance.GetOrganizationData(slotOnwer);
        if (data != null)
            CharacterDescription.instance.SetUp(data);
        else
            Debug.Log("오류발생 / 인격 편성 체크필요");

    }
    #endregion
}
