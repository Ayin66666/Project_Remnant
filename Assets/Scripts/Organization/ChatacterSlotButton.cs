using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatacterSlotButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    // 1. 편성 리스트에서 1회 클릭 시 = 편성순서
    // 2. 인격 리스트에서 1회 클릭 시 = 선택하기
    // 3. 공통 기능 = 꾹 누르면 = 상세정보


    [Header("---Setting---")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float pressTime;
    [SerializeField] private bool isPress;
    private Coroutine pressCoroutine;
    private enum ButtonType { OrderButton, ListButton }


    [Header("---UI---")]
    [SerializeField] private GameObject uiText;
    [SerializeField] private CharacterSlot slot;


    public void OnPointerEnter(PointerEventData eventData)
    {
        uiText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiText.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        switch (buttonType)
        { 
            // 클릭 시간에 따른 분기 ( 1초 미만 = 순서 / 1초 이상 = 상세 정보 )
            case ButtonType.OrderButton:
                if (pressCoroutine != null) StopCoroutine(pressCoroutine);
                pressCoroutine = StartCoroutine(PressChick());
                break;

            // 인격 리스트 표시
            case ButtonType.ListButton: 
                slot.ShowIdentityList();
                break;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }


    /// <summary>
    /// 해당 기능은 슬롯 타입이 
    /// </summary>
    /// <returns></returns>
    private IEnumerator PressChick()
    {
        isPress = true;

        // 타이머 동작
        float timer = 0;
        while (isPress && timer < pressTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 타이머에 따른 분기
        if (timer >= pressTime)
        {
            // 상세 정보
            slot.ShowIdentityUI();
        }
        else
        {
            // 순서 편성
            slot.OrderSetting();
        }
    }
}
