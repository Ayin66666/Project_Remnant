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
    [SerializeField] private ActionNode action;

    #region Getter
    public TriggerType Trigger => triggerType;
    public TargetType Target => targetType;
    public CompareType Compare => compareType;
    public int ConditionValue => conditionValue;
    public List<ValueNode> Values => values;
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
        LessEqual,
        Equal,
        GreaterEqual,
    }

    public enum ValueType
    {
        Power,
        Count
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
        public Sin sinType;
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
public enum TriggerType
{
    None,

    // 스킬 사용
    SkillUse,
    Hit,
    SkillEnd,

    // 합
    ClashWin,
    ClashLose,

    // 턴
    TurnStart,
    TurnEnd
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
