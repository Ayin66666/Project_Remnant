using UnityEngine;


public class KeywordSO : EffectBaseSO
{
    [Header("---Keyword Base Setting---")]
    [SerializeField] protected bool isderived;
    public bool IsDerived => isderived;


    public override void Use(CharacterBase target)
    {

    }
}


