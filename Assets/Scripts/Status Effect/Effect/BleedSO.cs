using UnityEngine;


[CreateAssetMenu(fileName = "BleedSO", menuName = "Character/StatusEffect/Bleed", order = int.MaxValue)]
public class BleedSO : KeywordSO
{
    // 공격, 합 진행 시 출혈 위력만큼 데미지

    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        KeywordEffectRuntimeData data = target.GetKeyword(KeywordType.Bleed);
        if (data == null) return;

        // 데미지 전달
        int damage = data.power;
        target.TakeDamage(damage);

        // 키워드 감소
        target.ConsumeKeyword(KeywordType.Burn, 1);
    }
}
