using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EgoContainerUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private EgoData ego;
    public EgoData Ego { get { return ego; } }

    [Header("---UI---")]
    [SerializeField] private Image egoIcon;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI egoNameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI targetCountText;
    [SerializeField] private TextMeshProUGUI descriptionText;


    /// <summary>
    /// 데이터 수령 후 UI 설정
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="skill"></param>
    public void SetUp(IdentityData identity, EgoData ego)
    {
        // 초기화
        Clear();

        // 데이터 설정
        this.ego = ego;

        // 아이콘
        egoIcon.sprite = ego.master.egoIcon;

        // 코인 위력
        coinPowerText.text = $"+ {ego.master.coinPower}";

        // 코인 개수
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < ego.master.coins.Count; i++)
        {
            sb.Append("<sprite=1>");
        }
        coinText.text = sb.ToString();

        // 스킬 이름
        egoNameText.text = ego.master.egoName;

        // 데미지
        sb = new StringBuilder();
        for (int i = 0; i < ego.master.coins.Count; i++)
        {
            sb.Append(ego.master.coins[i].value);
            if (i < ego.master.coins.Count - 1) sb.Append(" / ");
        }
        damageText.text = sb.ToString();

        // 가중치
        sb = new StringBuilder(ego.master.targetCount);
        for (int i = 0; i < ego.master.targetCount; i++)
        {
            sb.Append("<sprite=1>");
        }
        targetCountText.text = sb.ToString();

        // 설명
        descriptionText.text = ego.master.egoDescription;

        // 비용 매우 비싸니 주의! - 절대 1회 이상 호출되는 경우가 없도록 할것!
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    /// <summary>
    /// UI 초기화
    /// </summary>
    public void Clear()
    {
        egoIcon.sprite = null;
        coinPowerText.text = string.Empty;
        coinText.text = string.Empty;
        egoNameText.text = string.Empty;
        damageText.text = string.Empty;
        descriptionText.text = string.Empty;
        targetCountText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}
