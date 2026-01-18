using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChatacterSlotButton : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    [Header("---Setting---")]
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private float pressTime;
    [SerializeField] private bool isPress;
    private Coroutine pressCoroutine;
    private enum ButtonType { Order, Select }


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
            case ButtonType.Order: // 클릭 시간에 따른 분기
                if (pressCoroutine != null) StopCoroutine(pressCoroutine);
                pressCoroutine = StartCoroutine(PressChick());
                break;

            case ButtonType.Select: // 인격 리스트 표시
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
            // 상세정보
            Debug.Log("Long Press / 상세정보");
            // 상세 정보는 어디에서?
        }
        else
        {
            // 편성하기
            slot.Organizing();
        }
    }
}
