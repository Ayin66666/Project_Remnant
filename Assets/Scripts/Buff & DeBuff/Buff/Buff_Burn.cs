using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff_Burn : Buff_Base
{
    public BurnType burnType;
    public enum BurnType { Burn }


    public override void Use()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateBuff()
    {
        throw new System.NotImplementedException();
    }

    public override void Type_Change(Buff_Base buff)
    {
        burnType = ((Buff_Burn)buff).burnType;
    }
}
