using UnityEngine;


[CreateAssetMenu(fileName = "ChargeSO", menuName = "Character/StatusEffect/Charge", order = int.MaxValue)]
public class ChargeSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 충전은 획득, 소모만 존재하기 때문에 따로 기능을 구현하지 않음
    }

    public void ChargeUse(CharacterBase target, int useCount)
    {
        EffectRuntimeData data = target.GetKeyword(KeywordType.Charge);
        if (data == null)
        {
            Debug.LogError($"충전을 보유하고 있지 않음! {target} / {useCount}");
            return;
        }

        if (data.count < useCount)
        {
            Debug.LogError($"충전 보유량 이상으로 소모하려 함! {target} / 보유 : {data.count} 소모 : {useCount}");
            return;
        }

        target.ConsumeKeyword(KeywordType.Charge, useCount);
    }
}
