using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Status : Buff_Base
{

    public override void Use()
    {
        // ���� Ÿ�Կ� ���� ���� ���� ȣ��
        switch (type)
        {
            case BuffType.AttackPoint:
                break;

            case BuffType.DefensePoint:
                break;

            case BuffType.DamageIncrease:
                break;

            case BuffType.DamageReduction:
                break;

            case BuffType.minSpeed:
                break;

            case BuffType.maxSpeed:
                break;
        }
    }

    public override void UpdateBuff()
    {

    }

    public override void Type_Change(Buff_Base buff)
    {

    }
}
