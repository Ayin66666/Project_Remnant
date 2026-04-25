using Game.Character;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(fileName = "BattleStageSO", menuName = "Canto/Stage/BattleStageSO", order = int.MaxValue)]
public class BattleStageSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private StageType stageType;
    [SerializeField] private List<PhaseData> phaseData;

    public enum StageType
    {
        Normal,
        Boss
    }

    public enum NextCondition
    {
        DefeatAllEnemies,
        SurviveNTurns
    }

    [System.Serializable]
    public class PhaseData
    {
        [Header("---Setting---")]
        public NextCondition nextCondition;
        public int surviveTurnNum;
        public List<BattleStageEventSO> eventList;

        [Header("---Enemy Data---")]
        public List<SpawnData> enemies;

        [Header("---Post Processing---")]
        public VolumeProfile postProcessingProfile;

        [Header("---Background Data---")]
        public Sprite floor;
        public Sprite wall;
        public List<Sprite> background;
        public AudioClip phaseBgm;
    }

    public StageType Type => stageType;
    public List<PhaseData> PhaseDataList => phaseData;
}

[System.Serializable]
public class SpawnData
{
    public enum SpawnType
    {
        Random,
        Fixed
    }

    [Header("---Enemy Data---")]
    public SpawnType spawnType;
    public int spawnNum;
    public int level;
    public IdentityMasterSO enemy;
}


