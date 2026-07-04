using UnityEngine;


[CreateAssetMenu(fileName = "SinkingSO", menuName = "Character/StatusEffect/Sinking", order = int.MaxValue)]
public class SinkingSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        KeywordEffectRuntimeData data = target.GetKeyword(KeywordType.Sinking);
        if (data == null) return;

        // 정신력 감소
        target.TakeMDamage(data.power);

        // 카운트 감소
        target.ConsumeKeyword(KeywordType.Sinking, 1);
    }
}
