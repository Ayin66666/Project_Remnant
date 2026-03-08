using System.Collections.Generic;
using UnityEngine;
using Game.Character;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;

    [Header("---Character---")]
    private Dictionary<int, IdentityMasterSO> identitySO;

    [Header("---Ego---")]
    private Dictionary<int, EgoMasterSO> egoSO;

    [Header("---Stage---")]


    [Header("---Inventory---")]
    [SerializeField] private GameObject obj;


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
        LoadEgoData();

        // 스테이지

        // 인벤토리
    }


    #region 인격
    /// <summary>
    /// 인격 so 로드하기
    /// </summary>
    public void LoadIdentityData()
    {
        // 데이터 로드
        identitySO = new Dictionary<int, IdentityMasterSO>();
        IdentityDatabaseSO databaseSO = Resources.Load<IdentityDatabaseSO>("Identity");
        if(databaseSO == null)
        {
            Debug.LogError("인격 SO 로드 실패!");
            return;
        }

        // 딕셔너리 할당
        foreach (var container in databaseSO.SOContainers)
        {
            for (int i = 0; i < container.so.Count; i++)
            {
                if (identitySO.ContainsKey(container.so[i].identityId))
                {
                    Debug.LogError($"인격 SO 딕셔너리 할당 중 중복 오류 발생 / {container.so[i].identityId}");
                    continue;
                }

                identitySO.Add(container.so[i].identityId, container.so[i]);
            }
        }
    }

    /// <summary>
    /// SO 전달
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public IdentityMasterSO GetIdentity(int id)
    {
        return identitySO[id];
    }

    /// <summary>
    /// 전체 데이터 전달
    /// </summary>
    /// <returns></returns>
    public Dictionary<int, IdentityMasterSO> GetAllIdentity()
    {
        return identitySO;
    }
    #endregion


    #region 에고
    public void LoadEgoData()
    {

    }

    public EgoMasterSO GetEgo(int id)
    {
        return egoSO[id];
    }

    public Dictionary<int, EgoMasterSO> GetAllEgo()
    {
        return egoSO;
    }
    #endregion


    #region 스테이지
    #endregion
}
