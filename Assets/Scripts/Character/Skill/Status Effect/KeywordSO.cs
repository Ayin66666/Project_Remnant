using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KeywordSO : EffectBaseSO
{
    [Header("---KeyWord Setting---")]
    [SerializeField] private KeywordType keywordType;

    // 화상, 출혈, 진동, 침잠, 파열, 호흡, 충전
    public enum KeywordType
    { None, Bleed, Burn, Vibration, Sinking, Rupture, poise, Charge }



    public override void Use(StatusEffectContainer owner)
    {
        throw new System.NotImplementedException();
    }
}
