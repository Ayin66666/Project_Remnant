using Game.Character;
using Item;
using System.Collections.Generic;
using UnityEngine;


public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;

    [Header("---Character---")]
    [SerializeField] private IdentityDatabaseSO identitySO;
    public IdentityDatabaseSO IdentityDatabaseSO => identitySO;

    [Header("---Ego---")]
    [SerializeField] private EgoDatabaseSO egoSO;
    public EgoDatabaseSO EgoDatabaseSO => egoSO;

    [Header("---Stage---")]
    [SerializeField] private CantoDatabaseSO cantoSO;
    public CantoDatabaseSO CantoDatabaseSO => cantoSO;

    [Header("---Item---")]
    [SerializeField] private ItemSOContainer itemSOcontainer;
    private Dictionary<int, ItemSO> itemDic;
    public Dictionary<int, ItemSO> ItemDic => itemDic;


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
        LoadData();
    }

    /// <summary>
    /// 최초 1회 실행 - 파일에서 so 읽어오기
    /// </summary>
    private void LoadData()
    {
        // 인격
        LoadIdentityData();

        // 에고
        LoadEgoData();

        // 스테이지
        LoadStageData();

        // 아이템
        LoadIventoryData();
    }


    #region SO 로드
    /// <summary>
    /// 인격 so 로드하기
    /// </summary>
    private void LoadIdentityData()
    {
        // 데이터 로드
        identitySO = Resources.Load<IdentityDatabaseSO>("Identity/IdentityContainer");
        if (identitySO == null)
        {
            Debug.LogError("인격 SO 로드 실패!");
            return;
        }

        // Debug.Log("인격 SO 로드 종료 / 성공");
    }

    /// <summary>
    /// 에고 so 로드하기
    /// </summary>
    private void LoadEgoData()
    {
        egoSO = Resources.Load<EgoDatabaseSO>("Ego/EgoContainer");
        if (egoSO == null)
        {
            Debug.LogError("에고 SO 로드 실패!");
            return;
        }

        // Debug.Log("에고 SO 로드 종료 / 성공");
    }

    /// <summary>
    /// 칸토 so 로드하기
    /// </summary>
    private void LoadStageData()
    {
        cantoSO = Resources.Load<CantoDatabaseSO>("Canto/CantoDatabase");
        if (cantoSO == null)
        {
            Debug.LogError("칸토 SO 로드 실패!");
            return;
        }

        // Debug.Log("칸토 SO 로드 종료 / 성공");
    }

    /// <summary>
    /// 아이템 so 로드 & so 딕셔너리화
    /// </summary>
    private void LoadIventoryData()
    {
        itemSOcontainer = Resources.Load<ItemSOContainer>("Item/ItemSOContainer");
        if (itemSOcontainer == null)
        {
            Debug.LogError("아이템 SO 로드 실패!");
            return;
        }

        itemDic = new Dictionary<int, ItemSO>();
        foreach (var itemList in itemSOcontainer.AllItems)
        {
            foreach (var item in itemList)
            {
                if (itemDic.ContainsKey(item.ItemID))
                    Debug.LogError($"아이템 ID 중복 발견! ID: {item.ItemID}, 이름: {item.ItemName}");

                else
                    itemDic.Add(item.ItemID, item);
            }
        }
    }
    #endregion
}
