using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EffectNode
{
    [Header("---Effect Node---")]
    /// <summary>
    /// 이펙트 노드
    /// </summary>
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private TargetType targetType;
    [SerializeField] private List<ConditionNode> conditions;

    public TriggerType Trigger => triggerType;
    public TargetType Target => targetType;
    public List<ConditionNode> Conditions => conditions;

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
        Less,
        LessEqual,
        Equal,
        GreaterEqual,
        Greater
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
    /// 동작 조건 노드
    /// </summary>
    public struct ConditionNode
    {
        [Header("---Condition---")]
        public List<ValueNode> values;
        public CompareType compareType;
        public int value;
    }

    [System.Serializable]
    /// <summary>
    /// 조건 종류 & 조건 타입 노드
    /// </summary>>
    public struct ValueNode
    {
        [Header("---Value---")]
        public EffectBaseSO effet;
        public ValueType valueType;
    }

    [System.Serializable]
    /// <summary>
    /// 조건 만족 시 동작할 액션 타입 노드
    /// </summary>
    public struct ActionNode
    {
        // 액션 노드는 설명과 액션 인덱스만 보유
        // -> 세부 동작 선언은 SkillBase에서 해둠!
        [Header("---Public Action---")]
        public ActionType actionType;
        public int actionValue;

        [Header("---Original Action---")]
        public int actionIndex;
        [SerializeField, TextArea] private string actionDesCription;
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
    Add,
    Reomve,

    healHp,
    Damage,


    Original
}
