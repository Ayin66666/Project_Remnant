using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StatusData", menuName = "Identity/Status", order = int.MaxValue)]
public class StatusDataSO : ScriptableObject
{
    // 스테이터스 종류
    // 1. 기초 능력치 (체력, 그로기, 기초공, 기초방, 속도)
    // 2. 레벨업 상승 능력치 (1렙당 체력증, 1렙당 공증, 1렙당 방증)
    // 3. 동기화 상승 능력치 (동기화 별 증가 스테이터스 수치)

    [Header("---Status---")]
    [SerializeField] private int baseHp;
    [SerializeField] private int baseAttackPoint;
    [SerializeField] private int baseDefencePoint;
    [SerializeField] private List<float> attackResistance;
    [SerializeField] private List<int> groggy;
    [SerializeField] private GrowthFactor growthFactor;
    [SerializeField] private LevelUpStat levelUpStat;
    [SerializeField] private List<SyncUpStat> syncUpData;


    /// <summary>
    /// 성장 보정치 - 탱커, 딜러, 서포터에 따른 능력 성장 보정용!
    /// </summary>
    [System.Serializable]
    public struct GrowthFactor
    {
        public float hpFactor;
        public float attackFactor;
        public float defenceFactor;
    }

    /// <summary>
    /// 레벨업 당 스테이터스 증가 수치
    /// </summary>
    [System.Serializable]
    public struct LevelUpStat
    {
        public int hp;
        public int attack;
        public int defence;
    }

    /// <summary>
    /// 동기화 당 스테이터스 증가 수치
    /// </summary>
    [System.Serializable]
    public struct SyncUpStat
    {
        public int hp;
        public int attack;
        public int defence;
        public Vector2Int attackSpeed;
    }

    public int BaseHp { get { return baseHp; } set { baseHp = value; } }
    public List<int> Groggy { get { return groggy; } set { groggy = value; } }
    public int BaseAttackPoint { get { return baseAttackPoint; } set { baseAttackPoint = value; } }
    public int BaseDefencePoint { get { return baseDefencePoint; } set { baseDefencePoint = value; } }
    public List<float> AttackResistance { get { return attackResistance; } set { attackResistance = value; } }
    public LevelUpStat LevelUpData { get { return levelUpStat; } set { levelUpStat = value; } }
    public List<SyncUpStat> SyncUpData { get { return syncUpData; } set { syncUpData = value; } }
    public GrowthFactor GrowthFactorData { get { return growthFactor; } set { growthFactor = value; } }
}
