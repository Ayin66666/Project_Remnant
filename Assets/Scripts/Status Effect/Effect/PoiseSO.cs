using UnityEngine;


[CreateAssetMenu(fileName = "PoiseSO", menuName = "Character/StatusEffect/Poise", order = int.MaxValue)]
public class PoiseSO : KeywordSO
{
    public override void Use(CharacterBase target)
    {
        // 호흡 값 받아오기
        (int power, int count) = target.GetKeyword(KeywordType.poise);
        bool isCir = (power * 5) > Random.Range(0, 100);
    }
}
