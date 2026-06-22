using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skill_SkillName", menuName = "Skill/SkillSO", order = int.MaxValue)]
public class SkillSO : ScriptableObject
{
    [Header("---Skill Data---")]
    /// <summary>
    /// 죄악 타입
    /// </summary>
    public Sin crimeType;
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
    public string skillName;
    /// <summary>
    /// 스킬 아이콘 - UI 및 전투 표시용
    /// </summary>
    public Sprite icon;


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
        public List<CoinInfo> coins;
    }

    [System.Serializable]
    /// <summary>
    /// 코인의 앞 & 뒷면 데미지 value, 공격 횟수, 공격 당 데미지 배율 데이터
    /// </summary>
    public struct CoinInfo
    {
        [Header("---데미지 배율 & 타격 분할---")]
        /// <summary>
        /// 스킬 배율 (앞면 = X / 뒷면 = Y)
        /// </summary>
        public Vector2 motionValue;
        /// <summary>
        /// (value x 공격 레벨?)로 계산된 데미지를 기반으로 총 데미지 계산
        /// + 총 데미지를 attackEffect의 damagePercent로 나눠서 각 타격마다 데미지 부여
        /// </summary>
        public List<HitInfo> hitDatas;

        [Header("---발동 효과---")]
        /// <summary>
        /// 사용시, 적중시 같은 효과 발동 조건 데이터가 담긴 so
        /// </summary>
        public List<EffectNode> effectNodes;
    }

    [System.Serializable]
    /// <summary>
    /// 타격 횟수, 총 데미지 기준 비율(%) 데이터
    /// </summary>
    public struct HitInfo
    {
        [Header("---Hit Info---")]
        public int hitCount;
        public float damagePercent;
    }
    #endregion
}
