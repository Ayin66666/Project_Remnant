using System.Collections.Generic;
using UnityEngine;
using Game.Character;


namespace Game.Stage
{
    [CreateAssetMenu(fileName = "StageData_Canto00_Stage00", menuName = "Canto/Stage/StageMasterSO", order = int.MaxValue)]
    public class StageMasterSO : ScriptableObject
    {
        [Header("---Setting---")]
        /// <summary> 
        /// 스테이지 id
        /// </summary>>
        [SerializeField] private int stageId;
        /// <summary>
        /// 진입할 스테이지의 타입 (메인, 배틀, 튜토리얼)
        /// </summary>
        [SerializeField] private StageType stageType;
        /// <summary>
        /// 스테이지 이름
        /// </summary>
        [SerializeField] private string stageName;
        /// <summary>
        /// 스테이지 레벨
        /// </summary>
        [SerializeField] private int stageLevel;
        /// <summary>
        /// 스테이지 번호 N-NN
        /// </summary>
        [SerializeField] private string stageOrder;
        /// <summary>
        /// 스테이지 이미지
        /// </summary>
        [SerializeField] private Sprite stageImage;
        /// <summary>
        /// 유효 속성
        /// </summary>
        [SerializeField] private List<AttackType> validAttack;
        [SerializeField] private List<Sin> validCrimes;
        /// <summary>
        ///  스테이지 진입 제약 조건
        /// </summary>
        [SerializeField] private List<StageLimitData> limitData;
        /// <summary>
        /// Ex 클리어 조건
        /// </summary>
        [SerializeField] private ExClearCondition exClearCondition;
        /// <summary>
        /// 등장 적 프리뷰 데이터 - 스테이지 선택 시 초상화로 보여줄 등장 적 데이터
        /// </summary>
        [SerializeField] private List<EnemyMasterSO> previewEnemies;
        /// <summary>
        /// 인게임 전투 스테이지의 배치 및 몬스터 데이터 SO
        /// </summary>
        [SerializeField] private BattleStageSO battleStageSO;

        #region 프로퍼티
        public int Stageid => stageId;
        public StageType StageType => stageType;
        public string StageName => stageName;
        public int StageLevel => stageLevel;
        public List<AttackType> ValidAttack => validAttack;
        public List<Sin> ValidCrimes => validCrimes;
        public string StageOrder => stageOrder;
        public Sprite StageSprite => stageImage;
        public ExClearCondition ExClearCondition => exClearCondition;
        public List<StageLimitData> LimitData => limitData;
        public List<EnemyMasterSO> EnemyData => previewEnemies;
        public BattleStageSO BattleStageSO => battleStageSO;

        #endregion
    }


    #region DataClass
    [System.Serializable]
    /// <summary>
    /// Ex클리어 조건 여부
    /// </summary>>
    public class ExClearCondition
    {
        public ExClear ConditionType;
        public int conditionCount;
        [SerializeField, TextArea] private string conditionDescription;
    }

    [System.Serializable]
    /// <summary>
    /// 스테이지 진입 시 제한사항
    /// </summary>
    public class StageLimitData
    {
        /// <summary>
        /// 제한 타입
        /// </summary>
        public StageLimit stageLimit;
        /// <summary>
        /// 필수 편성 & 제외 수감자
        /// </summary>
        public CharacterId sinner;
        /// <summary>
        /// 가용 가능 수감자 수
        /// </summary>
        public int count;
    }
    #endregion


    #region Public Enum
    public enum StageType
    {
        Main,
        Battle,
        Tutorial
    }

    /// <summary>
    /// Ex 클리어를 위한 조건 enum
    /// </summary>
    public enum ExClear
    {
        StageClear,
        TurnLimit,
        NoDied
    }

    /// <summary>
    /// 스테이지 진입 전 제한 조건
    /// </summary>
    public enum StageLimit
    {
        None,
        MustUse,
        CantUse,
        OrganizationLimit
    }

    /// <summary>
    /// 스테이지 클리어 타입 체크 (클리어X, 일반, Ex)
    /// </summary>
    public enum StageClearType
    {
        None,
        Clear,
        ExClear
    }
    #endregion
}