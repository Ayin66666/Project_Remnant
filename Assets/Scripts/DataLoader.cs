using System;
using System.Collections.Generic;
using UnityEngine;


public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;

    [Header("---Character---")]
    private Dictionary<int, IdentityMasterSO> identitySO;
    public Dictionary<int, IdentityMasterSO> IdentitySO => identitySO;


    [Header("---Ego---")]
    

    [Header("---Stage---")]


    [Header("---Inventory---")]
    [SerializeField] private GameObject obj;
    public GameObject Obj => obj;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    /// <summary>
    /// 최초 1회 실행 - 파일에서 so 읽어오기
    /// </summary>
    public void LoadData()
    {
        // 인격
        LoadIdentityData();

        // 에고


        // 스테이지

        // 인벤토리
    }


    /// <summary>
    /// 인격 so 로드하기
    /// </summary>
    public void LoadIdentityData()
    {
        identitySO = new Dictionary<int, IdentityMasterSO>();

        IdentityMasterSO[] masters = Resources.LoadAll<IdentityMasterSO>("Identity");
        foreach (var master in masters)
        {
            if (identitySO.ContainsKey(master.identityId))
            {
                Debug.LogError($"Duplicate Identity ID : {master.identityId}");
                continue;
            }

            identitySO.Add(master.identityId, master);
        }
    }
}
