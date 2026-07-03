using UnityEngine;


[CreateAssetMenu(fileName = "BurnSO", menuName = "Character/StatusEffect/Burn", order = int.MaxValue)]
public class BurnSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 데이터 받아오기
        KeywordEffectRuntimeData data = target.GetKeyword(KeywordType.Burn);
        if(data == null) return;

        // 데미지 인포 작성
        // -> 이 부분 바로 TakeDamage에 int로 값을 받기 때문에 필요없어짐!

        // 데미지 작성
        int damage = data.power;

        // 데미지 전달
        target.TakeDamage(damage);

        // 카운트 감소
        target.ConsumeKeyword(KeywordType.Burn, 1);
    }
}
