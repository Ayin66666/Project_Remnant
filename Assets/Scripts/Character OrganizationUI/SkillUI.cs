using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class SkillUI : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] private List<SkillContainerUI> skill;

    [Header("---UI---")]
    [SerializeField] private GameObject passiveUI;
    [SerializeField] private TextMeshProUGUI passiveText;

    [Header("---Prefab---")]
    [SerializeField] private GameObject skillContainerUIPrefab;


    /// <summary>
    /// 인격 데이터를 받아서 스킬 개수만큼 배치
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(IdentityData data)
    {
        // 스킬 UI

        // 패시브 UI
    }

    #region Button Event
    /// <summary>
    /// 스킬 1~3 & 가드
    /// </summary>
    /// <param name="index"></param>
    public void ClickSkillButton(int skillIndex)
    {
        passiveUI.SetActive(false);

        SkillType selectedType = (SkillType)skillIndex;
        foreach (SkillContainerUI sk in skill)
        {
            if (selectedType == sk.Skill.skillType) 
                sk.gameObject.SetActive(true);
            else 
                sk.gameObject.SetActive(false);
        }
    }

    public void ClickPassive()
    {
        passiveUI.SetActive(true);
    }
    #endregion
}
