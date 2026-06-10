using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SelectStartPoint : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("---UI---")]
    [SerializeField] private RectTransform startRect;
    [SerializeField] private Image cursorImage;
    [SerializeField] private GameObject lineImage;
    [SerializeField] private List<GameObject> lineList;



    #region 마우스 이벤트
    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }
    #endregion
}
