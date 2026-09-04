using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_BlackwaterArt_SinkingCurrent : SkillBase
{
    [Header("---Setting---")]
    [SerializeField] private Transform[] movePos;


    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 적을 지나치며 강한 일격
        // 뒤로 후려차며 2타
        // 데미지 분배는 1타(40%) + 1타(60%)

        // 바라보는 방향 전환
        SetFacing();

        anim.SetTrigger("Action");
        anim.SetBool("isSkill_1", true);

        // 1타 - 공격하며 뒤로 넘어가기(40%)
        bool isAttack = false;
        Vector3 startPos = character.transform.position;
        Vector3 endPos = movePos[0].transform.position;
        float timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime / 0.35f;
            character.transform.position = Vector3.Lerp(startPos, endPos, timer);

            if (timer > 0.5f && !isAttack)
            {
                isAttack = true;
                Attack1();
            }

            yield return null;
        }

        // 바라보는 방향 전환
        SetFacing();

        // 딜레이
        yield return new WaitForSeconds(0.15f);

        // 2타 - 뒤로 후려차기(60%)
        startPos = character.transform.position;
        endPos = movePos[1].transform.position;
        timer = 0;
        while(timer < 1)
        {
            timer += Time.deltaTime / 0.15f;
            character.transform.position = Vector3.Lerp(startPos, endPos, timer);

            yield return null;
        }
        Attack2();
    }


    private void Attack1()
    {
        // 1타 (40%)
        (bool isCri, int damage) = CalCoinDamage(totalDamage, 0.4f);
    }

    private void Attack2()
    {
        // 2타 (60%)
        (bool isCri, int damage) = CalCoinDamage(totalDamage, 0.6f);
    }
}
