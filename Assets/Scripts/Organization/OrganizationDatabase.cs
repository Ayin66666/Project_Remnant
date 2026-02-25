using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;


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
    }


    #region 파일에서 인격 & 에고 데이터 읽어오기
    /// <summary>
    /// 파일에서 인격 데이터 로드
    /// </summary>
    public void SetUpIdentityData(SaveData saveData)
    {
        identityInfo = new List<IdentityInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo identityInfo = new IdentityInfo();
            identityInfo.sinner = characterId;

            // 경로 지정 & 데이터 로드
            string path = $"Identity/{characterId.ToString().ToUpper()}";
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            identityInfo.info = new List<IdentityData>(masters.Length);

            // Json 이 없을 경우 데이터 생성 로직임!
            foreach (IdentityMasterSO master in masters)
            {
                IdentityData data = new IdentityData();
                data.isUnlocked = true;
                data.level = 1;
                data.sync = 1;
                data.master = master;

                identityInfo.info.Add(data);

                Debug.Log($"데이터 생성 : {data.master}");
            }
            

            this.identityInfo.Add(identityInfo);
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
    #endregion


    #region 세이브 파일 로드 & 신규 데이터 생성
    /// <summary>
    /// 데이터 로드
    /// </summary>
    public void LoadData(SaveData data)
    {
        // 로드 데이터 전달해주기
        identityInfo = data.ownedIdentity;
        egoInfo = data.ownedEgo;
    }

    /// <summary>
    /// 신규 데이터 생성 시 호출 - 각 인격의 첫번째 인격만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<IdentityInfo> NewIdentityData()
    {
        List<IdentityInfo> newData = new List<IdentityInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            IdentityInfo identityInfo = new IdentityInfo();
            identityInfo.sinner = characterId;

            // 경로 지정 & 데이터 로드
            string path = $"Identity/{characterId.ToString().ToUpper()}";
            IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>(path);

            // 런타임 데이터 생성
            identityInfo.info = new List<IdentityData>(masters.Length);

            for (int i = 0; i < masters.Length; i++)
            {
                IdentityData data = new IdentityData()
                {
                    isUnlocked = i == 0,
                    level = 1,
                    sync = 1,
                    master = masters[i]
                };
            }

            newData.Add(identityInfo);
        }

        return newData;
    }

    /// <summary>
    /// 신규 데이터 생서 시 호출 - 각 인격의 첫번째 에고만 열린 상태로 보내줌
    /// </summary>
    /// <returns></returns>
    public List<EgoInfo> NewEgoData()
    {
        List<EgoInfo> newData = new List<EgoInfo>();
        foreach (CharacterId characterId in Enum.GetValues(typeof(CharacterId)))
        {
            EgoInfo egoInfo = new EgoInfo();
            egoInfo.sinner = characterId;

            string path = $"Ego/{characterId.ToString().ToUpper()}";
            EgoMasterSO[] masters = Resources.LoadAll<EgoMasterSO>(path);
            egoInfo.info = new List<EgoData>(masters.Length);
            for (int i = 0; i < masters.Length; i++)
            {
                EgoData data = new EgoData()
                {
                    isUnlocked = i == 0,
                    sync = 1,
                    master = masters[i]
                };

                egoInfo.info.Add(data);
            }

            newData.Add(egoInfo);
        }

        return newData;
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
