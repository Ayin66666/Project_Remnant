using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SkillContainerUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private SkillSO skill;
    public SkillSO Skill {  get { return skill; } }

    [Header("---UI---")]
    [SerializeField] private Image skillIcon;
    [SerializeField] private TextMeshProUGUI coinPowerText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI skillNameText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI targetCountText;
    [SerializeField] private TextMeshProUGUI descriptionText;

    /// <summary>
    /// 데이터 수령 후 UI 설정
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="skill"></param>
    public void SetUp(IdentityData identity, SkillSO skill)
    {
        // 초기화
        Clear();
        
        // 데이터 설정
        this.skill = skill;

        // 아이콘
        skillIcon.sprite = skill.ui.icon;

        // 코인 위력
        coinPowerText.text = $"+ {skill.coinPower}";

        // 코인 개수
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < skill.coins.Count; i++)
        {
            sb.Append("<sprite=1>");
        }
        coinText.text = sb.ToString();

        // 스킬 이름
        skillNameText.text = skill.ui.skillName;

        // 데미지
        sb = new StringBuilder();
        for(int i = 0; i < skill.coins.Count; i++)
        {
            sb.Append(skill.coins[i].value);
            if (i < skill.coins.Count - 1) sb.Append(" / ");
        }
        damageText.text = sb.ToString();

        // 가중치
        sb = new StringBuilder(skill.targetCount);
        for(int i = 0; i < skill.targetCount; i++)
        {
            sb.Append("<sprite=1>");
        }
        targetCountText.text = sb.ToString();

        // 설명
        descriptionText.text = skill.ui.skillDescription;

        // 비용 매우 비싸니 주의! - 절대 1회 이상 호출되는 경우가 없도록 할것!
        LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
    }

    /// <summary>
    /// UI 초기화
    /// </summary>
    public void Clear()
    {
        skillIcon.sprite = null;
        coinPowerText.text = string.Empty;
        coinText.text = string.Empty;
        skillNameText.text = string.Empty;
        damageText.text = string.Empty;
        descriptionText.text = string.Empty;
        targetCountText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}
