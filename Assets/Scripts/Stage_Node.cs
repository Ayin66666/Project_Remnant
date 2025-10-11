using UnityEngine;
using UnityEngine.EventSystems;


public class Stage_Node : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("---Node Setting---")]
    [SerializeField] private StageData stageData;
    [SerializeField] private int chapterIndex;
    [SerializeField] private int stageIndex;


    [Header("---UI---")]
    [SerializeField] private GameObject outlineSet;


    public void OnPointerEnter(PointerEventData eventData)
    {
        // 아웃라인 On
        outlineSet.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 아웃라인 Off
        outlineSet.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 스테이지 상세 UI On
        UI_Manager.instance.StageSelectUI(stageData, true);
    }
}
