using UnityEngine;


[CreateAssetMenu(fileName = "StatSO", menuName = "Character/StatusEffect/StatSO", order = int.MaxValue)]
public class StatSO : EffectBaseSO
{
    [Header("---Stat Setting---")]
    [SerializeField] private CombatEffectType statType;

    /// <summary>
    /// 공격 포인트, 방어 포인트, 최종뎀증, 최종뎀감
    /// </summary>
    public enum CombatEffectType
    { 
        None, 
        AttackPoint, 
        DefensePoint, 
        FinalDamageDealt, 
        FinalDamageTaken,
    }



    public override void Use(CharacterBase target)
    {
        // 이펙트 동작 방식은 내가 어떤 동작을 해야하는지 알려주는 방식이 맞지 않나?
        // 아니면 값을 받아서 동작해야하나?

        // 고민중
        // Use를 호출받으면, 호출자가 던져준 정보를 기반으로 동작
        // 컨테이너의 데이터(대상의 스텟, 적용 수치)를 받아 동작
        // 대상에게 AddEffect() 함수 형태로 전달
        // -> 이때 AddEffect()는 so와 value만 받아서 동작
        // -> 데이터를 받은 뒤에 자신에게 so와 동일한 데이터가 있다면 거기 합산
        // -> 데이터가 없다면 새로운 데이터로 저장


        switch (statType)
        {
            case CombatEffectType.None:
                break;

            case CombatEffectType.AttackPoint:
                // 공격 포인트 + 값
                break;

            case CombatEffectType.DefensePoint:
                // 방어 포인트 + 값
                break;

            case CombatEffectType.FinalDamageDealt:
                // 최종 데미지 증가 + 값
                break;

            case CombatEffectType.FinalDamageTaken:
                // 받뎀감 + 값
                break;
        }
    }

}
