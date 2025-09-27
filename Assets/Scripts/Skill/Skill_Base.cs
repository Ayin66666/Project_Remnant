using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Skill_Base : MonoBehaviour
{
    [Header("---Value---")]
    public SkillType skillType;
    public SkillData_SO skillData;
    public enum SkillType { Guard, Dodge, Counter, Skill, Skill3, Ego };


    public abstract void Use();
}
