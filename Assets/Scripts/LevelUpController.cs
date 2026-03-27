using Game.Character;
using UnityEngine;


public class LevelUpController : MonoBehaviour
{
    /*
     * 역할 : 상세정보 창 레벨업 & 동기화 업 관리
     * 데이터 위치
     *    - CharacterRosterManager (현제 레벨, 동기화, 보유 경험치 등)
     *    - LevelManager (레벨업에 필요한 경험치 총량)
     */
    [Header("---Setting---")]
    [SerializeField] private SinnerRuntimeData sinnerData;


    #region 기본 호출 로직
    /// <summary>
    /// 상세정보 UI 오픈 시 호출 - UI를 오픈하는 인격의 데이터 할당
    /// </summary>
    /// <param name="data"></param>
    public void SetUp(SinnerRuntimeData data)
    {
        sinnerData = data;
    }

    /// <summary>
    /// 리셋 함수 = 데이터 삭제용 (UI 종료 시 호출)
    /// </summary>
    public void Clear()
    {
        sinnerData = null;
    }
    #endregion


    #region UI
    /// <summary>
    /// 레벨업 후 UI 업데이트
    /// </summary>
    public void UpdataUI()
    {

    }
    #endregion


    #region Identity & Ego LevelUp
    /// <summary>
    /// 인격 레벨업 함수 - 버튼이 호출
    /// </summary>
    public void IdenLevelUp()
    {

    }

    /// <summary>
    /// 인격 동기화업 함수 - 버튼이 호출
    /// </summary>
    public void IdenSyncUp()
    {

    }

    /// <summary>
    /// 에고 동기화업 함수 - 버튼이 호출
    /// </summary>
    public void EgoSyncUp()
    {

    }
    #endregion
}
