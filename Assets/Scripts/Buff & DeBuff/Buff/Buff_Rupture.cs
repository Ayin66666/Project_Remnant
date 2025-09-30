using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff_Rupture : Buff_Base
{
    public RuptureType ruptureType;
    public enum RuptureType { Rupture }


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
        ruptureType = ((Buff_Rupture)buff).ruptureType;

    }
}
