using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_BlackwaterArt_Undertow : SkillBase
{
    [Header("---Skill Setting---")]
    [SerializeField] private List<Transform> targetPos;
    [SerializeField] private List<GameObject> effects;

    // 1타 35% 2타 65% 데미지 비율

    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 공격 시작
        character.SetAttackState(true);

        // 전체 데미지 계산
        totalDamage = character.CalDamage(CreateInfo());

        // 적 위치 조절 (내 앞으로 이동)
        BattleManager.instance.SetTargetPos(useData.targets, targetPos[0]);

        // 남은 코인만큼 공격
        int attackCount = skillSO.syncDatas[character.Sync].coins.Count; // ?? 여기 파불코도 고려해서 만들어야 하는데?

        // 공격 종료
        character.SetAttackState(false);

        yield return null;
    }

    private IEnumerator CoinA()
    {
       int damage = totalDamage / skillSO.syncDatas[character.Sync].coins[0].DamagePercent;

        anim.SetTrigger("Action");
        anim.SetBool("Attack_1-1", true);
        while(anim.GetBool("Attack_1-1"))
        {
            yield return null;
        }
    }

    private void CoinB()
    {
        int damage = totalDamage / skillSO.syncDatas[character.Sync].coins[1].DamagePercent;
    }
}
