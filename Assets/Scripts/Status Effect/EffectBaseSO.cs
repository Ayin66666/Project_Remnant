using UnityEngine;


public abstract class EffectBaseSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private TriggerType triggerType;
    [SerializeField] private StackType stackType;
    public enum StackType
    {
        None,
        Stackable
    }

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
    /// 세부 기능 구현부 함수
    /// 필요 데이터는 (누구에게, 뭐를, 얼마나)임!
    /// </summary>
    public abstract void Use(EffectContext data);
}



[System.Serializable]
public class EffectContext
{
    public CharacterBase target;
    public EffectNode.ValueType type;
    public int value;
}