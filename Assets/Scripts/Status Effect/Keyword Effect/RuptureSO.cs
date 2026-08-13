using UnityEngine;

[CreateAssetMenu(fileName = "RuptureSO", menuName = "Character/StatusEffect/Rupture", order = int.MaxValue)]
public class RuptureSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        EffectRuntimeData data = target.GetKeyword(KeywordType.Rupture);
        if (data == null) return;

        // 데미지 부여
        target.TakeDamage(false, data.power);

        // 키워드 감소
        target.ConsumeKeyword(KeywordType.Rupture, 1);
    }
}
