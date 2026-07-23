using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveSO", menuName = "Character/PassiveSO", order = int.MaxValue)]
public class PassiveSO : ScriptableObject
{
    [Header("---Passive Data---")]
    [SerializeField] private string passiveName;
    [SerializeField, TextArea] private string passiveDescription;
    [SerializeField] private List<PassiveData> passiveList;

    public string PassiveName => passiveName;
    public string PassiveDescription => passiveDescription;
    public List<PassiveData> PassiveList => passiveList;


    [System.Serializable]
    /// <summary>
    /// 패시브 데이터 (트리거 타입, 패시브 아이디, 액션 벨류)
    /// </summary>
    public struct PassiveData
    {
        [Header("---Passive---")]
        [SerializeField] private TriggerType triggerType;
        [SerializeField] private int passiveId;
        [SerializeField] private List<ActionNode> actionList;

        public TriggerType Trigger => triggerType;
        public int PassiveId => passiveId;
        public List<ActionNode> ActionList => actionList;
    }
}
