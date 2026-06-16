using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_BlackwaterArt_Undertow : SkillBase
{
    [Header("---Skill Setting---")]
    [SerializeField] private List<GameObject> effects;


    protected override IEnumerator SkillAction()
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        foreach(GameObject obj in effects)
        {
            obj.SetActive(false);
        }
    }
}
