using System.Collections.Generic;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager instance;


    [Header("---현제 편성중인 캐릭터의 데이터---")]
    [SerializeField] private CharacterId curSinner;
    [SerializeField] private List<CharacterSlot> characterSlot;
    [SerializeField] private List<EgoEquipSlot> egoSlot;

    [Header("---UI---")]
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject identityListUI;
    [SerializeField] private GameObject egoListUI;

    [Header("---Component---")]
    [SerializeField] private SlotPooling pooling;


    #region 시작 데이터 세팅 로직
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        Application.targetFrameRate = 30;
        SetUpOrganization();
    }


    /// <summary>
    /// 게임 시작 시 편성 데이터 로드 & 초기값 설정
    /// </summary>
    public void SetUpOrganization()
    {
        // 데이터가 있다면 - 로드
        if (false)
        {
            // OrganizationDatabase.instance.LoadData();
        }
        else
        {
            // 데이터가 없다면 - 현재는 무조건 초기값 세팅
        }
    }
    #endregion


    #region 인격
    /// <summary>
    /// 캐릭터 클릭 -> 캐릭터 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenCharacterList(CharacterId id)
    {
        curSinner = id;

        // 데이터 세팅 - 인격
        IdentityInfo info1 = OrganizationDatabase.instance.GetIdentityInfo(id);
        Debug.Log(info1);
        if (info1 == null)
        {
            Debug.Log("인격 정보 없음");
            return;
        }

        // 슬롯에 데이터 전달
        for (int i = 0; i < info1.info.Count; i++)
        {
            if (info1.info[i].isUnlocked)
            {
                CharacterSelectSlot slot = pooling.GetIdentitySlot();

                // 여기 일단은 false로 넣긴 하는데, 원래는 편성 여부 체크해서 넣어야 함!
                slot.SetUp(info1.info[i], false); 
                slot.gameObject.SetActive(true);
            }
        }

        // UI 오픈
        selectUI.SetActive(true);
        identityListUI.SetActive(true);
        egoListUI.SetActive(false);
    }

    /// <summary>
    /// 리스트 닫기
    /// </summary>
    public void CloseIdentityList()
    {
        pooling.ClearIdentitySlot();
        pooling.ClearEgoSlot();

        egoListUI.SetActive(false);
        selectUI.SetActive(false);
        identityListUI.SetActive(false);
    }
    #endregion


    #region 에고
    /// <summary>
    /// 에고 클릭 -> 에고 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenEgoList(Rank rank)
    {
        // 리스트 클리어
        pooling.ClearEgoSlot();
        /*
        // 데이터 세팅 - ego
        EgoInfo info2 = egoInfo.Find(x => x.sinner == curSinner);
        Debug.Log(info2);
        Debug.Log(info2.info.Count);

        for (int i = 0; i < info2.info.Count; i++)
        {
            // 에고 해금 여부 & 에고 티어가 동일한지 체크
            if (info2.info[i].isUnlocked && info2.info[i].master.egoRank == rank)
            {
                // 에고 추가
                EgoListSlot slot = pooling.GetEgoSlot();
                slot.SetUp(info2.info[i]);
                slot.gameObject.SetActive(true);
            }
        }

        egoListUI.SetActive(true);
        selectUI.SetActive(true);
        identityListUI.SetActive(false);
        */
    }
    #endregion


    #region 편성
    /// <summary>
    /// 편성 인격 변경하기
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingIdentity(IdentityData info)
    {
        // 데이터베이스로 전달
        OrganizationDatabase.instance.SetIdentity(info);
    }

    /// <summary>
    /// 편성 ego 변경
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingEgo(EgoData info)
    {
        // 데이터베이스로 전달
        OrganizationDatabase.instance.SetEgo(info);
    }
    #endregion
}


