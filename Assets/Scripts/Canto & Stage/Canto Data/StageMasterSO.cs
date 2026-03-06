using System.Collections.Generic;
using UnityEngine;


namespace Game.Stage
{
    [CreateAssetMenu(fileName = "StageData_Canto00_Stage00", menuName = "Canto/Stage/StageManager", order = int.MaxValue)]
    public class StageMasterSO : ScriptableObject
    {
        [Header("---Setting---")]
        /// <summary>
        /// 스테이지 이름
        /// </summary>
        [SerializeField] private string stageName;
        /// <summary>
        /// 스테이지 번호 N-NN
        /// </summary>
        [SerializeField] private string stageOrder;
        /// <summary>
        /// 스테이지 이미지
        /// </summary>
        [SerializeField] private Sprite stageImage;
        /// <summary>
        ///  스테이지 진입 제약 조건
        /// </summary>
        [SerializeField] private List<StageLimitData> limitData;
        /// <summary>
        /// Ex 클리어 조건
        /// </summary>
        [SerializeField] private ExClearCondition exClearCondition;
        /// <summary>
        /// 등장 Enenmy 데이터
        /// </summary>
        [SerializeField] private List<EnemyWaveData> enemyWaveData;


        #region 프로퍼티
        public string StageName { get { return stageName; } }
        public string StageOrder { get { return stageOrder; } }
        public Sprite StageSprite { get { return stageImage; } }
        public ExClearCondition ExClearCondition { get { return exClearCondition; } }
        public List<StageLimitData> LimitData { get { return limitData; } }
        public List<EnemyWaveData> EnemyWaveData { get { return enemyWaveData; } }
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

    /// <summary>
    /// 스테이지 내 적 소환 웨이브 데이터
    /// </summary>
    public class EnemyWaveData
    {
        /// <summary>
        /// 개발자용 & 웨이브 이름 작성용 변수
        /// </summary>
        [SerializeField] private string waveName;
        /// <summary>
        /// 적 리스트
        /// </summary>
        public List<IdentityMasterSO> enemyList;
    }
    #endregion


    #region Public Enum
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