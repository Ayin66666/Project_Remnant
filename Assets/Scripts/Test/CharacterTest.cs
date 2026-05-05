using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterTest : CharacterBase
{
    protected override void SetupStatus(StatusDataSO data)
    {
        throw new System.NotImplementedException();
    }

    public override void Die()
    {
        // 이거 최종적으로는 PlayerCharacter 스크립트 단일이 동작하게 할 예정
        // 몬스터의 경우 enemyBase를 구현하고 이걸 상속받아 enemy_Nmae 형태로 각각 구현
    }
}
