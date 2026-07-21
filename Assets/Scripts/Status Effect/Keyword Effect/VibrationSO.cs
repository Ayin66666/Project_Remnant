using UnityEngine;


[CreateAssetMenu(fileName = "VibrationSO", menuName = "Character/StatusEffect/Vibration", order = int.MaxValue)]
public class VibrationSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        EffectRuntimeData data = target.GetKeyword(KeywordType.Vibration);
        if (data == null) return;

        // 흐트러짐 데미지 부여
        target.TakeSDamage(data.power);

        // 키워드 감소
        target.ConsumeKeyword(KeywordType.Burn, 1);
    }
}
