using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill_SkillName", menuName = "Skill/SkillSO", order = int.MaxValue)]
public class SkillSO : ScriptableObject
{
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
