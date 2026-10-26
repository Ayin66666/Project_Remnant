using UnityEngine;


public abstract class EffectBaseSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private TriggerType triggerType; // -> 이거 만약 트리거가 다중이면? / List 처리해야하나? / flag 사용했음!
    [SerializeField] private EffectCategory effectCategory;
    [SerializeField] private KeywordType keywordType;
    [SerializeField] private int maxPower;
    [SerializeField] private int maxCount;

    public enum EffectCategory
    {
        Public,
        Keyword,
        Original
    }
    public EffectCategory Category => effectCategory;
    public KeywordType Keyword => keywordType;

    [Header("---UI---")]
    [SerializeField] private Sprite icon;
    [SerializeField] private string effectName;
    [SerializeField, TextArea] private string effectDescription;

    public TriggerType Trigger => triggerType;
    public int MaxPower => maxPower;
    public int MaxCount => maxCount;
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
    Bleed, // 완
    Burn,  // 완
    Vibration, // 완
    Sinking, // 완 
    Rupture, // 완
    Poise, // 완 
    Charge // 완
}