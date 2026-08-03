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
    /// 동작 조건 노드
    /// </summary>
    public struct ConditionNode
    {
        [Header("---Condition---")]
        public TargetType checkTarget;
        public CompareType compareType;
        public int conditionValue;
        public List<EffectValue> values;
    }
    #endregion
}

[System.Serializable]
/// <summary>
/// 값 저장 노드
/// </summary>>
public struct EffectValue
{
    // 이거 power랑 count 를 나눌 필요 있나?
    // 어차피 value & duration 다 있으니
    // keyword는 value가 위력, duration이 횟수로,
    // public은 value가 위력, duration이 지속 턴으로 하면 되는게?

    [Header("---Value---")]
    public EffectBaseSO effect;
    public ValueType valueType; // 이거 필요하긴 함 -> 조건 체크에서 필요
    public int value;
    public int duration;
}

[System.Serializable]
/// <summary>
/// 동작 액션 종류
/// 26.07.23 Public으로 전환함! => 사유 : passive 및 ego에서도 재사용
/// </summary>
public struct ActionNode
{
    [Header("---Action---")]
    public ActionType actionType;
    public EffectValue valueNode;

    [Header("---Original Action---")]
    public int originalActionId;
    [TextArea] public string actionDescription;
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
    TurnEnd = 1 << 7,

    // 키워드
    KeywordAdded = 1 << 8,
    KeywordConsumed = 1 << 9,

    // 회피
    DodgeSuccess = 1 << 10,
}

public enum ActionType
{
    None,
    AddEffect,
    RemoveEffect,
    SkillEffect,
    Original
}

public enum ValueType
{
    None,
    Power,
    Count
}
#endregion
