using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class Skill
{
    public Player_Base.SkillType Type;
    public Skill_Base skill;
    public int skillCount;
}


public abstract class Player_Base : Character_Base
{
    [Header("---Skill---")]
    [SerializeField] private List<Skill> normalSkill;
    [SerializeField] private Skill defenceSkill;
    [SerializeField] private Dictionary<EGOType, Skill_Base> egoSkill;

    public enum SkillType { Skill1, Skill2, Skill3, Defence, EGO };
    public enum EGOType { Zayin, TETH, HE, WAW, ALEPH };


    /// <summary>
    /// �� ���� �� ���� �ӵ� & ��ų ����
    /// </summary>
    public void Player_Setting()
    {
        Speed_Setting();
        EgoData_Setting();
    }

    /// <summary>
    /// ���� ���� �� ��â ���� ������ ��������
    /// </summary>
    public void EgoData_Setting()
    {

    }

    /// <summary>
    /// ���� �ӵ� ����
    /// </summary>
    public void Speed_Setting()
    {

    }

    /// <summary>
    /// ���� ����ġ ���� ��ų ����
    /// </summary>
    public void Skill_Setting()
    {
        int totalWeight = normalSkill.Sum(s => s.skillCount);
        int randomValue = Random.Range(0, totalWeight);

        foreach (Skill skill in normalSkill)
        {
            if (randomValue < skill.skillCount)
            {
                // return skill.skill; -> �� ��ų�� ����Ʈ�� �߰�
            }
                
            randomValue -= skill.skillCount;
        }
    }
}
