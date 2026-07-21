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
    }


    public override void Use(CharacterBase target)
    {

    }
}
