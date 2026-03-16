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
        foreach (var data in identityData.SOContainers)
        {
            // 수감자 런타임 데이터 생성
            SinnerRuntimeData runtimeData = new SinnerRuntimeData()
            {
                sinner = data.Sinner,
                identityDic = new Dictionary<int, IdentityData>(),
                egoDic = new Dictionary<int, EgoData>()
            };

            // 수감자 전원의 인격 런타임 데이터 생성 & 할당
            foreach (var identity in data.so)
            {
                runtimeData.identityDic.Add(identity.identityId, new IdentityData(identity));
            }

            // 수감자 전원의 에고 런타임 데이터 생성 & 할당
            var matchingEgo = egoData.SOContainers.FirstOrDefault(e => e.Sinner == data.Sinner);
            if (matchingEgo != null)
            {
                foreach (var egoSO in matchingEgo.so)
                {
                    runtimeData.egoDic.Add(egoSO.egoId, new EgoData(egoSO));
                }
            }

            // 런타임 딕셔너리에 추가
            runtimeInfo.Add(data.Sinner, runtimeData);
        }

        foreach (var data in runtimeInfo)
        {
            Debug.Log($"데이터 체크 {data.Key}번 / {data.Value} 데이터");
        }
    }
    #endregion


    #region 세이브 파일 로드 & 신규 데이터 생성
    /// <summary>
    /// 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplySaveData(SaveData saveData)
    {
        foreach (var owned in saveData.ownedCharacterData)
        {
            if (runtimeInfo.TryGetValue(owned.sinner, out var runtime))
                continue;

            // Identity
            foreach (var saveIdentity in owned.identity)
            {
                if (runtime.identityDic.TryGetValue(saveIdentity.identityId, out IdentityData runtimeIdentity))
                {
                    runtimeIdentity.isUnlocked = saveIdentity.isUnlock;
                    runtimeIdentity.sync = saveIdentity.sync;
                    runtimeIdentity.level = saveIdentity.level;
                    runtimeIdentity.curExp = saveIdentity.curExp;
                }
            }

            // Ego
            foreach (var saveEgo in owned.ego)
            {
                if (runtime.egoDic.TryGetValue(saveEgo.egoId, out EgoData runtimeEgo))
                {
                    runtimeEgo.isUnlocked = saveEgo.isUnlock;
                    runtimeEgo.sync = saveEgo.sync;
                }
            }
        }
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 / 편성 데이터 제작 (각 수감자의 기본 인격 배정)
    /// </summary>
    /// <returns></returns>
    public List<OrganizationData> CreatOrganizationData()
    {
        // 편성 리스트 만들기 (세이브용) - so 컨테이너 so 에서 각 인격의 1번 데이터 호출
        List<OrganizationData> organizationData = new List<OrganizationData>();
        IdentityDatabaseSO so = DataLoader.instance.IdentityDatabaseSO;
        foreach (var soContainer in so.SOContainers)
        {
            if (soContainer.so.Count == 0) 
                continue;

            int baseId = soContainer.so[0].identityId;
            organizationData.Add(new OrganizationData()
            {
                sinner = soContainer.Sinner,
                identity = runtimeInfo[soContainer.Sinner].identityDic[baseId],
                ego = new List<EgoData>()

            });
        }

        // 딕셔너리에 데이터 넣기(런타임용)


        // 반환
        return organizationData;
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 / 인격 & 에고 보유 데이터 제작
    /// </summary>
    /// <returns></returns>
    public List<OwnedSaveData> CreatData()
    {
        // 신규 데이터 생성
        CreateIdentityData();
        CreateEgoData();

        // 세이브 데이터 형태 (List<OwnedSaveData>) 로 변환 후 전달
        return GetOwendData();
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 인격만 열린 상태로 변경
    /// </summary>
    /// <returns></returns>
    private void CreateIdentityData()
    {
        // 1번 인격만 언락
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 에고만 열린 상태로 변경
    /// </summary>
    /// <returns></returns>
    private void CreateEgoData()
    {
        // 1번 에고만 언락
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
        // Linq 동작하는지 테스트 필요!
        return organizationOrderList
            .Where(id => id != CharacterId.None && organizationData.ContainsKey(id))
            .Select(id => organizationData[id])
            .ToList();
    }

    /// <summary>
    /// 수감자의 인격 & 에고 보유 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<OwnedSaveData> GetOwendData()
    {
        // 세이브 데이터 형태로 가공 후 전달
        List<OwnedSaveData> list = new List<OwnedSaveData>();
        foreach (var runtime in runtimeInfo.Values)
        {
            // 인격 데이터 전환
            List<OwnedSaveData.Identity> identity = new List<OwnedSaveData.Identity>();
            foreach (var iData in runtime.identityDic.Values)
            {
                identity.Add(new OwnedSaveData.Identity
                {
                    isUnlock = iData.isUnlocked,
                    identityId = iData.master.identityId,
                    curExp = iData.curExp,
                    level = iData.level,
                    sync = iData.sync,
                });
            }

            // 에고 데이터 전환
            List<OwnedSaveData.Ego> ego = new List<OwnedSaveData.Ego>();
            foreach (var eData in runtime.egoDic.Values)
            {
                ego.Add(new OwnedSaveData.Ego
                {
                    isUnlock = eData.isUnlocked,
                    egoId = eData.master.egoId,
                    sync = eData.sync,
                });
            }

            // 데이터 삽입
            OwnedSaveData data = new OwnedSaveData
            {
                sinner = runtime.sinner,
                identity = identity,
                ego = ego,
            };

            // 전체 리스트에 추가
            list.Add(data);
        }

        return list;
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
        return null;
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

