using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CoinInfoSO", menuName = "Skill/CoinInfo", order = int.MaxValue)]
public class CoinInfoSO : ScriptableObject
{
    [Header("---Setting---")]
    /// <summary>
    /// 스킬 배율 (앞면 = X / 뒷면 = Y)
    /// </summary>
    [SerializeField] private Vector2 motionValue;
    /// <summary>
    /// 사용시, 적중시 같은 효과 발동 조건 데이터가 담긴 so
    /// </summary>
    [SerializeField] private List<EffectNode> effectNodes;

    public Vector2 MotionValue => motionValue;
    public List<EffectNode> EffectNodes => effectNodes;
}


