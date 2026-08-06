using UnityEngine;


[CreateAssetMenu(fileName = "SkillEffect", menuName = "Character/StatusEffect/PublicEffect/SkillEffectSO", order = int.MaxValue)]
public class SkillEffectSO : EffectBaseSO
{
    [Header("---Setting---")]
    [SerializeField] private SkillEffectType effectType;
    public SkillEffectType EffectType => effectType;
    public enum SkillEffectType 
    {
        None,

        ExtraDamage, // 추가 데미지
        IncreaseFinalDamage, // 최종 피해량 증가
        CriticalMultiplier, // 치명타 데미지 증가

        VibrationExplosion, // 진동 폭발
        
        BasePowerUp, // 기본 위력 증가
        CoinPowerUp, // 동전 위력 증가
    }


    public override void Use(CharacterBase target)
    {
        // 해당 함수는 Keyword에서만 사용함
        // Public의 경우 SO는 자신이 무엇인지에 대한 정보만 가지고 있음
    }
}
