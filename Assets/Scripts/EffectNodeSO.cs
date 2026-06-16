using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Effect Node", menuName = "Skill/Effect Node", order = int.MaxValue)]
public class EffectNodeSO : ScriptableObject
{
    [Header("---Effect Node---")]
    /// <summary>
    /// 이펙트 노드
    /// </summary>
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private TargetType targetType;
    [SerializeField] private List<ConditionNode> conditions;

    public TriggerType Trigger => Trigger;
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

    /* 액션 노드 제거 결정 - 해당 기능은 skillbase 를 상속받은 세부 스크립트에서 선언
    [System.Serializable]
    /// <summary>
    /// 조건 만족 시 동작할 액션 타입 노드
    /// </summary>
    public struct ActionNode
    {
        // 일단은 이렇게 적었으나,
        // 최종적으로는 유틸리티 기반 기능 구현으로 갈듯
        [Header("---Action---")]
        public ActionType action;
        public int value;
        public enum ActionType { Add, Remove, Damage, Heal };
    }
    */
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
