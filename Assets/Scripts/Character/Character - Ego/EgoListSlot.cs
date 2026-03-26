using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Character;


public class EgoListSlot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("---Setting---")]
    [SerializeField] private EgoData egoData;
    private Coroutine pressCoroutine;
    private bool isPressed;


    [Header("---UI---")]
    [SerializeField] private TextMeshProUGUI egoName;
    [SerializeField] private Image egoImage;
    [SerializeField] private Image syncIcon;
    [SerializeField] private Image rankIcon;
    [SerializeField] private Sprite[] syncSprite;
    [SerializeField] private Sprite[] rankSprite;


    /// <summary>
    /// 슬롯 세팅
    /// </summary>
    /// <param name="info"></param>
    public void SetUp(EgoData info)
    {
        egoData = info;
        egoName.text = info.master.egoName;
        egoImage.sprite = info.master.egoIcon;
        syncIcon.sprite = syncSprite[info.sync];
        rankIcon.sprite = rankSprite[(int)info.master.egoRank];
    }

    /// <summary>
    /// 슬롯 초기화
    /// </summary>
    public void Clear()
    {
        egoData = null;

        egoName.text = "";
        egoImage.sprite = null;
        syncIcon.sprite = null;
        rankIcon.sprite = null;
    }


    #region Press Evnet
    public void OnPointerDown(PointerEventData eventData)
    {
        if(pressCoroutine != null) StopCoroutine(pressCoroutine);
        pressCoroutine = StartCoroutine(PressTimer());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
    
    public IEnumerator PressTimer()
    {
        isPressed = true;
        float timer = 0;

        // 입력 시간 체크
        while(timer < 1 && isPressed)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if(timer >= 1)
        {
            // 에고 상세 정보 표시
            // ...
        }
        else
        {
            // 에고 장착
            CharacterRosterManager.instance.SetEgo(egoData);
        }
    }
    #endregion
}
