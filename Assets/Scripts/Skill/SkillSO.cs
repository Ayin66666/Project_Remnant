using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill_SkillName", menuName = "Skill/SkillSO", order = int.MaxValue)]
public class SkillSO : ScriptableObject
{
    [Header("---Type---")]
    public SkillType skillType;
    public SkillVariantType skillVariantType;
    public enum SkillType { Skill1, Skill2, Skil3, Guard, Attack };
    public enum SkillVariantType { Base = 0, Enhanced = 1 }


    [Header("---Skill Data---")]
    public AttackType attackType;
    public Crime crimeType;
    public int coinPower;
    public List<CoinInfo> coins;
    public SkillUI ui;

    [Header("---Action---")]
    public SkillBase skill;


    [System.Serializable]
    public struct CoinInfo
    {
        public float value;
        public int attackCount;
    }

    [System.Serializable]
    public struct SkillUI
    {
        public Sprite icon;
        public string skillName;
        [TextArea] public string skillDescription;
    }
}
