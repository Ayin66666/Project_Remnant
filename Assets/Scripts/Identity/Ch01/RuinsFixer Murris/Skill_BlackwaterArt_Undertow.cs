using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill_BlackwaterArt_Undertow : SkillBase
{
    // 1스킬 (강화)
    [Header("---Skill Setting---")]
    [SerializeField] private List<Transform> targetMovePos;
    [SerializeField] private List<Transform> movePos;
    [SerializeField] private List<string> animBool;


    // 1타 100% 데미지 비율
    protected override IEnumerator SkillAction(SkillUseData useData)
    {
        // 공격 시작
        character.SetAttackState(true);
        Reset();

        // 타겟 데이터 추가 - 임시
        targetList.AddRange(useData.targets);

        // 사용 시 효과 적용
        for (int i = 0; i < skillSO.syncDatas[character.Sync].skillEffects.Count; i++)
        {
            ApplyEffect(skillSO.syncDatas[character.Sync].skillEffects[i], useData.targets);
        }

        // 전체 데미지 계산 - 데미지 분배는 1타(50%) + 5타(10% x5)
        CalTotalDamage();
        
        // 적 위치 조절 (내 앞으로 이동)
        BattleManager.instance.SetTargetPos(useData.targets, targetMovePos[0]);

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
        character.CharacterMove(0.55f, targetMovePos[index].position);
    }


    #region 애니메이션 이벤트 - 데미지 로직
    /// <summary>
    /// 1타 - 50% 1회
    /// </summary>
    public void Attack1_1()
    {
        (bool isCri, int damage) = CalCoinDamage(totalDamage, 0.5f);
        foreach (CharacterBase target in targetList)
        {
            target.TakeDamage(isCri, damage);
        }
    }

    /// <summary>
    /// 2타 - 10% 5회
    /// </summary>
    public void Attack1_2()
    {
        StartCoroutine(CoAttack1_2());
    }

    /// <summary>
    /// Attack1_2() 의 세부동작 코루틴 (데미지 부여)
    /// </summary>
    /// <returns></returns>
    private IEnumerator CoAttack1_2()
    {
        (bool isCri, int damage) = CalCoinDamage(totalDamage, 0.5f);
        for (int i = 0; i < 5; i++)
        {
            foreach (CharacterBase target in targetList)
            {
                target.TakeDamage(isCri, damage / 5);
            }

            // 딜레이
            yield return new WaitForSeconds(0.05f);
        }
    }
    #endregion
}
