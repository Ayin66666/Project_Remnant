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
    [SerializeField] private List<CharacterSlot> characterSlot;
    [SerializeField] private List<EgoEquipSlot> egoSlot;
    private Dictionary<CharacterId, IdentityData> organizationData = new();
    private Dictionary<CharacterId, EgoData> egoOrganizationData = new();


    [Header("---UI---")]
    [SerializeField] private GameObject selectListUI;
    [SerializeField] private GameObject egoListUI;
    [SerializeField] private SlotPooling pooling;


    [Header("---Test / 테스트 종료 후 삭제 예정!---")]
    [SerializeField] private IdentityData[] testInfo;



    #region 시작 데이터 세팅 로직
    private void Start()
    {
        instance = this;
        Application.targetFrameRate = 30;

        SetUpIdentityData();
        SetUpOrganization();
    }

    /// <summary>
    /// 보유중인 인격 데이터 설정
    /// </summary>
    public void SetUpIdentityData()
    {
        identityInfo = new List<IdentityInfo>();
        foreach (CharacterId characterId in System.Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo egoInfo = new IdentityInfo();
            egoInfo.sinner = characterId;

            // 경로 지정
            string path = $"Identity/{characterId.ToString().ToUpper()}";

            // 경로 내 캐릭터 데이터 로드
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
    /// 게임 시작 시 편성 데이터 로드 & 초기값 설정
    /// </summary>
    public void SetUpOrganization()
    {
        // 데이터가 있다면 - 로드
        // 로직 구현


        // 데이터가 없다면 - 현재는 무조건 초기값 세팅(이때 초기값은 어디에?)
        organizationData.Clear();
        for (int i = 0; i < testInfo.Length; i++)
        {
            organizationData.Add(testInfo[i].master.sinner, testInfo[i]);
            characterSlot[i].SetUp(testInfo[i]);
            Debug.Log(testInfo[i].isUnlocked);
        }

        Debug.Log($"편성 데이터 설정 완료 / {organizationData.Count}");
    }
    #endregion


    #region 인격
    /// <summary>
    /// 캐릭터 클릭 -> 캐릭터 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenCharacterList(CharacterId id)
    {
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
        selectListUI.SetActive(true);
    }

    /// <summary>
    /// 캐릭터 리스트 닫기
    /// </summary>
    public void CloseIdentityList()
    {
        pooling.ClearIdentitySlot();
        selectListUI.SetActive(false);
    }

    /// <summary>
    /// 편성 인격 변경하기
    /// </summary>
    /// <param name="info"></param>
    public void ChangeIdentity(IdentityData info)
    {
        // 딕셔너리에 저장
        if (organizationData.ContainsKey(info.master.sinner))
        {
            organizationData[info.master.sinner] = info;
        }
        else
        {
            organizationData.Add(info.master.sinner, info);
        }
    }
    #endregion


    #region 에고
    /// <summary>
    /// 에고 클릭 -> 에고 리스트 오픈
    /// </summary>
    /// <param name="id"></param>
    public void OpenEgoList(CharacterId id, Rank rank)
    {
        // 데이터 세팅 - ego
        EgoInfo info2 = egoInfo.Find(x => x.sinner == id);
        for (int i = 0; i < egoOrganizationData.Count; i++)
        {
            // 에고 해금 여부 & 에고 티어가 동일한지 체크
            if (egoOrganizationData[id].isUnlocked && egoOrganizationData[id].master.egoRank == rank)
            {
                // 에고 추가
                EgoListSlot slot = pooling.GetEgoSlot();
                slot.SetUp(egoOrganizationData[id]);
                slot.gameObject.SetActive(true);
            }
        }
    }

    /// <summary>
    /// 편성 ego 변경
    /// </summary>
    /// <param name="info"></param>
    public void ChangeEgo(EgoData info)
    {
        if(egoOrganizationData.ContainsKey(info.master.sinner))
        {
            egoOrganizationData[info.master.sinner] = info;
        }
        else
        {
            egoOrganizationData.Add(info.master.sinner, info);
        }
    }
    #endregion
}


