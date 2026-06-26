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
    /// (value x 공격 레벨?)로 계산된 데미지를 기반으로 총 데미지 계산
    /// + 총 데미지를 attackEffect의 damagePercent로 나눠서 각 타격마다 데미지 부여
    /// </summary>
    [SerializeField] private List<HitInfo> hitDatas;
    /// <summary>
    /// 사용시, 적중시 같은 효과 발동 조건 데이터가 담긴 so
    /// </summary>
    [SerializeField] private List<EffectNode> effectNodes;

    public Vector2 MotionValue => motionValue;
    public List<HitInfo> HitDatas => hitDatas;
    public List<EffectNode> EffectNodes => effectNodes;


    [System.Serializable]
    /// <summary>
    /// 타격 횟수, 총 데미지 기준 비율(%) 데이터
    /// </summary>
    public struct HitInfo
    {
        [Header("---Hit Info---")]
        public int hitCount;
        public float damagePercent;
    }
}


