using System.Collections.Generic;
using UnityEngine;


namespace Game.Character
{
    [CreateAssetMenu(fileName = "EnemyName_Type", menuName = "Enemy/EnemyMaster", order = int.MaxValue)]
    public class EnemyMasterSO : ScriptableObject
    {
        // 인격 Id - Id 규칙은 다음과 같음
        // 1. 캐릭터 Id (2자리) + 인격 성급 (1자리) + 인격 번호 (3자리)
        // 예시) ch1의 2성 1번째 인격 -> 012001 (읽을때는 01 2 001)

        [Header("---Data---")]
        public EnemyType enemyType;
        public int enemyId;
        public GameObject prefab;
        public List<SkillSO> skillData;
        public StatusDataSO statData;

        [Header("---UI---")]
        public Sprite portrait;
        public string identityName;

        /// <summary>
        /// 패시브 데이터를 모아둔 리스트
        /// </summary>
        public List<PassiveUIData> passiveList;

        // 정신력 데이터
        public Sprite mentalityIcon;
        public string mentalityName;
        [TextArea] public string mentalityDescription;
    }

    public enum EnemyType
    {
        Normal,
        Boss
    }
}
