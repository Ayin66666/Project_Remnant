using Game.Character;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CharacterRosterManager : MonoBehaviour
{
    public static CharacterRosterManager instance;

    [Header("---편성 데이터---")]
    [SerializeField] private List<CharacterId> organizationOrderList;
    [SerializeField] private Dictionary<CharacterId, OrganizationData> organizationData;

    [Header("---보유 데이터---")]
    [SerializeField] private List<IdentityInfo> identityRuntimeData;
    [SerializeField] private List<EgoInfo> egoRuntimeData;

    [Header("---신규 런타임 데이터 (인격 & 에고 통합)---")]
    [SerializeField] private Dictionary<CharacterId, SinnerRuntimeData> runtimeInfo;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 데이터 로드
        CreateIdentityRuntimeData();
    }


    #region 런타임 데이터 생성
    /// <summary>
    /// 로더에서 데이터 획득 후 런타임 베이스 데이터 생성
    /// </summary>
    public void CreateIdentityRuntimeData()
    {
        // 데이터 로드
        IdentityDatabaseSO identityData = DataLoader.instance.IdentityDatabaseSO;
        EgoDatabaseSO egoData = DataLoader.instance.EgoDatabaseSO;

        runtimeInfo = 
            new Dictionary<CharacterId, SinnerRuntimeData>(identityData.SOContainers.Count);

        // 신규 데이터 생성 & 할당
        foreach(var data in identityData.SOContainers)
        {
            // 수감자 런타임 데이터 생성
            SinnerRuntimeData runtimeData = new SinnerRuntimeData()
            {
                sinner = data.Sinner,
                identityDic = new Dictionary<int, IdentityData>(),
                egoDic = new Dictionary<int, EgoData>()
            };

            // 수감자 전원의 인격 런타임 데이터 생성 & 할당
            foreach(var identity in data.so)
            {
                runtimeData.identityDic.Add(identity.identityId, new IdentityData(identity));
            }

            // 수감자 전원의 에고 런타임 데이터 생성 & 할당
            var matchingEgo = egoData.SOContainers.FirstOrDefault(e => e.Sinner == data.Sinner);
            if (matchingEgo != null)
            {
                foreach(var egoSO in matchingEgo.so)
                {
                    runtimeData.egoDic.Add(egoSO.egoId, new EgoData(egoSO));
                }
            }

            // 런타임 딕셔너리에 추가
            runtimeInfo.Add(data.Sinner, runtimeData);
        }

        foreach(var data in runtimeInfo)
        {
            Debug.Log($"데이터 체크 {data.Key}번 / {data.Value} 데이터");
        }
    }
    #endregion


    #region 세이브 파일 로드 & 신규 데이터 생성
    /// <summary>
    /// 생성된 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplySaveData(SaveData saveData)
    {
        /*
        // 세이브 데이터 덮어쓰기
        Dictionary<CharacterId, IdentityInfo> saveDict = saveData.ownedIdentity.ToDictionary(x => x.sinner);
        foreach (IdentityInfo loadData in identityRuntimeData)
        {
            // 수감자 선택
            IdentityInfo info;
            saveDict.TryGetValue(loadData.sinner, out info);
            if (info == null) continue;

            // 데이터 주입
            foreach (IdentityData runtimeData in loadData.info)
            {
                IdentityData save = info.info.Find(x => x.master.identityId == runtimeData.master.identityId);
                if (save != null)
                {
                    runtimeData.isUnlocked = save.isUnlocked;
                    runtimeData.level = save.level;
                    runtimeData.sync = save.sync;
                    runtimeData.curExp = save.curExp;
                }
            }
        }
        */

        
        foreach (var info in runtimeInfo.Values)
        {

        }
    }

    /// <summary>
    /// 생성된 기초 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplyEgoData(SaveData saveData)
    {
        Dictionary<CharacterId, EgoInfo> saveDict = saveData.ownedEgo.ToDictionary(x => x.sinner);
        foreach (EgoInfo loadData in egoRuntimeData)
        {
            // 수감자 선택
            EgoInfo info;
            saveDict.TryGetValue(loadData.sinner, out info);
            if (info == null) continue;

            // 데이터 주입
            foreach (EgoData runtimeData in loadData.info)
            {
                EgoData save = info.info.Find(x => x.master.egoId == runtimeData.master.egoId);
                if (save != null)
                {
                    runtimeData.isUnlocked = save.isUnlocked;
                    runtimeData.sync = save.sync;
                }
            }
        }
    }


    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 인격만 배치한 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<OrganizationData> CreatOrganizationData()
    {
        var data = identityRuntimeData
            .Select(x => new OrganizationData
            {
                sinner = x.sinner,
                identity = x.info[0],
                ego = new List<EgoData>()
            })
            .ToList();

        return data;
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 인격만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<IdentityInfo> CreateIdentityData()
    {
        // 1번 인격만 언락
        foreach (IdentityInfo character in identityRuntimeData)
        {
            if (character.info != null && character.info.Count > 0)
                character.info[0].isUnlocked = true;
        }

        return identityRuntimeData;
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 에고만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<EgoInfo> CreateEgoData()
    {
        foreach (EgoInfo ego in egoRuntimeData)
        {
            if (ego.info != null && ego.info.Count > 0)
                ego.info[0].isUnlocked = true;
        }

        return egoRuntimeData;
    }
    #endregion


    #region 세이브용 데이터 전달
    /// <summary>
    /// 편성 순서 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<CharacterId> GetOrganizationOrderData()
    {
        return organizationOrderList;
    }

    /// <summary>
    /// 각 수감자의 인격 & 에고 편성 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<OrganizationData> GetOrganiztionData()
    {
        /*
        List<OrganizationData> data = new List<OrganizationData>();
        int l = Enum.GetValues(typeof(CharacterId)).Length;
        for (int i = 0; i < l; i++)
        {
            data.Add(organizationData[(CharacterId)i]);
        }
        
        return data;
        */

        // Linq 동작하는지 테스트 필요!
        return organizationOrderList
            .Where(id => id != CharacterId.None && organizationData.ContainsKey(id))
            .Select(id => organizationData[id])
            .ToList();
    }

    /// <summary>
    /// 인격 보유 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<IdentityInfo> GetIdentityData()
    {
        return identityRuntimeData;
    }

    /// <summary>
    /// 에고 보유 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<EgoInfo> GetEgoInfo()
    {
        return egoRuntimeData;
    }
    #endregion


    #region 편성
    /// <summary>
    /// 선택한 인격 편성
    /// </summary>
    public void SetIdentity(IdentityData data)
    {
        // 혹시 모를 인격 미편성 체크
        bool haveData = organizationData.ContainsKey(data.master.sinner);
        if (haveData)
        {
            // 데이터 저장
            organizationData[data.master.sinner].identity = data;

            // 편성창 슬롯 업데이트
            OrganizationManager.instance.CharacterSlotUpdata(data);

            // 로그
            Debug.Log($"인격 편성 완료 / {organizationData[data.master.sinner].identity}");
        }
        else
        {
            // 모종의 이유로 수감자의 데이터가 제대로 설정되지 않았을 경우
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
    /// 편성된 인격 & 에고 데이터 전달
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public OrganizationData GetOrganizationData(CharacterId sinner)
    {
        if (organizationData.ContainsKey(sinner))
        {
            Debug.Log($"편성 데이터 있음 / {organizationData[sinner].identity}");
            return organizationData[sinner];
        }
        else
        {
            Debug.Log($"편성 데이터 없음 / {sinner}");
            return null;
        }
    }

    /// <summary>
    /// 지정된 인격의 보유 데이터 전달
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public IdentityInfo GetIdentityInfo(CharacterId sinner)
    {
        int index = identityRuntimeData.FindIndex(x => x.sinner == sinner);
        if (index != -1)
            return identityRuntimeData[index];
        else
        {
            Debug.Log($"인격 정보 없음 / {sinner}");
            return null;
        }
    }
    #endregion


    #region 편성 순서 로직
    public void OrganizationOrderSetting(CharacterId sinner)
    {
        // 1. 편성 여부 체크
        int index = organizationOrderList.FindIndex(x => x == sinner);
        if (index != -1)
        {
            // 편성중이라면 - 편성 해제
            organizationOrderList.Remove(sinner);
        }
        else
        {
            // 미편성이라면 - 편성
            organizationOrderList.Add(sinner);
        }
    }

    /// <summary>
    /// 편성 데이터 초기화
    /// </summary>
    public void ClearOrganizationOrder()
    {
        organizationOrderList.Clear();
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


/// <summary>
/// 신규 런타임 인격 보유 데이터
/// </summary>
[System.Serializable]
public class SinnerRuntimeData
{
    public CharacterId sinner;
    public Dictionary<int, IdentityData> identityDic;
    public Dictionary<int, EgoData> egoDic;
}

[System.Serializable]
public class IdentityData
{
    [Header("---Status---")]
    public bool isUnlocked;
    public int sync;
    public int level;
    public int curExp;

    [Header("---Reference---")]
    public IdentityMasterSO master;


    /// <summary>
    /// 생성자
    /// </summary>
    /// <param name="so"></param>
    public IdentityData(IdentityMasterSO so)
    {
        isUnlocked = false;
        sync = 1;
        level = 1;
        curExp = 0;
        master = so;
    }
}

