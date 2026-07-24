using UnityEngine;


[CreateAssetMenu(fileName = "BlackWaterSO", menuName = "Character/StatusEffect/BlackWater", order = int.MaxValue)]

public class Original_BlackWaterSO : EffectBaseSO
{
    public override void Use(CharacterBase target)
    {
        // 공용 키워드는 EffectBaseSO를 기반으로 동작함
        // 기본적으로 Keyword처럼 위력, 횟수만 존재
        // 만약 Turn에 대한 소요가 필요하다면 횟수를 매턴 제거하는 로직으로 대체하면 될듯
    }
}
