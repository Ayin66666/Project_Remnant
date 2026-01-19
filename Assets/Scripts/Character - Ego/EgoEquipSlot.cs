using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class EgoEquipSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    // 소형 & 대형의 요구 데이터가 달라 분리하기로 결정

    [Header("---Setting---")]
    [SerializeField] private CharacterId sinner;
    [SerializeField] private Rank slotRank;
    [SerializeField] private EgoData equipEgoData;

    // Press Setting
    private float timer;
    private bool isPressed = false;
    private Coroutine pressCoroutine;


    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image egoImage;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Image syncIcon;


    [Header("---Rank & Sync Image---")]
    [SerializeField] private Sprite[] tierSprites;
    [SerializeField] private Sprite[] syncSprites;


    /// <summary>
    /// 캐릭터 리스트 창 오픈 시 호출되는 로직 - 편성 데이터가 있다면 info 입력, 아니면 null로 입력할 것!
    /// </summary>
    /// <param name="info"></param>
    public void LoadEquipEgo(Rank slotRank, CharacterId sinner, EgoData info)
    {
        this.sinner = sinner;
        this.slotRank = slotRank;
        
        // 로드 데이터가 있다면
        if(info != null)
        {
            equipEgoData = info;
            nameText.text = info.master.egoName;
            egoImage.sprite = info.master.egoSprite;
            egoImage.color = new Color(1, 1, 1, 1);
            rankIcon.sprite = tierSprites[(int)info.master.egoRank];
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
        egoImage.sprite = info.master.egoSprite;
        egoImage.color = new Color(1, 1, 1, 1);
        rankIcon.sprite = tierSprites[(int)info.master.egoRank];
        syncIcon.sprite = syncSprites[info.sync];
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        sinner = CharacterId.None;

        equipEgoData = null;
        nameText.text = "";
        egoImage.sprite = null;
        egoImage.color = new Color(1, 1, 1, 0);
        rankIcon.sprite = tierSprites[0];
        syncIcon.sprite = syncSprites[0];
    }


    #region Press Event
    public void OnPointerDown(PointerEventData eventData)
    {
        if (pressCoroutine != null) StopCoroutine(pressCoroutine);
        pressCoroutine = StartCoroutine(PressTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

    private IEnumerator PressTimer()
    {
        isPressed = true;
        timer = 0;

        // 타이머
        while (timer < 1f && isPressed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 입력 시간 체크
        if(timer >= 1f)
        {
            // 1초동안 꾹 누른다면 상세 정보 UI
            // ...
        }
        else
        {
            OrganizationManager.instance.OpenEgoList(sinner, slotRank);
        }
    }
    #endregion
}




