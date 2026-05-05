using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;


public class EnemyBase : CharacterBase
{
    /// <summary>
    /// 에너미 - 스테이터스 기반 능력치 세팅 (추가 계산 x)
    /// </summary>
    /// <param name="data"></param>
    protected override void SetupStatus(StatusDataSO data)
    {
        maxHp = data.BaseHp;
        curHp = maxHp;
        attack = data.BaseAttackPoint;
        defence = data.BaseDefencePoint;
        speed = data.SyncUpData[0].attackSpeed;

        groggy.Clear();
        foreach (int g in data.Groggy)
        {
            groggy.Add(Mathf.RoundToInt(maxHp * g / 100));
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
