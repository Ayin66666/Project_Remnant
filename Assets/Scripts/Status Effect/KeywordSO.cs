using UnityEngine;


public class KeywordSO : EffectBaseSO
{
    [Header("---Keyword Base Setting---")]
    [SerializeField] protected bool isderived;
    public bool IsDerived => isderived;


    public override void Use(CharacterBase target)
    {
        // 세부 구현은 override 하여 세부 클래스에서 구현함!
    }
}


