using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CharacterSlot : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private SlotType type;
    [SerializeField] private IdentityData identityInfo;
    public enum SlotType { Organizing, IdentitySelect }


    [Header("---UI---")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject selectTextSet;
    [SerializeField] private GameObject buttonSet;

    [SerializeField] private GameObject[] rankIcon;
    [SerializeField] private Sprite[] borderSprite;


    #region 데이터 설정
    /// <summary>
    /// 캐릭터 슬롯 데이터 설정
    /// </summary>
    /// <param name="info"></param>
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
        // -> 인격 선택창 표시
    }

    /// <summary>
    /// 인격 선택창에서 클릭 (해당 인격 선택)
    /// </summary>
    public void IdentitySelect()
    {
        // -> 인격을 배치함 (1~12번)
    }

    /// <summary>
    /// 전투창에서 클릭 (인격의 편성 순서 선택)
    /// </summary>
    public void Organizing()
    {
        // -> 해당 인격을 장착함
    }
    #endregion
}
