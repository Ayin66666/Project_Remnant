using UnityEngine;


public abstract class Skill_Base : MonoBehaviour
{
    [Header("---Data---")]
    public string egoName;
    public int id;


    [Header("---Value---")]
    public SkillType skillType;
    public SkillData_SO skillData;
    public enum SkillType { Guard, Dodge, Counter, Skill, Skill3, Ego };


    public abstract void Use();
    public abstract void Skill_Reset();
}
