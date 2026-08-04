using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_BlackwaterArt_Undertow : SkillBase
{
    // 1스킬 (강화)
    [Header("---Skill Setting---")]
    [SerializeField] private List<Transform> targetMovePos;
    [SerializeField] private List<GameObject> effects;
    [SerializeField] private List<Transform> movePos;
    [SerializeField] private List<string> animBool;


    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 1타 100% 데미지 비율

        // 공격 시작
        character.SetAttackState(true);

        // 사용 시 효과 적용
        ApplySkillEffect();

        // 전체 데미지 계산 - 1코인 스킬이라 위와 같이 계산
        totalDamage = (int)(character.CalDamage(CreateInfo()) // 기본 데미지 계산
            * (PoiseSO.IsCritical(character) ? 1.5f : 1f) // 크리티컬 여부 계산
            * (useData.isCoinDestroy[0] ? 0.25f : 1f)); // 코인 파괴 여부 계산


        // 적 위치 조절 (내 앞으로 이동)
        BattleManager.instance.SetTargetPos(useData.targets, targetMovePos[0]);

        // 코인 상태 확인
        if (useData.isCoinDestroy[0])
            totalDamage = (int)(totalDamage * 0.25f);


        // 최종 데미지 계산
        int damage = totalDamage /
            (int)(skillSO.syncDatas[character.Sync].coins[0].DamagePercent
            * (useData.isCoinDestroy[0] ? 0.25f : 1f));

        // 애니메이션
        anim.SetTrigger("Action");
        anim.SetBool(animBool[0], true);
        while (anim.GetBool(animBool[0]))
        {
            yield return null;
        }

        // 공격 종료
        character.SetAttackState(false);
    }

    public override void Movement(int index)
    {
        character.CharacterMove(0.55f, targetMovePos[0].position);
    }
}
