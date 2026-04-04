using UnityEngine;

public abstract class StatusEffectSO : ScriptableObject
{
    [Header("---State---")]
    public EffectType effectType;
    public StackType stackType;
    public TriggerType triggerType;
    public StatType statType;
    public KeywordType keywordType;


    [Header("---UI data---")]
    public Sprite effectIcon;
    public string effectName;
    [TextArea] public string effectDescription;

    // 버프, 디버프
    public enum EffectType 
    { Buff, Debuff }
    
    // 스택 불가, 스택가능
    public enum StackType 
    { None, Stackable }
    
    // 턴 시작, 턴 종료, 피격, 타격
    public enum TriggerType 
    { None, OnTurnStart, OnTurnEnd, OnHit, OnDamaged }

    // 화상, 출혈, 진동, 침잠, 파열, 호흡, 충전
    public enum KeywordType 
    { None, Bleed, Burn, Vibration, Sinking, Rupture, poise, Charge }
    
    // 공격 포인트, 방어 포인트, 최종뎀증, 최종뎀감
    public enum StatType 
    { None, AttackPoint, DefensePoint, FinalDamageDealt, FinalDamageTaken }


    /// <summary>
    /// 세부 기능 구현
    /// </summary>
    public abstract void Use(StatusEffectContainer owner, StatusEffectContainer target);
}
