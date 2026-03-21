using Game.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;


public class CharacterRosterManager : MonoBehaviour
{
    public static CharacterRosterManager instance;

    [Header("---런타임 데이터---")]
    /// <summary>
    /// 편성 순서 데이터
    /// </summary>
    [SerializeField] private List<CharacterId> organizationOrder;
    public List<CharacterId> OrganizationOrder => organizationOrder;

    /// <summary>
    /// 편성 데이터
    /// </summary>
    [SerializeField] private Dictionary<CharacterId, OrganizationData> organizationData;
    /// <summary>
    /// 보유 데이터
    /// </summary>
    [SerializeField] private Dictionary<CharacterId, SinnerRuntimeData> runtimeInfo;

    [Header("---인스펙터 체크용---")]
    [SerializeField] private List<SinnerRuntimeData> runtimeinfoCheck;
    [SerializeField] private List<OrganizationData> organizationDataCheck;


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
        // 런타임 데이터 생성
        CreateRuntimeData();
    }


    #region 런타임 데이터 생성
    /// <summary>
    /// 런타임 데이터 생성
    /// </summary>
    public void CreateRuntimeData()
    {
        // 인격 & 에고 보유 데이터
        CreateIdentityRuntimeData();

        // 편성
        CreateOrganizationRuntimeData();

        // 편성 순서
        CreateOrganizationOrderData();

        // 데이터 체크용 리스트
        runtimeinfoCheck = runtimeInfo.Values.ToList();
        organizationDataCheck = organizationData.Values.ToList();
    }

    /// <summary>
    /// 인격 & 에고 런타임 베이스 데이터 생성
    /// </summary>
    private void CreateIdentityRuntimeData()
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
    }

    /// <summary>
    /// 편성 런타임 베이스 데이터 생성
    /// </summary>
    private void CreateOrganizationRuntimeData()
    {
        // 기본 편성은 기본 인격들만 배치되어 있는 상태로
        organizationData = new Dictionary<CharacterId, OrganizationData>();
        foreach(var container in DataLoader.instance.IdentityDatabaseSO.SOContainers)
        {
            OrganizationData data = new OrganizationData()
            {
                sinner = container.Sinner,
                identity = runtimeInfo[container.Sinner].identityDic[container.defaultIdentityId],
                ego = new List<EgoData>(5),
            };

            organizationData.Add(container.Sinner, data);
        }
    }

    /// <summary>
    /// 편성 순서 런타임 베이스 데이터 생성
    /// </summary>
    private void CreateOrganizationOrderData()
    {
        organizationOrder = new List<CharacterId>();
    }
    #endregion


    #region 세이브 파일 로드 & 신규 데이터 생성
    /// <summary>
    /// 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplySaveData(SaveData saveData)
    {
        // 인격 & 에고 보유 데이터
        foreach (var owned in saveData.ownedCharacterData)
        {
            if (!runtimeInfo.TryGetValue(owned.sinner, out var runtime))
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
                    // Debug.Log($"인격 데이터 덮어쓰기\n{runtimeIdentity.isUnlocked}\n{runtimeIdentity.sync}\n{runtimeIdentity.level}\n{runtimeIdentity.curExp}");
                }
            }

            // Ego
            foreach (var saveEgo in owned.ego)
            {
                if (runtime.egoDic.TryGetValue(saveEgo.egoId, out EgoData runtimeEgo))
                {
                    runtimeEgo.isUnlocked = saveEgo.isUnlock;
                    runtimeEgo.sync = saveEgo.sync;
                    // Debug.Log("에고 데이터 덮어쓰기");
                }
            }
        }

        // 편성 인격 & 에고 데이터
        foreach (var orData in saveData.organizationDatas)
        {
            Debug.Log($"orgData null? {organizationData == null}");
            Debug.Log($"key 존재? {organizationData.ContainsKey(orData.sinner)}");
            Debug.Log($"value null? {organizationData[orData.sinner] == null}");

            organizationData[orData.sinner].identity =
                runtimeInfo[orData.sinner].identityDic[orData.identityId];

            // 에고 데이터
            foreach (var id in orData.egoId)
            {
                EgoData ego = runtimeInfo[orData.sinner].egoDic[id];
                organizationData[orData.sinner].ego.Add(ego);
            }
        }

        // 편성 순서 데이터
        organizationOrder = saveData.organizationOrder;
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 / 인격 & 에고 보유 데이터 제작
    /// </summary>
    /// <returns></returns>
    public void InitializeDefaultState()
    {
        // 1번 인격 언락
        IdentityDatabaseSO idSO = DataLoader.instance.IdentityDatabaseSO;
        foreach (var frist in idSO.SOContainers)
        {
            runtimeInfo[frist.Sinner].identityDic[frist.so[0].identityId].isUnlocked = true;
        }

        // 1번 에고 언락
        EgoDatabaseSO egSO = DataLoader.instance.EgoDatabaseSO;
        foreach (var frist in egSO.SOContainers)
        {
            runtimeInfo[frist.Sinner].egoDic[frist.so[0].egoId].isUnlocked = true;
        }
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 / 편성 데이터 제작 (각 수감자의 기본 인격 배정)
    /// </summary>
    /// <returns></returns>
    public List<OrganizationSaveData> CreateSinnerOrganizationData()
    {
        // 편성 리스트 만들기 (세이브용) - so 컨테이너 so 에서 각 인격의 1번 데이터 호출
        List<OrganizationSaveData> list = new List<OrganizationSaveData>();
        foreach (var data in DataLoader.instance.IdentityDatabaseSO.SOContainers)
        {
            // 신규 편성 데이터 생성
            OrganizationSaveData newData = new OrganizationSaveData()
            {
                sinner = data.Sinner,
                identityId = data.so[0].identityId,
                egoId = new List<int>()
            };

            list.Add(newData);
        }

        // 런타임용 데이터 생성 (딕셔너리)
        organizationData = new Dictionary<CharacterId, OrganizationData>(list.Count);
        foreach (var data in list)
        {
            // 데이터 생성
            OrganizationData newData = new OrganizationData()
            {
                sinner = data.sinner,
                identity = runtimeInfo[data.sinner].identityDic[data.identityId],
                ego = new List<EgoData>()
            };

            // 딕셔너리에 추가
            organizationData.Add(data.sinner, newData);
        }

        // 데이터 반환
        return list;
    }
    #endregion


    #region 세이브용 데이터 전달
    /// <summary>
    /// 편성 순서 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<CharacterId> GetOrganizationOrderData()
    {
        return organizationOrder;
    }

    /// <summary>
    /// 세이브용 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<OrganizationSaveData> GetSinnerOrganiztionData()
    {
        // organizationData 를 list 형태로 가공해서 전달할 것!
        List<OrganizationSaveData> save = new List<OrganizationSaveData>();
        foreach (var runtime in organizationData.Values)
        {
            OrganizationSaveData data = new OrganizationSaveData()
            {
                sinner = runtime.sinner,
                identityId = runtime.identity.master.identityId,
                egoId = runtime.ego?.Select(id => id.master.egoId).ToList()
            };

            save.Add(data);
        }

        return save;
    }

    /// <summary>
    /// 수감자의 인격 & 에고 보유 데이터 전달
    /// </summary>
    /// <returns></returns>
    public List<OwnedSaveData> GetOwendData()
    {
        // 세이브 데이터 형태로 가공 후 전달
        List<OwnedSaveData> list = new List<OwnedSaveData>(runtimeInfo.Count);
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
        Debug.Log($"{data} / {data.master.sinner}");
        if (organizationData == null)
        {
            Debug.LogError("organizationData 자체가 null");
            return;
        }

        if (!organizationData.TryGetValue(data.master.sinner, out var orgData))
        {
            Debug.LogError($"key 없음: {data.master.sinner}");
            return;
        }

        Debug.Log(orgData == null ? "value null" : "정상");


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
    /// 해당 수감자의 보유 데이터 전달
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public SinnerRuntimeData GetIdentityData(CharacterId sinner)
    {
        return runtimeInfo[sinner];
    }

    // 이거 bool만 반환해도 될듯?
    /// <summary>
    /// 해당 수감자가 편성된 상태인지 (1~12번) 여부 데이터 전달
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public (bool, int) GetIdentityOrderData(CharacterId sinner)
    {
        int index = organizationOrder.IndexOf(sinner);
        if (index != -1) return (true, index);
        else return (false, -1);
    }

    /// <summary>
    /// 수감자의 편성 순서 가져오기
    /// </summary>
    /// <param name="sinner"></param>
    /// <returns></returns>
    public int GetIdentityOrder(CharacterId sinner)
    {
        int value = organizationOrder.FindIndex(x => x == sinner);
        Debug.Log(value == -1 ? "데이터 없음" : $"데이터 확인 {value}");
        return value;
    }
    #endregion


    #region 편성 순서 로직
    public void OrganizationOrderSetting(CharacterId sinner)
    {
        // 1. 편성 여부 체크
        int index = organizationOrder.FindIndex(x => x == sinner);
        Debug.Log($"Before: {string.Join(",", organizationOrder)}");
        if (index != -1)
        {
            // 편성중이라면 - 편성 해제
            Debug.Log("REMOVE");
            organizationOrder.Remove(sinner);
        }
        else
        {
            // 미편성이라면 - 편성
            Debug.Log("ADD");
            organizationOrder.Add(sinner);
        }

        Debug.Log($"After: {string.Join(",", organizationOrder)}");
    }

    /// <summary>
    /// 편성 데이터 초기화
    /// </summary>
    public void ClearOrganizationOrder()
    {
        organizationOrder.Clear();
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

