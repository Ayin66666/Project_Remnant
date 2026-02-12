using System;
using System.Collections.Generic;
using UnityEngine;


public class OrganizationDatabase : MonoBehaviour
{
    public static OrganizationDatabase instance;

    [Header("---편성 데이터---")]
    [SerializeField] private List<CharacterId> organizationList;
    [SerializeField] private Dictionary<CharacterId, OrganizationData> organizationData;

    [Header("---보유 데이터---")]
    [SerializeField] private List<IdentityInfo> identityInfo;
    [SerializeField] private List<EgoInfo> egoInfo;


    private void Awake()
    {
        instance = this;

        // 인격 & 에고 & 편성 데이터 로드
        SetUpIdentityData();
        SetUpEgoData();
        SetUpOrganizationData();
    }


    #region 인격 & 에고 데이터 로드
    /// <summary>
    /// 파일에서 인격 데이터 로드
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
    /// 파일에서 에고 데이터 로드
    /// </summary>
    public void SetUpEgoData()
    {
        egoInfo = new List<EgoInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            EgoInfo egoInfo = new EgoInfo();
            egoInfo.sinner = characterId;

            string path = $"Ego/{characterId.ToString().ToUpper()}";
            EgoMasterSO[] masters = Resources.LoadAll<EgoMasterSO>(path);
            egoInfo.info = new List<EgoData>(masters.Length);
            foreach (EgoMasterSO master in masters)
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
    /// 파일에서 편성 데이터 로드
    /// </summary>
    public void SetUpOrganizationData()
    {
        // 데이터 로드
        if (false)
        {
            // saveLoadManager로부터 데이터 로드
        }
        else
        {
            // 로드 데이터가 없다면 기본값 생성
            organizationData = new Dictionary<CharacterId, OrganizationData>();
            int length = Enum.GetValues(typeof(CharacterId)).Length;
            for (int i = 0; i < length; i++)
            {
                OrganizationData data = new OrganizationData();
                data.sinner = (CharacterId)i;
                data.identity = null;
                data.ego = new List<EgoData>();

                organizationData.Add((CharacterId)i, data);
            }
        }
    }
    #endregion


    #region 편성
    /// <summary>
    /// 선택한 인격 편성
    /// </summary>
    public void SetIdentity(IdentityData data)
    {
        bool haveData = organizationData.ContainsKey(data.master.sinner);
        if (haveData)
        {
            organizationData[data.master.sinner].identity = data;
        }
        else
        {
            // 모종의 이유로 수감자의 데이터가 제대로 편성되지 않았을 경우
            Debug.Log($"에러 발생 / 인격 데이터 설정 오류 / {data.master.sinner}");
        }
    }

    /// <summary>
    /// 선택한 에고 편성
    /// </summary>
    /// <param name="data"></param>
    public void SetEgo(EgoData data)
    {
        bool haveData = organizationData.ContainsKey(data.master.sinner);
        if (haveData)
        {
            int egoKey = organizationData[data.master.sinner].ego.FindIndex(x => x.master.egoRank == data.master.egoRank);
            organizationData[data.master.sinner].ego[egoKey] = data;
        }
        else
        {
            // 모종의 이유로 수감자의 데이터가 제대로 편성되지 않았을 경우
            Debug.Log($"에러 발생 / 인격 데이터 설정 오류 / {data.master.sinner}");
        }
    }
    #endregion


    #region 데이터 체크 & 전달
    /// <summary>
    /// 이미 순서가 편성되어 있는지 체크
    /// </summary>
    /// <param name="sinner"></param>
    public bool OrganizingCheck(CharacterId sinner)
    {
        int index = organizationList.FindIndex(x => x == sinner);
        if (index != -1) return true;
        else return false;
    }

    /// <summary>
    /// 편성된 인격 & 에고 데이터 전달
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public OrganizationData GetOrganizationData(CharacterId sinner)
    {
        if (organizationData.ContainsKey(sinner))
            return organizationData[sinner];
        else
        {
            Debug.Log($"편성 데이터 없음 / {sinner}");
            return null;
        }
    }
    #endregion


    #region 편성 순서 로직
    /// <summary>
    /// 지정 인격 편성
    /// </summary>
    /// <param name="sinner"></param>
    public void OrganizationOrderSetting(CharacterId sinner)
    {
        int index = organizationList.FindIndex(x => x == sinner);
        if (index != -1)
            organizationList.Add(sinner);
        else
            Debug.Log($"중복편성 오류 발생 / {sinner}, {index}");
    }

    /// <summary>
    /// 지정 인격의 편성 취소
    /// </summary>
    public void RemoveOrganizationOrder(CharacterId sinner)
    {
        int index = organizationList.FindIndex(x => x == sinner);
        if (index != -1)
            organizationList.Remove(sinner);
        else
            Debug.Log($"편성되지 않은 인격 / {sinner} / 체크 필요");

    }

    /// <summary>
    /// 편성 데이터 초기화
    /// </summary>
    public void ClearOrganizationOrder()
    {
        organizationList.Clear();
    }
    #endregion
}


/// <summary>
/// 편성 데이터 - 에고 & 인격 데이터를 저장함
/// </summary>
[System.Serializable]
public class OrganizationData
{
    public CharacterId sinner;
    public IdentityData identity;
    public List<EgoData> ego;
}
