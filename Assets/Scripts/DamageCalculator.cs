using System.Collections.Generic;
using UnityEngine;


public static class DamageCalculator
{
    /// <summary>
    /// 데미지 계산 로직 - 공격자 & 피격자 데이터를 전달받아 데미지를 계산 후 반환
    /// </summary>
    /// <returns></returns>
    public static int CalculateDamage(CalInfo info)
    {
        // 데미지 계산 로직은 다음과 같음
        // 코인 앞면 : 1 뒷면 : 0.5
        // {(공격력 * 스킬 계수 * 치명타 배율 * 코인 결과) * 최종 데미지 증감 * step} * 데미지 감소
        // step = (공격력 - 방어력) / 3 = 차이 10당 데미지 10% 증감 (최대 +=30%)


        // 공격자 데이터 세팅

        // 피격자 데이터 세팅

        // 데미지 계산
        int damage = 0;

        //반환
        return damage;
    }
}

[System.Serializable]
public struct CalInfo
{
    [Header("---공격자 데이터---")]
    /// <summary>
    /// 공격자
    /// </summary>
    [SerializeField] private CharacterBase attacker;
    /// <summary>
    /// 공격 스킬
    /// </summary>
    [SerializeField] private SkillSO skill;
    /// <summary>
    /// 데미지 계산 시 적용할 스킬 효과 List
    /// </summary>
    [SerializeField] private List<ActionNode> actions;

    [Header("---피격자 데이터---")]
    /// <summary>
    /// 피격자
    /// </summary>
    [SerializeField] private CharacterBase defender;
}
