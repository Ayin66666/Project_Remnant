using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoinInfoSO", menuName = "Character/Skill/CoinInfo", order = int.MaxValue)]
public class CoinInfoSO : ScriptableObject
{
    // 이거 전체 motionValue는 skillso에만 있고,
    // coininfo에는 총 데미지 기준 해당 코인으로 넣을 데미지 배율만 있는 게 맞지 않나?
    // 앞면 뒷면에 따른 데미지 변동은 앞면 1.0, 뒷면 0.65로 고정 세팅이니 따로 값 넣을 필요가 없어보이는데

    // 26.07.24 : motionValue 제거함! => 해당 값은 SkillSO로 이동했고,
    // 대신 해당 코인이 총 데미지에서 몇 %의 데미지를 줄지 세팅하는 값으로 교체함

    [Header("---Setting---")]
    /// <summary>
    /// 해당 코인이 전체 데미지 중 몇 %의 데미지를 주는지
    /// </summary>
    [SerializeField] private int damagePercent;
    /// <summary>
    /// 사용시, 적중시 같은 효과 발동 조건 데이터가 담긴 so
    /// </summary>
    [SerializeField] private List<EffectNode> effectNodes;

    public int DamagePercent => damagePercent;
    public List<EffectNode> EffectNodes => effectNodes;
}


