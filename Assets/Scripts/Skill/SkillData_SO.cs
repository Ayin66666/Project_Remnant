using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill Data", menuName = "Skill/Skill Data", order = int.MaxValue)]
public class SkillData_SO : ScriptableObject
{
    [Header("---Value---")]
    [SerializeField] private List<SkillData> skillData;
    public List<SkillData> SkillDatas { get { return skillData; } private set { skillData = value; } }


    [System.Serializable]
    public class SkillData
    {
        [Header("---UI---")]
        public Sprite icon;
        [TextArea] public string description;


        [Header("---Value---")]
        public List<Value> value;
    }

    [System.Serializable]
    public class Value
    {
        public int coinCount;
        public Vector2 coinValue;
        public int attackCount;
        public List<Effect> effect;
    }

    [System.Serializable]
    public class Effect
    {
        public Hit_Effect effect;
        public int value;
    }
}
