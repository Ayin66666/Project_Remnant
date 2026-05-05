using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Character;


public class PlayerCharacter : CharacterBase
{
    /// <summary>
    /// 플레이어 - 동기화 기반 스테이터스 세팅 기능
    /// </summary>
    /// <param name="data"></param>
    protected override void SetupStatus(StatusDataSO data)
    {
        maxHp = data.BaseHp // 기본 체력
            + data.SyncUpData[sync].hp // 동기화 체력 
            + Mathf.RoundToInt(data.LevelUpData.hp * level * data.GrowthFactorData.hpFactor); // 레벨업 체력

        curHp = maxHp;

        attack = data.BaseAttackPoint
            + data.SyncUpData[sync].attack
            + Mathf.RoundToInt(data.LevelUpData.attack * level * data.GrowthFactorData.attackFactor);

        defence = data.BaseDefencePoint
            + data.SyncUpData[sync].defence
            + Mathf.RoundToInt(data.LevelUpData.defence * level * data.GrowthFactorData.defenceFactor);

        speed = data.SyncUpData[sync].attackSpeed;

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
