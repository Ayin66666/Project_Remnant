using UnityEngine;


[CreateAssetMenu(fileName = "EGO_CharacterName_Rank_EgoName", menuName = "Ego/EgoMaster", order = int.MaxValue)]
public class EgoMasterSO : ScriptableObject
{
    [Header("---Data---")]
    public CharacterId sinner;
    /// <summary>
    /// 에고 Id - Id 규칙은 다음과 같음
    /// 1. 캐릭터 Id (2자리) + 인격 성급 (1자리) + 인격 번호 (3자리)
    /// 예시) ch1의 2성 1번째 인격 -> 012001 (읽을때는 01 2 001)
    /// </summary>
    public string egoId;
    /// <summary>
    /// 인격의 성급 (1성 ~ 3성)
    /// </summary>
    public int egoRank;
    /// <summary>
    /// 인게임 전투에서 사용될 캐릭터의 프리팹
    /// </summary>
    public GameObject prefab;


    [Header("---UI---")]
    // UI 데이터는 따로 so 사용 예정
    /// <summary>
    /// 편성창 UI 이미지 - 임시
    /// </summary>
    public Sprite portrait;
    /// <summary>
    /// 편성창 이름 - 임시
    /// </summary>
    public string egoName;
}
