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
    /// 턴 시작 시 공격 속도 & 스킬 설정
    /// </summary>
    public void Player_Setting()
    {
        Speed_Setting();
        EgoData_Setting();
    }

    /// <summary>
    /// 게임 시작 시 편성창 에고 데이터 가져오기
    /// </summary>
    public void EgoData_Setting()
    {

    }

    /// <summary>
    /// 공격 속도 설정
    /// </summary>
    public void Speed_Setting()
    {

    }

    /// <summary>
    /// 매턴 가중치 랜덤 스킬 선택
    /// </summary>
    public void Skill_Setting()
    {
        int totalWeight = normalSkill.Sum(s => s.skillCount);
        int randomValue = Random.Range(0, totalWeight);

        foreach (Skill skill in normalSkill)
        {
            if (randomValue < skill.skillCount)
            {
                // return skill.skill; -> 이 스킬을 리스트에 추가
            }
                
            randomValue -= skill.skillCount;
        }
    }
}
