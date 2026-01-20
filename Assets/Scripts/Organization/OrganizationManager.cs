using System;
using System.Collections.Generic;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    public static OrganizationManager instance;

    [Header("---Setting / Data---")]
    [SerializeField] private List<IdentityInfo> identityInfo;
    [SerializeField] private List<EgoInfo> egoInfo;

    [Header("---Organization---")]
    [SerializeField] private CharacterId curSinner;
    [SerializeField] private List<CharacterSlot> characterSlot;
    [SerializeField] private List<EgoEquipSlot> egoSlot;


    [Header("---UI---")]
    [SerializeField] private GameObject selectUI;
    [SerializeField] private GameObject identityListUI;
    [SerializeField] private GameObject egoListUI;


    [Header("---Component---")]
    [SerializeField] private SlotPooling pooling;


    [Header("---Test / 테스트 종료 후 삭제 예정!---")]
    [SerializeField] private IdentityData[] testInfo;


    #region 시작 데이터 세팅 로직
    private void Start()
    {
        instance = this;
        Application.targetFrameRate = 30;

        SetUpIdentityData();
        SetUpEgoData();
        SetUpOrganization();
    }

    /// <summary>
    /// 보유중인 인격 데이터 설정
    /// </summary>
    public void SetUpIdentityData()
    {
        identityInfo = new List<IdentityInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo egoInfo = new IdentityInfo();
            egoInfo.sinner = characterId;

            // 경로 지정 & 데이터 로드
            string path = $"Identity/{characterId.ToString().ToUpper()}";
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            egoInfo.info = new List<IdentityData>(masters.Length);
            foreach (IdentityMasterSO master in masters)
            {
                // Json 이 없을 경우 데이터 생성 로직임!
                IdentityData data = new IdentityData();
                data.isUnlocked = true;
                data.level = 1;
                data.sync = 1;
                data.master = master;

                egoInfo.info.Add(data);
            }

            identityInfo.Add(egoInfo);
        }
    }

    /// <summary>
    /// 보유중인 에고 데이터 설정
    /// </summary>
    public void SetUpEgoData()
    {
        egoInfo = new List<EgoInfo>();
        foreach(CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            EgoInfo egoInfo = new EgoInfo();
            egoInfo.sinner = characterId;

            string path = $"Ego/{characterId.ToString().ToUpper()}";
            EgoMasterSO[] masters = Resources.LoadAll<EgoMasterSO>(path);
            egoInfo.info = new List<EgoData>(masters.Length);
            foreach(EgoMasterSO master in masters)
            {
                EgoData data = new EgoData();
                data.isUnlocked = true;
                data.sync = 1;
                data.master = master;

                egoInfo.info.Add(data);
            }

            this.egoInfo.Add(egoInfo);
        }
    }

    /// <summary>
    /// 게임 시작 시 편성 데이터 로드 & 초기값 설정
    /// </summary>
    public void SetUpOrganization()
    {
        // 데이터가 있다면 - 로드

        // 데이터가 없다면 - 현재는 무조건 초기값 세팅
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
        IdentityInfo info1 = identityInfo.Find(x => x.sinner == id);
        for (int i = 0; i < info1.info.Count; i++)
        {
            if (info1.info[i].isUnlocked)
            {
                CharacterSlot slot = pooling.GetIdentitySlot();
                slot.SetUp(info1.info[i]);
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
    }
    #endregion


    #region 편성
    /// <summary>
    /// 편성 ego 변경
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingEgo(EgoData info)
    {

    }

    /// <summary>
    /// 편성 인격 변경하기
    /// </summary>
    /// <param name="info"></param>
    public void OrganizingIdentity(IdentityData info)
    {

    }
    #endregion
}


