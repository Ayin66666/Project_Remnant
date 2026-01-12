using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Burn", menuName = "Status Effect/Burn", order = int.MaxValue)]
public class Burn : StatusEffectSO
{
    public override void Use(StatusEffectContainer owner, StatusEffectContainer target)
    {
        // 턴 종료 시 화상 수치만큼 데미지 후 횟수 1 감소
    }
}
