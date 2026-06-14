using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Effect Node", menuName = "Skill/Effect Node", order = int.MaxValue)]
public class EffectNodeSO : ScriptableObject
{
    [Header("---Effect Node---")]
    /// <summary>
    /// 이펙트 노드
    /// </summary>
    public TriggerType triggerType;
    public TargetType targetType;
    public List<ConditionNode> conditions;

    #region Enum
    public enum TriggerType
    {
        OnUse,
        OnHit,
        OnClashWin,
        OnClashLose,
        OnSkillEnd
    }

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
        public StatusEffectSO effet;
        public ValueType valueType;
    }
}
