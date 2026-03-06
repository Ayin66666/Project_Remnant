using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Game.Stage;


public class StageNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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
        // 원인 모를 데이터 이슈 발생 - 디버그 찍어보면 data 자체는 제대로 들어오는데,
        // 아래 이미지 삽입에서 null 이슈
        // 문제는 so에도 데이터 입력 문제 없는듯
        // 만약 런타임 데이터 가공에서 이슈가 있었다면 so가 null이었을 것

        this.data = data;

        // 스테이지 UI 데이터 삽입
        stageImage.sprite = data.stageSO.StageSprite;
        stageName.text = data.stageSO.StageName;
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
        }

    }
    #endregion
}
