using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EffectNode
{
    [Header("---Trigger---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private TargetType targetType;

    [Header("---Condition---")]
    [SerializeField] private ConditionNode condition;

    [Header("---Action---")]
    [SerializeField] private ActionNode action;

    #region Getter
    public TriggerType Trigger => triggerType;
    public TargetType Target => targetType;
    public ConditionNode Condition => condition;
    public ActionNode Action => action;
    #endregion

    #region Enum
    public enum TargetType
    {
        Self,
        Target,
        Both,
        AllEnemies,
        AllAllies,
        Everyone
    }

    public enum CompareType
    {
        None,
        LessEqual,
        Equal,
        GreaterEqual,
    }
    #endregion

    #region 노드 구조체
    [System.Serializable]
    /// <summary>
    /// 값 저장 노드
    /// </summary>>
    public struct EffectValue
    {
        [Header("---Value---")]
        public EffectBaseSO effect;
        public ValueType valueType;
        public int value;
    }

    [System.Serializable]
    /// <summary>
    /// 동작 조건 노드
    /// </summary>
    public struct ConditionNode
    {
        [Header("---Condition---")]
        public CompareType compareType;
        public int conditionValue;
        public List<EffectValue> values;
    }

    [System.Serializable]
    /// <summary>
    /// 동작 액션 종류
    /// </summary>
    public struct ActionNode
    {
        [Header("---Public Action---")]
        public ActionType actionType;
        public List<EffectValue> valueNode;

        [Header("---Original Action---")]
        public int actionIndex;
        [TextArea] public string actionDescription;
    }
    #endregion
}


#region Public Enum -> 나중에 위치 전환 필요
/// <summary>
/// 기능의 동작 타이밍 Enum
/// </summary>
[System.Flags]
public enum TriggerType
{
    None = 0,

    // 스킬 사용
    SkillUse = 1 << 0,
    Attack = 1 << 1,
    Hit = 1 << 2,
    SkillEnd = 1 << 3,

    // 합
    ClashWin = 1 << 4,
    ClashLose = 1 << 5,

    // 턴
    TurnStart = 1 << 6,
    TurnEnd = 1 << 7
}

public enum ActionType
{
    // 버프, 디버프 추가 & 제거
    AddEffect,
    RemoveEffect,

    // 회복
    HealHp,
    Shield,

    // 데미지
    Damage,
    DamageRatio,

    // 데미지 증가
    DamageMultiplier,
    CriticalMultiplier,

    // 재사용
    ReuseCoin,
    ReuseSkill,

    // 오리지널 효과
    Original
}

public enum ValueType
{
    Power,
    Count
}
#endregion
