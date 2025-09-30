using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Buff_Vibration : Buff_Base
{
    public VibrationType vibrationType;
    public enum VibrationType { Vibration }


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
        vibrationType = ((Buff_Vibration)buff).vibrationType;
    }
}
