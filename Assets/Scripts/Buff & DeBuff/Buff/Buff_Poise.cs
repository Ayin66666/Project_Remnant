using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Poise : Buff_Base
{
    public PoiseType submersionType;
    public enum PoiseType { Poise }

    public override void Use()
    {

    }

    public override void UpdateBuff()
    {

    }

    public override void Type_Change(Buff_Base buff)
    {
        submersionType = ((Buff_Poise)buff).submersionType;
    }
}
