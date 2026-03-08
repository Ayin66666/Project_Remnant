using System.Collections.Generic;
using UnityEngine;


namespace Game.Character
{
    [CreateAssetMenu(fileName = "IdentityContainer", menuName = "Identity/IdentityContainer", order = int.MaxValue)]
    public class IdentityDatabaseSO : ScriptableObject
    {
        [Header("---Data---")]
        [SerializeField] private List<IdentitySOContainer> soContainers;
        public List<IdentitySOContainer> SOContainers => soContainers;
    }


    [System.Serializable]
    /// <summary>
    /// SO ¹­À½
    /// </summary>
    public class IdentitySOContainer
    {
        public CharacterId Sinner;
        public List<IdentityMasterSO> so;
    }
}