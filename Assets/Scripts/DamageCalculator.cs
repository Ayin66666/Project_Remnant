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
        // (공격력 * 스킬 계수 * 코인 배율) * 치명타 배율 * 참관타 내성 * 코인 앞 뒷면 * 코인 파괴 여부 * 최종 데미지 증감 * 레벨 차이 증감
        // 레벨 차이 증감 = (공격력 - 방어력) / 3 = 두 수치의 차이 10당 데미지 10% 증감 (최대 ±30%)


        // 공격자 데이터 세팅
        StatData attacker = info.attacker.GetStat();
        float skillValue = info.skill.syncDatas[info.attacker.Sync].motionValue;

        // 피격자 데이터 세팅
        StatData defender = info.defender.GetStat();

        // 스킬 효과 세팅
        SkillEffect skillEffects = ApplySkillEffects(info.actions);


        // --- 이하 데미지 계산 로직 ---

        // 기본 데미지 (스킬 게수, 코인 배율)
        float damage = attacker.aPoint * skillValue * info.coinValue;

        // 치명타 적용
        damage *= (info.isCritical ? attacker.critMultiplier + skillEffects.critMultiplier : 1.0f);

        // 참관타 내성 적용
        damage *= info.skill.attackType switch
        {
            AttackType.Slash => 1.0f - defender.slashGurad,
            AttackType.Pierce => 1.0f - defender.pierceGuard,
            AttackType.Blunt => 1.0f - defender.bluntGuard,
            _ => 1.0f
        };

        // 코인 앞 & 뒷면 적용
        damage *= (info.isFront ? 1.0f : 0.5f);

        // 코인 파괴 여부 적용
        damage *= (info.isBreak ? 0.25f : 1.0f);

        // 최종 데미지 증감 적용
        damage *= (1.0f + ((attacker.increaseDamage + skillEffects.increaseDamage) / 100f) - (defender.reductionDamage / 100f));

        // 레벨 차이 적용
        float step = Mathf.Clamp(1.0f + (attacker.aPoint - defender.dPoint) / 100f, 0.7f, 1.3f);
        damage *= step;

        // 반환 -> 데미지 음수 방지 및 최소 데미지 보정
        return Mathf.Max(1, (int)damage);
    }

    /// <summary>
    /// Info 안에 있는 List<ActionNode> actions를 읽어 적용 가능한 수치 데이터로 전환
    /// </summary>
    /// <param name="info"></param>
    /// <returns></returns>
    public static SkillEffect ApplySkillEffects(List<ActionNode> actions)
    {
        SkillEffect skillEffect = new SkillEffect()
        {
            increaseDamage = 0.0f,
            critMultiplier = 0.0f
        };

        // 전환할 데이터가 없는 경우 종료
        if (actions == null || actions.Count == 0)
        {
            Debug.Log("전환할 데이터가 없음");
            return skillEffect;
        }

        for (int i = 0; i < actions.Count; i++)
        {
            ActionNode act = actions[i];
            if (act.actionType != ActionType.SkillEffect)
            {
                // 스킬 효과가 아닌 경우 종료
                Debug.LogError($"[DamageCalculator] 스킬 효과가 아닌 ActionNode가 전달 / (ActionType : {act.actionType})");
                break;
            }

            SkillEffectSO effect = act.valueNode.effect as SkillEffectSO;
            if(effect == null)
            {
                // 스킬 효과 SO가 아닌 경우 종료
                Debug.LogError($"[DamageCalculator] SkillEffectSO가 아닌 ActionNode가 전달 / (ActionType : {act.actionType})");
                break;
            }

            // 여러 종류지만 실제 대응해야 하는 효과는 스킬의 데미지 증가만!
            switch (effect.EffectType)
            {
                // 대응이 필요 없는 케이스 -> skillbase에서 효과 체크 후 적용시킴
                case SkillEffectSO.SkillEffectType.None:
                case SkillEffectSO.SkillEffectType.ExtraDamage:
                case SkillEffectSO.SkillEffectType.VibrationExplosion:
                case SkillEffectSO.SkillEffectType.BasePowerUp:
                case SkillEffectSO.SkillEffectType.CoinPowerUp:
                    break;

                // 대응이 필요한 케이스 -> 데미지 계산 시 적용
                case SkillEffectSO.SkillEffectType.IncreaseFinalDamage:
                    skillEffect.increaseDamage += act.valueNode.value;
                    break;

                case SkillEffectSO.SkillEffectType.CriticalMultiplier:
                    skillEffect.critMultiplier += act.valueNode.value;
                    break;
            }
        }

        return skillEffect;
    }

    [System.Serializable]
    public struct SkillEffect
    {
        // 추가할 옵션이 있다면 이곳에 추가 후 데미지 계산 로직에 적용
        public float increaseDamage; // 데미지 증가
        public float critMultiplier; // 치명타 배율
    }
}

[System.Serializable]
public struct CalInfo
{
    // 26.11.11 고민
    // -> 스킬 코인 타입에 따른 데미지 계산 로직 (파불코 = 파괴 시 0.25 / 일반 1.0)

    [Header("---공격자 데이터---")]
    /// <summary>
    /// 공격자
    /// </summary>
    public CharacterBase attacker;
    /// <summary>
    /// 공격 스킬
    /// </summary>
    public SkillSO skill;
    /// <summary>
    /// 해당 코인이 주는 데미지 비율(전체 데미지 중 해당 코인이 n%의 데미지를 준다)
    /// </summary>
    public float coinValue;
    /// <summary>
    /// 코인 앞 뒷면 여부 (앞면 : true, 뒷면 : false)
    /// </summary>
    public bool isFront;
    /// <summary>
    /// 치명타 여부 (치명타 : true, 일반타 : false)
    /// </summary>
    public bool isCritical;
    /// <summary>
    /// 코인 파괴 여부 (파괴 시 데미지 0.25 / 일반 1.0)
    /// </summary>
    public bool isBreak;
    /// <summary>
    /// 데미지 계산 시 적용할 스킬 효과 List
    /// </summary>
    public List<ActionNode> actions;

    [Header("---피격자 데이터---")]
    /// <summary>
    /// 피격자
    /// </summary>
    public CharacterBase defender;
}
