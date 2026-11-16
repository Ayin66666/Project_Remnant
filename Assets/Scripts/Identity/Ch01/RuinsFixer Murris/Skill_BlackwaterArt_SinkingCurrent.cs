using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class Skill_BlackwaterArt_SinkingCurrent : SkillBase
{
    [Header("---Setting---")]
    [SerializeField] private Transform[] movePos;
    private SkillUseData data;

    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 적을 지나치며 강한 일격
        // 뒤로 후려차며 2타
        // 데미지 분배는 1타(40%) + 1타(60%)


        // --- 동작 로직 ---
        data = useData;

        // 바라보는 방향 전환
        SetFacing();

        // 1타 - 공격하며 뒤로 넘어가기(40%)
        bool isAttack = false;
        Vector3 startPos = character.transform.position;
        Vector3 endPos = movePos[0].transform.position;
        float timer = 0;
        while (timer < 1)
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
        while (timer < 1)
        {
            timer += Time.deltaTime / 0.15f;
            character.transform.position = Vector3.Lerp(startPos, endPos, timer);

            yield return null;
        }
        Attack2();
    }


    private void Attack1() // -> 이 기능 통합해서 하나의 함수로 만드는게 더 좋지 않나?
    {
        // 1타 (40%)
        for (int i = 0; i < data.targets.Count; i++)
        {
            // 계산을 위한 데이터 제작
            CalInfo info = new CalInfo
            {
                attacker = character,
                skill = skillSO,
                coinValue = skillSO.syncDatas[character.Sync].coins[0].DamagePercent * 0.01f,
                isFront = true,
                isCritical = false,
                isBreak = false,
                actions = null,
                defender = data.targets[i]
            };

            // 데미지 정보 제작 후 공격 대상에게 전달
            AttackInfo attackInfo = new AttackInfo
            {
                attackType = skillSO.attackType,
                sinType = skillSO.sinType,
                isCritical = false,
                damage = DamageCalculator.CalculateDamage(info)
            };
            data.targets[i].TakeDamage(attackInfo);
        }
    }

    private void Attack2()
    {
        // 2타 (60%)
    }
}
