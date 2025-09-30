using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff_Bleeding : Buff_Base
{
    public BleddingType bleddingType;
    public enum BleddingType { Bleeding }


    public override void Use()
    {
        // 수치만큼 체력 감소
    }

    public override void UpdateBuff()
    {
        // 동작할때마다 수치 1 감소
        buff_Duration--;
    }

    public override void Type_Change(Buff_Base buff)
    {
        bleddingType = ((Buff_Bleeding)buff).bleddingType;
    }
}
