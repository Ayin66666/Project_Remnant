using UnityEngine;


[CreateAssetMenu(fileName = "DamageModifier", menuName = "Character/StatusEffect/PublicEffect/DamageModifierSO", order = int.MaxValue)]
public class DamageModifierSO : EffectBaseSO
{
    [Header("---Setting---")]
    [SerializeField] private SinType sin;
    [SerializeField] private DamageEffectType effectType;
    public DamageEffectType EffectType => effectType;
    public enum DamageEffectType
    {
        None,

        // 데미지 증가
        IncreaseDamage,
        IncreaseSinDamage,

        // 데미지 감소
        DamageReduction,

        // 코인
        SkillBasePower,
        SkillCoinPower,
    }


    public override void Use(CharacterBase target)
    {
        switch (effectType)
        {
            case DamageEffectType.None:
                break;

            case DamageEffectType.IncreaseDamage:
                break;

            case DamageEffectType.IncreaseSinDamage:
                break;

            case DamageEffectType.DamageReduction:
                break;

            case DamageEffectType.SkillBasePower:
                break;

            case DamageEffectType.SkillCoinPower:
                break;
        }
    }
}