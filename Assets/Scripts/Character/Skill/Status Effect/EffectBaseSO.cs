using UnityEngine;

public abstract class EffectBaseSO : ScriptableObject
{
    [Header("---State---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private StackType stackType;
    public TriggerType Trigger => triggerType;
    public StackType Stack => stackType;

    [Header("---UI data---")]
    [SerializeField] private Sprite icon;
    [SerializeField] private string effectName;
    [SerializeField, TextArea] private string effectDescription;
    public Sprite Icon => icon;
    public string EffectName => effectName;
    public string EffectDescription => effectDescription;

    /// <summary>
    /// 스택 불가, 스택가능
    /// </summary> 
    public enum StackType { None, Stackable }


    /// <summary>
    /// 세부 기능 구현
    /// </summary>
    public abstract void Use(StatusEffectContainer owner);
}
