using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Description_Slot : MonoBehaviour
{
    [Header("---Setting---")]
    [SerializeField] Player_Base.SkillType type;
    [SerializeField] private Skill_Base skill;


    [Header("---UI---")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI descriptionText;


    /// <summary>
    /// ½½·Ô ¼³Á¤
    /// </summary>
    /// <param name="type"></param>
    /// <param name="skill"></param>
    public void Setting(Player_Base.SkillType type, Skill_Base skill)
    {
        this.type = type;
        this.skill = skill;

        icon.sprite = skill.skillData.icon;
        descriptionText.text = skill.skillData.description;
    }
}
