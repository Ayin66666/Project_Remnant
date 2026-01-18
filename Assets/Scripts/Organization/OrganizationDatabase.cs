using System.Collections.Generic;
using UnityEngine;


public class OrganizationDatabase : MonoBehaviour
{

    [SerializeField] private List<OrganizationData> organizationList;


    /// <summary>
    /// 편성
    /// </summary>
    public void AddOrganization(OrganizationData data)
    {
        OrganizationData d = FindOrganizationData(data.identity.master.sinner);
        
        if (d == null) 
            organizationList.Add(data);
        else 
            Debug.LogWarning("편성 실패: 이미 편성된 캐릭터");
    }

    /// <summary>
    /// 편성 취소
    /// </summary>
    public void RemoveOrganization(CharacterId id)
    {
        OrganizationData data = FindOrganizationData(id);
        
        if (data != null) 
            organizationList.Remove(data);
        else 
            Debug.LogWarning("편성 취소 실패: 해당 캐릭터 없음");
    }

    /// <summary>
    /// 편성 여부 체크
    /// </summary>
    /// <param name="id"></param>
    public OrganizationData FindOrganizationData(CharacterId id)
    {
        OrganizationData data = organizationList.Find(x => x.identity.master.sinner == id);
        return data;
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
