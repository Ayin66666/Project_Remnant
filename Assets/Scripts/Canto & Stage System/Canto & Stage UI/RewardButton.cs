using Game.Canto;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RewardButton : MonoBehaviour, IPointerClickHandler
{
    [Header("---Setting---")]
    [SerializeField] private int rewardIndex;
    [SerializeField] private bool canUsed;
    [SerializeField] private CantoManager canto;

    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private Image acquisitionIcon;
    [SerializeField] private TextMeshProUGUI rewardText;


    public void SetUp(CantoManager manager, RewardData data)
    {
        // Data
        canto = manager;
        rewardIndex = data.rewardIndex;

        // UI
        rewardText.text = $"{data.rewardSO.rewardValue}";
        switch (data.getReward)
        {
            case GetReward.Disabled:
                canUsed = false;
                break;
            case GetReward.Available:
                canUsed = true;
                break;
            case GetReward.Obtained:
                canUsed = false;
                acquisitionIcon.gameObject.SetActive(true);
                break;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canUsed) return;

        // 보상 요청
        canto.GetCantoReward(rewardIndex);
        
        // 슬롯 비활성화
        canUsed = false;
        acquisitionIcon.gameObject.SetActive(true);
    }
}



