using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class OrganizationDatabase : MonoBehaviour
{
    public static OrganizationDatabase instance;

    [Header("---편성 데이터---")]
    [SerializeField] private List<CharacterId> organizationOrderList;
    [SerializeField] private Dictionary<CharacterId, OrganizationData> organizationData;

    [Header("---보유 데이터---")]
    [SerializeField] private List<IdentityInfo> identityInfo;
    [SerializeField] private List<EgoInfo> egoInfo;


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

        // 데이터 로드
        LoadIdentityData();
        LoadEgoData();
    }


    #region 세이브 파일 로드 & 신규 데이터 생성
    /// <summary>
    /// 파일에서 SO 데이터를 읽은 후 런타임 데이터 생성
    /// </summary>
    public void LoadIdentityData()
    {
        // enum 기반 로드 방식 (구형)
        // 기본 데이터 로드
        identityInfo = new List<IdentityInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            // None은 건너뛰기 -> 임시로직
            if (characterId == CharacterId.None) continue;

            IdentityInfo identityInfo = new IdentityInfo();
            identityInfo.sinner = characterId;

            // 경로 지정 & 데이터 로드
            string path = $"Identity/{characterId.ToString().ToUpper()}";
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            identityInfo.info = new List<IdentityData>(masters.Length);
            foreach (IdentityMasterSO master in masters)
            {
                IdentityData data = new IdentityData()
                {
                    isUnlocked = false,
                    level = 1,
                    sync = 1,
                    master = master
                };
                identityInfo.info.Add(data);
            }
            this.identityInfo.Add(identityInfo);
        }
    }

    /// <summary>
    /// 파일에서 SO 데이터를 읽은 후 런타임 데이터 생성
    /// </summary>
    public void LoadEgoData()
    {
        egoInfo = new List<EgoInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            // None은 건너뛰기 -> 임시로직
            if (characterId == CharacterId.None) continue;

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
    /// 생성된 기초 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplyIdentityData(SaveData saveData)
    {
        // 세이브 데이터 덮어쓰기
        Dictionary<CharacterId, IdentityInfo> saveDict = saveData.ownedIdentity.ToDictionary(x => x.sinner);
        foreach (IdentityInfo loadData in identityInfo)
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
    }

    /// <summary>
    /// 생성된 기초 런타임 데이터에 세이브 데이터 주입
    /// </summary>
    /// <param name="saveData"></param>
    public void ApplyEgoData(SaveData saveData)
    {
        Dictionary<CharacterId, EgoInfo> saveDict = saveData.ownedEgo.ToDictionary(x => x.sinner);
        foreach (EgoInfo loadData in egoInfo)
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
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 인격만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<IdentityInfo> CreateIdentityData()
    {
        // 1번 인격만 언락
        foreach (IdentityInfo character in identityInfo)
        {
            if (character.info != null && character.info.Count > 0)
                character.info[0].isUnlocked = true;
        }

        return identityInfo;
    }

    /// <summary>
    /// 신규 데이터 생서 시 호출 - 각 인격의 첫번째 에고만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<EgoInfo> CreateEgoData()
    {
        foreach (EgoInfo ego in egoInfo)
        {
            if (ego.info != null && ego.info.Count > 0)
                ego.info[0].isUnlocked = true;
        }

        return egoInfo;
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
        int index = identityInfo.FindIndex(x => x.sinner == sinner);
        if (index != -1)
            return identityInfo[index];
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
