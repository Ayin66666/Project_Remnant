using System;
using System.Collections.Generic;
using UnityEngine;


public class OrganizationDatabase : MonoBehaviour
{
    public static OrganizationDatabase instance;

    [Header("---Data---")]
    [SerializeField] private List<CharacterId> organizationList;
    [SerializeField] private Dictionary<CharacterId, OrganizationData> organizationData;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetUp();
    }

    /// <summary>
    /// 게임 시작 시 최초 1회 호출 - 저장 데이터 체크 후 로드 or 기본 데이터 생성
    /// </summary>
    public void SetUp()
    {
        // 데이터 로드

        // 로드 데이터가 없다면 기본값 생성 - (기본값은 생성)
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


    /// <summary>
    /// 인격 편성
    /// </summary>
    public void SetOrganization(IdentityData data)
    {
        bool haveData = organizationData.ContainsKey(data.master.sinner);
        if (haveData)
        {
            organizationData[data.master.sinner].identity = data;
        }
        else
        {
            // 모종의 이유로 수감자의 데이터가 제대로 편성되지 않았을 경우 -> 일단 기본값 세팅
            OrganizationData d = new OrganizationData();
            d.sinner = data.master.sinner;
            d.identity = data;
            d.ego = new List<EgoData>();
            organizationData.Add(d.sinner, d);

            Debug.Log($"에러 발생 / 인격 데이터 설정 오류 / {data.master.sinner}");
        }
    }

    /// <summary>
    /// 에고 편성
    /// </summary>
    /// <param name="data"></param>
    public void SetOrganization(EgoData data)
    {
        bool haveData = organizationData.ContainsKey(data.master.sinner);
        if (haveData)
        {
            int egoKey = organizationData[data.master.sinner].ego.FindIndex(x => x.master.egoRank == data.master.egoRank);
            organizationData[data.master.sinner].ego[egoKey] = data;
        }
        else
        {
            // 모종의 이유로 수감자의 데이터가 제대로 편성되지 않았을 경우 -> 일단 기본값 세팅
            OrganizationData d = new OrganizationData();
            d.sinner = data.master.sinner;
            d.identity = null;
            d.ego = new List<EgoData>();
            d.ego.Add(data);
            organizationData.Add(d.sinner, d);

            Debug.Log($"에러 발생 / 인격 데이터 설정 오류 / {data.master.sinner}");
        }
    }

    public void Organizing(CharacterId sinner)
    {
        int index = organizationList.FindIndex(x => x == sinner);
        if (index == -1)
            organizationList.Add(sinner);
        else
            Debug.Log($"중복편성 오류 발생 / {sinner}, {index}");
    }

    /// <summary>
    /// 편성 취소
    /// </summary>
    public void RemoveOrganization(CharacterId id)
    {
        Debug.Log($"편성되지 않은 인격 / {id} / 체크 필요");
    }

    /// <summary>
    /// 편성 데이터 초기화
    /// </summary>
    public void ClearData()
    {
        organizationList.Clear();
    }
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
