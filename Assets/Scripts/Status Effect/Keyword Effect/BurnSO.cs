using UnityEngine;


[CreateAssetMenu(fileName = "BurnSO", menuName = "Character/StatusEffect/Burn", order = int.MaxValue)]
public class BurnSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        EffectRuntimeData data = target.GetKeyword(KeywordType.Burn);
        if(data == null) return;

        // 데미지 작성
        AttackInfo info = new AttackInfo()
        {
            sinType = SinType.Wrath,
            attackType = AttackType.None,
            isUseDamageCal = false,
            isCritical = false,
            attackPoint = 0,
            damage = data.power,
        };

        // 데미지 전달
        target.TakeDamage(info);

        // 카운트 감소
        target.ConsumeKeyword(KeywordType.Burn, 1);
    }
}
