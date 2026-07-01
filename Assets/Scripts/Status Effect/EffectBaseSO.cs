using UnityEngine;


public abstract class EffectBaseSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private StackType stackType;
    [SerializeField] private KeywordType keywordType;
    /// <summary>
    /// 스택 가능, 불가능 여부
    /// </summary>
    public enum StackType
    {
        None,
        Stackable
    }
    public KeywordType Keyword => keywordType;

    [Header("---UI---")]
    [SerializeField] private Sprite icon;
    [SerializeField] private string effectName;
    [SerializeField, TextArea] private string effectDescription;

    public TriggerType Trigger => triggerType;
    public StackType Stack => stackType;
    public Sprite Icon => icon;
    public string EffectName => effectName;
    public string EffectDescription => effectDescription;


    /// <summary>
    /// 세부 기능 구현부
    /// </summary>
    public abstract void Use(CharacterBase target);
}

/// <summary>
/// 없음, 화상, 출혈, 진동, 침잠, 파열, 호흡, 충전
/// </summary>
public enum KeywordType
{
    None,
    Bleed,
    Burn,
    Vibration,
    Sinking,
    Rupture,
    poise,
    Charge
}