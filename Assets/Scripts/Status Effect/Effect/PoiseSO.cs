using UnityEngine;


[CreateAssetMenu(fileName = "PoiseSO", menuName = "Character/StatusEffect/Poise", order = int.MaxValue)]
public class PoiseSO : KeywordSO
{
    // 호흡의 경우 info 생성 시 IsCritical을 호출하여 동작
    // info 생성 위치는 SkillBase의 CreateInfo 함수에서 생성

    public override void Use(CharacterBase target)
    {
        // 호흡 감소
        target.ConsumeKeyword(KeywordType.poise);
    }

    /// <summary>
    /// 치명타 여부 체크 함수
    /// </summary>
    /// <param name="attacker"></param>
    /// <returns></returns>
    public static bool IsCritical(CharacterBase attacker)
    {
        // 호흡 값 받아오기
        KeywordEffectRuntimeData data = attacker.GetKeyword(KeywordType.poise);
        bool isCir = (data.power * 5) > Random.Range(0, 100);

        return true;
    }
}
