using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Base : Character_Base
{
    public void Skill_Setting()
    {
        int ran = Random.Range(0, 6);
        if (ran < 3)
        {
            // 1 ��ų
        }
        else if (ran < 5)
        {
            // 2 ��ų
        }
        else
        {
            // 3 ��ų
        }
    }
}
