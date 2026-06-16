using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StatSO : EffectBaseSO
{
    [Header("---Stat Setting---")]
    [SerializeField] private StatType statType;


    /// <summary>
    /// 공격 포인트, 방어 포인트, 최종뎀증, 최종뎀감
    /// </summary>
    public enum StatType
    { None, AttackPoint, DefensePoint, FinalDamageDealt, FinalDamageTaken }

    public override void Use(StatusEffectContainer owner)
    {
        throw new System.NotImplementedException();
    }
}
