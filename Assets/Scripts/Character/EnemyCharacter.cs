using UnityEngine;


public class EnemyCharacter : CharacterBase
{
    /// <summary>
    /// 에너미 - 스테이터스 기반 능력치 세팅 (추가 계산 x)
    /// </summary>
    /// <param name="data"></param>
    protected override void SetUpStatus(StatusDataSO data)
    {
        maxHp = data.BaseHp;
        curHp = maxHp;
        attack = data.BaseAttackPoint;
        defence = data.BaseDefencePoint;
        speedRange = data.SyncUpData[0].attackSpeed;

        slashGurad = data.SlashGuard;
        pierceGuard = data.PierceGuard;
        bluntGuard = data.BluntGuard;

        stagger.Clear();
        foreach (int g in data.Groggy)
        {
            stagger.Add(Mathf.RoundToInt(maxHp * g / 100));
        }
    }

    public override void Die()
    {
        throw new System.NotImplementedException();
    }
}
