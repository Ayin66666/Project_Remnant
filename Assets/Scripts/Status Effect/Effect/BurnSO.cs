using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BurnSO", menuName = "Character/StatusEffect/Burn", order = int.MaxValue)]
public class BurnSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        (int power, int count) = target.GetKeyword(KeywordType.Burn);
        AttackInfo info = new AttackInfo()
        {

        };
        target.TakeDamage(info);
    }
}
