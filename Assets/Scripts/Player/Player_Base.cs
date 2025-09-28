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
            // 1 스킬
        }
        else if (ran < 5)
        {
            // 2 스킬
        }
        else
        {
            // 3 스킬
        }
    }
}
