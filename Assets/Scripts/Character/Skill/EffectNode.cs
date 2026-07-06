using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EffectNode
{
    [Header("---Effect Node---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private TargetType targetType;

    [Header("---Condition---")]
    [SerializeField] private CompareType compareType;
    [SerializeField] private int conditionValue;
    [SerializeField] private List<ValueNode> values;

    [Header("---Action---")]
    [SerializeField] private List<ActionNode> actions;

    #region Getter
    public TriggerType Trigger => triggerType;
    public TargetType Target => targetType;
    public CompareType Compare => compareType;
    public int ConditionValue => conditionValue;
    public List<ValueNode> Values => values;
    public List<ActionNode> Actions => actions;
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
    /// 조건 종류 & 조건 타입 노드
    /// </summary>>
    public struct ValueNode
    {
        [Header("---Value---")]
        public EffectBaseSO effect;
        public ValueType valueType;
    }

    [System.Serializable]
    /// <summary>
    /// 조건 만족 시 동작할 액션 타입 노드
    /// </summary>
    public struct ActionNode
    {
        [Header("---Public Action---")]
        public ActionType actionType;
        public ValueNode valueNode;
        public SinType sinType;
        public int actionValue;

        [Header("---Original Action---")]
        public int actionIndex;
        [TextArea] public string actionDescription;
    }
    #endregion
}

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
