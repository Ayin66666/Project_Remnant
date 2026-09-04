using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Skill_BlackwaterArt_Undertow : SkillBase
{
    // 1스킬 (강화)
    [Header("---Skill Setting---")]
    [SerializeField] private List<Transform> movePos;
    private bool isCri = false;


    // 1타 100% 데미지 비율
    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 데미지 분배는 1타(50%) + 5타(10% x5)
        (bool isCri, int damage) = CalCoinDamage(totalDamage, 0.5f);
        this.isCri = isCri;
        
        // 코인 토스
        character.CoinToss();
        
        // 애니메이션 + 이동
        anim.SetTrigger("Action");
        anim.SetBool("isSkill_1-2", true);

        Vector3 startPos = character.transform.position;
        Vector3 endPos = movePos[0].position;
        bool isAttack = false;
        float timer = 0;
        while(timer < 1)
        {
            if (timer > 0.5f && !isAttack)
            {
                isAttack = true;
                Attack1_1();
            }

            timer += Time.deltaTime / 0.35f;
            character.transform.position = Vector3.Lerp(startPos, endPos, timer);
            yield return null;
        }

        // 추가타 호출
        yield return StartCoroutine(Attack1_2());
        anim.SetBool("isSkill_1-2", false);

        // 공격 종료
        character.SetAttackState(false);
    }

    /// <summary>
    /// 1타 - 50% 1회
    /// </summary>
    public void Attack1_1()
    {
        AttackInfo info = new AttackInfo()
        {
            sinType = SinType.Lust,
            attackType = AttackType.None,
            isUseDamageCal = false,
            isCritical = isCri,
            attackPoint = character.GetStat(CharacterBase.StatType.AttackPoint),
            damage = totalDamage / 2,
        };

        foreach (CharacterBase target in targetList)
        {
            target.TakeDamage(info);
        }
    }

    /// <summary>
    /// Attack1_2() 의 세부동작 코루틴 (데미지 부여)
    /// </summary>
    /// <returns></returns>
    private IEnumerator Attack1_2()
    {
        AttackInfo info = new AttackInfo()
        {
            sinType = SinType.Lust,
            attackType = AttackType.None,
            isUseDamageCal = false,
            isCritical = this.isCri,
            attackPoint = character.GetStat(CharacterBase.StatType.AttackPoint),
            damage = totalDamage / 5,
        };

        for (int i = 0; i < 5; i++)
        {
            foreach (CharacterBase target in targetList)
            {
                target.TakeDamage(info);
            }

            // 딜레이
            yield return new WaitForSeconds(0.05f);
        }
    }
}
