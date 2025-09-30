using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Buff_Bleeding : Buff_Base
{
    public BleddingType bleddingType;
    public enum BleddingType { Bleeding }


    public override void Use()
    {
        // ��ġ��ŭ ü�� ����
    }

    public override void UpdateBuff()
    {
        // �����Ҷ����� ��ġ 1 ����
        buff_Duration--;
    }

    public override void Type_Change(Buff_Base buff)
    {
        bleddingType = ((Buff_Bleeding)buff).bleddingType;
    }
}
