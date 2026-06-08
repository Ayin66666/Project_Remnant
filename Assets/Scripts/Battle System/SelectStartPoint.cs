using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectStartPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("---Setting---")]
    [SerializeField] private bool isSelecting;
    private Coroutine selectingCoroutine;

    [Header("---UI---")]
    [SerializeField] private RectTransform startRect;
    [SerializeField] private Image cursorImage;
    [SerializeField] private GameObject lineImage;
    [SerializeField] private List<GameObject> lineList;


    private IEnumerator Line()
    {
        isSelecting = true;
        GameObject line = Instantiate(lineImage, startRect);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        // 라인 그리기 로직
        while (isSelecting)
        {
            // 라인 계산
            Vector2 startPos = startRect.position;
            Vector2 mousePos = Input.mousePosition;
            Vector2 dir = mousePos - startPos;

            // 각도 조절
            lineRect.up = dir.normalized;

            // 길이 조절
            lineRect.sizeDelta = new Vector2(lineRect.sizeDelta.x, dir.magnitude);

            yield return null;
        }

        selectingCoroutine = null;
    }


    #region 마우스 이벤트
    public void OnPointerDown(PointerEventData eventData)
    {

        if (selectingCoroutine != null) StopCoroutine(selectingCoroutine);
        selectingCoroutine = StartCoroutine(Line());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isSelecting = false;
    }
    #endregion
}
