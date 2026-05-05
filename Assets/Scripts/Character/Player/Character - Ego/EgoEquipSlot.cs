using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Character;


public class EgoEquipSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 소형 & 대형의 요구 데이터가 달라 분리하기로 결정

    [Header("---Setting---")]
    [SerializeField] private Rank slotRank;
    [SerializeField] private EgoData equipEgoData;
    public Rank SlotRank => slotRank;

    // Press Setting
    private float timer;
    private bool isPressed = false;
    private Coroutine pressCoroutine;


    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image egoImage;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Image syncIcon;
    [SerializeField] private Image progressBar;
    [SerializeField] private Image backgroundImage;


    [Header("---Rank & Sync Image---")]
    [SerializeField] private Sprite[] rankSprites;
    [SerializeField] private Sprite[] syncSprites;

    #region 기능
    /// <summary>
    /// 캐릭터 리스트 창 오픈 시 호출되는 로직 - 편성 데이터가 있다면 info 입력, 아니면 null로 입력할 것!
    /// </summary>
    /// <param name="info"></param>
    public void LoadEquipEgo(EgoData info)
    {
        // 로드 데이터가 있다면
        if (info != null)
        {
            equipEgoData = info;
            nameText.text = info.master.egoName;
            egoImage.sprite = info.master.egoIcon;
            egoImage.color = new Color(1, 1, 1, 1);
            rankIcon.sprite = rankSprites[(int)info.master.egoRank];
            syncIcon.sprite = syncSprites[info.sync];
        }
    }

    /// <summary>
    /// 장착된 에고 변경
    /// </summary>
    /// <param name="info"></param>
    public void ChangeEquipEgo(EgoData info)
    {
        equipEgoData = info;
        nameText.text = info.master.egoName;
        egoImage.sprite = info.master.egoIcon;
        egoImage.color = new Color(1, 1, 1, 1);
        rankIcon.sprite = rankSprites[(int)info.master.egoRank];
        syncIcon.sprite = syncSprites[info.sync];
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        equipEgoData = null;
        nameText.text = "";
        egoImage.sprite = null;
        egoImage.color = new Color(1, 1, 1, 0);
        rankIcon.sprite = rankSprites[0];
        syncIcon.sprite = syncSprites[0];
    }
    #endregion


    #region MouseOver Event
    public void OnPointerEnter(PointerEventData eventData)
    {
        backgroundImage.color = new Color(0.7f, 0.7f, 0.7f, 1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        backgroundImage.color = new Color(1f, 1f, 1f, 1f);
    }
    #endregion


    #region Press Event
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressCoroutine != null) StopCoroutine(pressCoroutine);
        pressCoroutine = StartCoroutine(PressTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        progressBar.fillAmount = 0;
    }

    private IEnumerator PressTimer()
    {
        isPressed = true;
        timer = 0;
        progressBar.fillAmount = 0;
        // 타이머
        while (timer < 1f && isPressed)
        {
            timer += Time.deltaTime;
            progressBar.fillAmount = Mathf.Clamp(timer, 0, 1);
            yield return null;
        }
        progressBar.fillAmount = 0;

        // 입력 시간 체크
        if (timer >= 1f)
        {
            // 1초동안 꾹 누른다면 상세 정보 UI
            // ...
            Debug.Log("Click - Over 1");
        }
        else
        {
            OrganizationManager.instance.OpenEgoList(slotRank);
        }
    }
    #endregion
}




