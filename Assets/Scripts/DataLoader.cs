using Game.Character;
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

    /*
    [Header("---Inventory---")]
    [SerializeField] private GameObject obj;
    */

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

        // 인벤토리
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
    #endregion
}
