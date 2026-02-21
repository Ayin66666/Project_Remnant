using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class StageSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("---Setting---")]
    [SerializeField] private StageData data;

    [Header("---UI---")]
    [SerializeField] private Image stageImage;
    [SerializeField] private Image clearImage;
    [SerializeField] private Image borderImage;
    [SerializeField] private TextMeshProUGUI stageName;


    public void SetUp(StageData data)
    {
        Reset();

        this.data = data;

        stageImage.sprite = data.stageSprite;
        stageName.text = data.stageName;
        switch (data.stageClearType)
        {
            case StageClearType.None:
                clearImage.color = Color.black;
                break;

            case StageClearType.Clear:
                clearImage.color = Color.red;
                break;

            case StageClearType.ExClear:
                clearImage.color = Color.green;
                break;
        }
    }

    public void Reset()
    {
        stageImage.sprite = null;
        stageName.text = string.Empty;
        clearImage.color = Color.white;
        borderImage.color = Color.white;
    }


    #region Mouse Event
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (data.canEnter)
            borderImage.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (data.canEnter)
            borderImage.color = Color.white;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (data.canEnter)
        {
            // 스테이지 설명 UI 표시
            CantoManager.instance.StageUI(data);
        }

    }
    #endregion
}
