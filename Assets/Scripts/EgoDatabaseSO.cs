using System.Collections.Generic;
using UnityEngine;


namespace Game.Character
{
    [CreateAssetMenu(fileName = "EgoContainer", menuName = "EGO/EgoContainer", order = int.MaxValue)]
    public class EgoDatabaseSO : ScriptableObject
    {
        [Header("---Data---")]
        [SerializeField] private List<EgoSOContainer> soContainers;
        public List<EgoSOContainer> SOContainers => soContainers;
    }


    [System.Serializable]
    /// <summary>
    /// SO ¹­À½
    /// </summary>
    public class EgoSOContainer
    {
        public CharacterId Sinner;
        public List<EgoMasterSO> so;
    }
}