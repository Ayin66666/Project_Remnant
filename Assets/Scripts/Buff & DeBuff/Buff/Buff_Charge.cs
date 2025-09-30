using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff_Charge : Buff_Base
{
    public ChargeType chargeType;
    public enum ChargeType { Charge }


    public override void Use()
    {
        switch (chargeType)
        {
            case ChargeType.Charge:
                break;
        }
    }

    public override void UpdateBuff()
    {
        throw new System.NotImplementedException();
    }

    public override void Type_Change(Buff_Base buff)
    {
        chargeType = ((Buff_Charge)buff).chargeType;
    }
}
