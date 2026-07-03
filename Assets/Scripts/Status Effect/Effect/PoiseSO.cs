using UnityEngine;


[CreateAssetMenu(fileName = "PoiseSO", menuName = "Character/StatusEffect/Poise", order = int.MaxValue)]
public class PoiseSO : KeywordSO
{
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
    public bool IsCritical(CharacterBase attacker)
    {
        // 호흡 값 받아오기
        KeywordEffectRuntimeData data = attacker.GetKeyword(KeywordType.poise);
        bool isCir = (data.power * 5) > Random.Range(0, 100);

        return true;
    }
}
