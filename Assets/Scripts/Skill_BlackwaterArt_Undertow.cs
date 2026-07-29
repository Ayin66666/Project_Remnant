using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_BlackwaterArt_Undertow : SkillBase
{
    [Header("---Skill Setting---")]
    [SerializeField] private List<GameObject> effects;

    // 1타 35% 2타 65% 데미지 비율

    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 공격 시작
        character.SetAttackState(true);

        // 적 위치 조절 (내 앞으로 이동)

        // 전체 데미지 계산
        character.CalDamage(CreateInfo());
        List<int> damageList = new List<int>();

        // 남은 코인만큼 공격
        for (int i = 0; i < useData.destroyedCoinCount; i++)
        {
            // 애니메이션 호출
            anim.SetTrigger("Action");
            anim.SetBool("isAttack", true);

            // 애니메이션 대기
            yield return new WaitWhile(() => anim.GetBool("isAttack"));
        }

        // 공격 종료
        character.SetAttackState(false);
    }
}
