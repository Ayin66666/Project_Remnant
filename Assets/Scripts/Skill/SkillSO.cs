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
    /// <summary>
    /// 참관타
    /// </summary>
    public AttackType attackType;
    /// <summary>
    /// 죄악 타입
    /// </summary>
    public Crime crimeType;
    /// <summary>
    /// 1코인 당 추가되는 위력 수치
    /// </summary>
    public int coinPower;
    /// <summary>
    /// 공격 가중치 (1 ~ 9)
    /// </summary>
    public int targetCount;
    /// <summary>
    /// 코인 데이터 (벨류, 타격 횟수)
    /// </summary>
    public List<CoinInfo> coins;
    /// <summary>
    /// UI 데이터
    /// </summary>
    public SkillUI ui;

    [Header("---Action---")]
    /// <summary>
    /// 애니메이션 & 애니메이션 이벤트 동작
    /// </summary>
    public SkillBase skill;


    [System.Serializable]
    public struct CoinInfo
    {
        /// <summary>
        /// 스킬 배율
        /// </summary>
        public float value;
        /// <summary>
        /// 타격 횟수 (데미지 / hitCount)
        /// </summary>
        public int hitCount;
    }

    [System.Serializable]
    public struct SkillUI
    {
        public Sprite icon;
        public string skillName;
        [TextArea] public string skillDescription;
    }
}
