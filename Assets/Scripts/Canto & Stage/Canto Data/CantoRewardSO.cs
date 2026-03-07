using UnityEngine;


namespace Game.Canto
{
    [CreateAssetMenu(fileName = "Canto00_CantoReward00", menuName = "Canto/CantoReward", order = int.MaxValue)]

    public class CantoRewardSO : ScriptableObject
    {
        [Header("---Setting---")]
        public int rewardTerms;
        public int rewardValue;
    }
}
