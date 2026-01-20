using System;
using System.Collections.Generic;
using UnityEngine;


public class OrganizationDatabase : MonoBehaviour
{
    [Header("---Data---")]
    [SerializeField] private List<OrganizationData> organizationList;


    public void SetUUp()
    {
        organizationList = new List<OrganizationData>();
        int length = Enum.GetValues(typeof(CharacterId)).Length;
        for (int i = 0; i < length; i++)
        {
            OrganizationData data = new OrganizationData();
            data.identity = null;
            data.ego = null;

            organizationList.Add(data);
        }
    }

    /// <summary>
    /// 편성
    /// </summary>
    public void AddOrganization(OrganizationData data)
    {

    }

    /// <summary>
    /// 편성 취소
    /// </summary>
    public void RemoveOrganization(CharacterId id)
    {

    }

    /// <summary>
    /// 편성 데이터 초기화
    /// </summary>
    public void ClaerData()
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
    public IdentityData identity;
    public List<EgoData> ego;
}
