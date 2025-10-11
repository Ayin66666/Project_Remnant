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
        // �ƿ����� On
        outlineSet.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // �ƿ����� Off
        outlineSet.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // �������� �� UI On
        UI_Manager.instance.StageSelectUI(stageData, true);
    }
}
