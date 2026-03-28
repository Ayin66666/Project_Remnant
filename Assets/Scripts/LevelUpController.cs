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

    [Header("---UI---")]
    [SerializeField] private GameObject identityLevelUpUI;
    [SerializeField] private GameObject identitySyncUpUI;


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

    // UI 가 붙어있는 설명 UI는 어디까지나 DontDestoryOnLoad 에 붙어있기 때문에
    // 직접 할당은 불가능함 -> 이거 Description UI에 접근해서 받아오는 식으로 해야할듯?
    // 지금은 일단 테스트용이라 바로 붙임
    public void IdentityLevelUpUI(bool isOn)
    {
        identityLevelUpUI.SetActive(isOn);
    }

    public void IdentitySyncUpUI(bool isOn)
    {
        identitySyncUpUI.SetActive(isOn);
    }

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
