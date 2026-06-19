using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Character;


public class DescriptionUI : MonoBehaviour
{
    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI targetCountText;
    [SerializeField] private TextMeshProUGUI descriptionText;


    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void Clear()
    {
        icon.sprite = null;
        nameText.text = string.Empty;
        coinText.text = string.Empty;
        valueText.text = string.Empty;
        targetCountText.text = string.Empty;
        descriptionText.text = string.Empty;
    }

    /// <summary>
    /// 스킬 셋업 - SkillSO
    /// </summary>
    /// <param name="skillSO"></param>
    public void SkillSetUp(IdentityData identity, SkillSO skillSO)
    {
        Clear();

        // UI 세팅
        icon.sprite = skillSO.icon;
        nameText.text = skillSO.skillName;
        descriptionText.text = SkillDescriptionBuilder.MakeDescription(skillSO, identity.sync);

        // 코인 개수
        StringBuilder sb = new StringBuilder(skillSO.syncDatas[0].coins.Count);
        for (int i = 0; i < skillSO.syncDatas[0].coins.Count; i++)
        {
            sb.Append("<sprite=0>");
        }
        coinText.text = sb.ToString();

        // 위력
        sb = new StringBuilder(skillSO.syncDatas[0].coins.Count * 6);
        for (int i = 0; i < skillSO.syncDatas[0].coins.Count; i++)
        {
            Vector2 value = skillSO.syncDatas[0].coins[i].motionValue;
            sb.Append(value.ToString("0.0"));
            if (i < skillSO.syncDatas[0].coins.Count - 1) sb.Append(" / ");
        }
        valueText.text = sb.ToString();

        // 가중치
        sb = new StringBuilder(skillSO.targetCount);
        for (int i = 0; i < skillSO.targetCount; i++)
        {
            sb.Append("<sprite=0>");
        }
        targetCountText.text = sb.ToString();
    }

    /// <summary>
    /// 에고 셋업 - EgoData
    /// </summary>
    /// <param name="ego"></param>
    public void EgoSetUp(EgoData ego)
    {
        Clear();

        // UI 세팅
        icon.sprite = ego.master.egoIcon;
        nameText.text = ego.master.egoName;
        descriptionText.text = ego.master.egoDescription;

        // 코인 개수
        StringBuilder sb = new StringBuilder(ego.master.coins.Count);
        for (int i = 0; i < ego.master.coins.Count; i++)
        {
            sb.Append("<sprite=0>");
        }
        coinText.text = sb.ToString();

        // 위력
        sb = new StringBuilder(ego.master.coins.Count * 6);
        for (int i = 0; i < ego.master.coins.Count; i++)
        {
            sb.Append(ego.master.coins[i].value.ToString("0.0"));
            if (i < ego.master.coins.Count - 1) sb.Append(" / ");
        }
        valueText.text = sb.ToString();

        // 가중치
        sb = new StringBuilder(ego.master.targetCount);
        for (int i = 0; i < ego.master.targetCount; i++)
        {
            sb.Append("<sprite=0>");
        }
        targetCountText.text = sb.ToString();
    }
}
