using System.Collections.Generic;
using UnityEngine;


namespace Game.Character
{
    [CreateAssetMenu(fileName = "EnemyName_Type", menuName = "Enemy/EnemyMaster", order = int.MaxValue)]
    public class EnemyMasterSO : ScriptableObject
    {
        [Header("---Data---")]
        public EnemyType enemyType;
        /// <summary>
        /// 인격 Id - Id 규칙은 다음과 같음
        /// 1. 캐릭터 Id (2자리) + 인격 성급 (1자리) + 인격 번호 (3자리)
        /// 예시) ch1의 2성 1번째 인격 -> 012001 (읽을때는 01 2 001)
        /// </summary>
        public int enemyId;
        /// <summary>
        /// 인게임 전투에서 사용될 캐릭터의 프리팹
        /// </summary>
        public GameObject prefab;
        /// <summary>
        /// 스킬 데이터를 모아둔 리스트
        /// </summary>
        public List<SkillSO> skillData;
        /// <summary>
        /// 능력치 데이터 - 기초 능력치 및 레벨업 당 능력치 증가폭 등
        /// </summary>
        public StatusDataSO statData;

        [Header("---UI---")]
        // UI 데이터는 따로 so 사용 예정
        /// <summary>
        /// 편성창 UI 이미지 - 임시
        /// </summary>
        public Sprite portrait;
        /// <summary>
        /// 편성창 이름 - 임시
        /// </summary>
        public string identityName;
        /// <summary>
        /// 패시브 데이터를 모아둔 리스트
        /// </summary>
        public List<PassiveUIData> passiveUIData;

        // 정신력 데이터
        /// <summary>
        /// 정신력 아이콘
        /// </summary>
        public Sprite mentalityIcon;
        /// <summary>
        /// 정신력 이름
        /// </summary>
        public string mentalityName;
        /// <summary>
        /// 정신력 설명
        /// </summary>
        [TextArea] public string mentalityDescription;
    }

    public enum EnemyType
    {
        Normal,
        Boss
    }
}
