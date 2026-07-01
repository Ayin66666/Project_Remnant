using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill_SkillName", menuName = "Skill/SkillSO", order = int.MaxValue)]
public class SkillSO : ScriptableObject
{
    [Header("---Skill Data---")]
    /// <summary>
    /// 죄악 타입
    /// </summary>
    public SinType sinType;
    /// <summary>
    /// 스킬 종류 (1 ~ 3스킬, 방어 스킬)
    /// </summary>
    public SkillType skillType;
    /// <summary>
    /// 참관타
    /// </summary>
    public AttackType attackType;
    /// <summary>
    /// 일반 & 강화스킬 여부
    /// </summary>
    public SkillVariantType skillVariantType;
    /// <summary>
    /// 공격 가중치 (1 ~ 9)
    /// </summary>
    public int targetCount;
    /// <summary>
    /// 동기화 별 스킬 데이터
    /// </summary>
    public List<SyncData> syncDatas;
    public enum SkillVariantType { Base = 0, Enhanced = 1 }

    [Header("---UI---")]
    /// <summary>
    /// 스킬 이름
    /// </summary>
    [SerializeField] private string skillName;
    /// <summary>
    /// 스킬 아이콘 - UI 및 전투 표시용
    /// </summary>
    [SerializeField] private Sprite icon;

    public string SkillName => skillName;
    public Sprite Icon => icon;


    #region 데이터 구조체
    [System.Serializable]
    /// <summary>
    /// 동기화 별 스킬 데이터 묶음
    /// </summary>
    public struct SyncData
    {
        [Header("---동기화 별 스킬 기본 데이터---")]
        /// <summary>
        /// 스킬의 기본 위력
        /// </summary>
        public int originalPower;
        /// <summary>
        /// 1코인 당 추가되는 위력 수치
        /// </summary>
        public int coinPower;
        /// <summary>
        /// CoinInfo 내의 effectNodes는 코인 별 효과, 
        /// 해당 List<EffectNode>는 사용 시 효과 전용
        /// </summary>
        public List<EffectNode> skillEffects;

        [Header("---코인 데이터---")]
        /// <summary>
        /// 코인 데이터 (벨류, 타격 횟수)
        /// </summary>
        public List<CoinInfoSO> coins;
    }
    #endregion
}
