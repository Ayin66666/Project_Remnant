using UnityEngine;


[CreateAssetMenu(fileName = "BlackWaterSO", menuName = "Character/StatusEffect/BlackWater", order = int.MaxValue)]

public class BlackWaterSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 이거 고유 키워드는 KeywordSO 사용하기보다는 따로 작업이 필요할거 같은데
        // 고유 키워드는 딕셔너리 기준 검색이라 enum에 값이 없으면 사용 못함
        // 그렇다고 공용에 넣기에는 turn 에 대입할 데이터가 없는데
    }
}
