using UnityEngine;

[CreateAssetMenu(fileName = "RuptureSO", menuName = "Character/StatusEffect/Rupture", order = int.MaxValue)]
public class RuptureSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        EffectRuntimeData data = target.GetKeyword(KeywordType.Rupture);
        if (data == null) return;

        AttackInfo info = new AttackInfo()
        {
            sinType = SinType.Lust,
            attackType = AttackType.None,
            isUseDamageCal = false,
            isCritical = false,
            attackPoint = 0,
            damage = data.power,
        };

        // 데미지 부여
        target.TakeDamage(info);

        // 키워드 감소
        target.ConsumeKeyword(KeywordType.Rupture, 1);
    }
}
