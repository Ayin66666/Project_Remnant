using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrganizationDatabase : MonoBehaviour
{
    
    private Dictionary<int, IdentityInfo> roganizationData;


    /// <summary>
    /// 현재 던전에 들어가는 파티의 편성 데이터 저장
    /// </summary>
    public void AddData()
    {

    }

    /// <summary>
    /// 데이터 전송
    /// </summary>
    public void SendData()
    {

    }

    /// <summary>
    /// 데이터 초기화
    /// </summary>
    public void ClaerData()
    {
        roganizationData.Clear();
    }
}
