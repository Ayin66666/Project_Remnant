using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_Submersion : Buff_Base
{
    public SubmersionType submersionType;
    public enum SubmersionType { Submersion }


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
        submersionType = ((Buff_Submersion)buff).submersionType;

    }
}
