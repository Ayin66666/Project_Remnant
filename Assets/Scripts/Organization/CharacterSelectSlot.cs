using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Character;

public class CharacterSelectSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("---Data---")]
    [SerializeField] private IdentityData identityInfo;
    [SerializeField] private bool isSelected;

    [Header("---UI---")]
    [SerializeField] private Image characterImage;
    [SerializeField] private Image border;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject selectTextSet;
    [SerializeField] private Sprite[] borderSprite;
    [SerializeField] private GameObject[] rankIcon;


    /// <summary>
    /// 슬롯 데이터 셋업
    /// </summary>
    /// <param name="info"></param>
    public void SetUp(IdentityData info, bool isSelected)
    {
        if(isSelected)
        {
            isSelected = true;
            selectTextSet.SetActive(true);
        }

        // UI 셋팅
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
    /// 인격 선택하기
    /// </summary>
    public void Select()
    {
        if(identityInfo != null)
        {
            isSelected = true;
            selectTextSet.SetActive(true);
            CharacterRosterManager.instance.SetIdentity(identityInfo);
        }
        else
        {
            Debug.Log($"선택 슬롯 오류 발생 / 인격 데이터 없음 / {identityInfo}");
        }
    }

    /// <summary>
    /// 인격 선택 해제 (이쪽은 UI만 해제함!)
    /// </summary>
    public void Deselect()
    {
        if(isSelected)
        {
            isSelected = false;
            selectTextSet.SetActive(false);
        }
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        characterImage.sprite = null;
        border.sprite = borderSprite[0];
        levelText.text = "";
        nameText.text = "";
        isSelected = false;
        selectTextSet.SetActive(false);
        foreach (GameObject icon in rankIcon)
        {
            icon.SetActive(false);
        }
    }


    #region 마우스 이벤트
    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
            selectTextSet.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
            selectTextSet.SetActive(false);
    }
    #endregion
}
