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
    public bool IsSelecting => isSelecting;

    [Header("---UI---")]
    [SerializeField] private RectTransform startRect;
    [SerializeField] private RectTransform curStartRect;
    [SerializeField] private Image cursorImage;
    [SerializeField] private GameObject lineImage;
    [SerializeField] private List<GameObject> lineList;


    private void Start()
    {
        curStartRect = startRect;
    }

    #region 선택 로직
    private IEnumerator Line()
    {
        isSelecting = true;
        GameObject line = Instantiate(lineImage, curStartRect);
        RectTransform lineRect = line.GetComponent<RectTransform>();

        // 라인 그리기 로직
        while (isSelecting)
        {
            // 라인 계산
            Vector2 startPos = curStartRect.position;
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
    
    /// <summary>
    /// 스킬 선택 로직 (호출 = selectNodeGroup 의 마우스 이벤트)
    /// </summary>
    /// <param name="group"></param>
    /// <param name="node"></param>
    public void Select(SelectNodeGroup group, SkillSelectNode node)
    {
        // 선택 모드가 아니라면 무시
        if (!isSelecting) 
            return;

        // 선택된 데이터 저장
        Debug.Log($"스킬 선택됨! {group} / {node}");
        SkillSelectManager.instance.SkillSelect(group, node);

        // 라인 고정


        // 스타팅 포인트 변경
        curStartRect = node.GetComponent<RectTransform>();
        isSelecting = false;

        // 라인 그리기 재시작
        if(selectingCoroutine != null) StopCoroutine(selectingCoroutine);
        selectingCoroutine = StartCoroutine(Line());
    }

    /// <summary>
    /// 선택 해제 로직
    /// </summary>
    public void DeSelect()
    {

    }
    #endregion


    #region 마우스 이벤트
    public void OnPointerDown(PointerEventData eventData)
    {
        // 라인 그리기 이벤트 시작 (스킬 선택)
        if (selectingCoroutine != null) StopCoroutine(selectingCoroutine);
        selectingCoroutine = StartCoroutine(Line());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 라인 그리기 종료 (스킬 선택 종료)
        isSelecting = false;
    }
    #endregion
}
