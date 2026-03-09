using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Game.Character;


[System.Serializable]
public class SaveData
{
    /* 저장해야하는 데이터
     1. 게임 버전
     2. 튜토리얼 진행 여부
     3. 인격 & 에고 & 편성 데이터
     4. 인벤토리
     5. 스테이지 클리어 & 진행도
    */

    [Header("---버전---")]
    public string version;

    [Header("---튜토리얼---")]
    public bool playTutorial;

    [Header("---인격 & 에고 & 편성---")]
    public List<OrganizationData> organizationDatas;
    public List<CharacterId> organizationOrder;
    public List<IdentityInfo> ownedIdentity;
    public List<EgoInfo> ownedEgo;

    [Header("---스테이지---")]
    public List<CantoData> cantoData;

    // [Header("---인벤토리---")]
    // 아직 미구현
}


public class SaveDataManager : MonoBehaviour
{
    // 역할 : 세이브 & 로드 매니저
    // 저장 방식 : Json 기반 데이터 저장 / 데이터 경로는 유니티 기본 저장 경로 사용

    [Header("---Setting---")]
    [SerializeField] private string directoryPath; // 폴더 위치
    [SerializeField] private string filePath; // 저장 데이터 위치
    [SerializeField] private string fileName = "savefile"; // 저장 데이터 이름


    private void Awake()
    {
        directoryPath = Path.Combine(Application.persistentDataPath, "Save");
        filePath = Path.Combine(directoryPath, fileName);
    }

    private void Start()
    {
        SetUp();
    }


    public void SetUp()
    {
        // 세이브 데이터 존재 여부 체크
        if (CheckData())
        {
            // 데이터 로드
            SaveData data = LoadData();
            if (data == null)
            {
                Debug.LogWarning("데이터 로드 실패 / 신규 데이터 생성");
                NewData();
            }
            else
            {
                Debug.Log($"데이터 로드 / {data}");

                // 인격 & 에고 데이터 전달
                OrganizationDatabase.instance.ApplyIdentityData(data);
                OrganizationDatabase.instance.ApplyEgoData(data);

                // 스테이지
                BattleContentManager.instance.ApplyCantoData(data);

                // 인벤토리

            }
        }
        else
        {
            // 신규 데이터 생성
            Debug.Log("신규 데이터 생성");
            NewData();
        }
    }


    #region 데이터 저장 & 로드
    /// <summary>
    /// 데이터 존재 여부 체크
    /// </summary>
    public bool CheckData()
    {
        return File.Exists(filePath);
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void SaveData()
    {
        // 각 매니저에서 데이터 받아오기

        // 데이터 저장
        SaveData saveData = new SaveData()
        {
            // 버전
            version = Application.version,
            playTutorial = true,

            // 인격 & 에고 소유
            ownedIdentity = OrganizationDatabase.instance.GetIdentityData(),
            ownedEgo = OrganizationDatabase.instance.GetEgoInfo(),

            // 수감자 편성
            organizationDatas = OrganizationDatabase.instance.GetOrganiztionData(),
            organizationOrder = OrganizationDatabase.instance.GetOrganizationOrderData(),

            // 스테이지
            cantoData = BattleContentManager.instance.GetCantoData(),

            // 인벤토리
            // inventoryData = InventoryManager.instance.GetInventoryData()
        };

        try
        {
            Directory.CreateDirectory(directoryPath);
            string save = JsonUtility.ToJson(saveData, true);
            File.WriteAllText(filePath, save);

            Debug.Log("데이터 저장 성공");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"[SaveDataManager] 데이터 저장 실패\n{e}");
        }
    }

    /// <summary>
    /// 데이터 로드
    /// </summary>
    public SaveData LoadData()
    {
        if (!File.Exists(filePath))
        {
            // 저장된 데이터가 없을 경우 - 생성
            Debug.LogError("저장된 데이터가 없습니다.");
            return null;
        }

        // 저장된 데이터가 있을 경우 - 로드
        try
        {
            // 데이터 로드 시도
            string json = File.ReadAllText(filePath);

            // 데이터가 비어있거나 손상된 경우 체크
            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogError("세이브 파일이 비어있거나 손상됨");
                return null;
            }

            // 데이터 역직렬화
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // 파일 손상 체크
            if (saveData == null)
            {
                Debug.LogError("역직렬화 실패 - 파일 손상 가능성");
                return null;
            }

            // 데이터 반환
            return saveData;
        }
        catch (System.Exception e)
        {
            // 로드 중 오류 발생 시
            Debug.LogError("데이터 로드 중 오류 발생: " + e.Message);
            return null;
        }
    }

    /// <summary>
    /// 신규 데이터 생성
    /// </summary>
    /// <returns></returns>
    public void NewData()
    {
        // 데이터 생성
        SaveData data = new SaveData
        {
            // 기본 데이터
            version = Application.version,
            playTutorial = false,

            // 편성 데이터  (편성 순서 & 값 없는 게 정상)
            organizationDatas = OrganizationDatabase.instance.CreatOrganizationData(),
            organizationOrder = new List<CharacterId>(0),

            // 인격 & 에고 보유 데이터
            ownedIdentity = OrganizationDatabase.instance.CreateIdentityData(),
            ownedEgo = OrganizationDatabase.instance.CreateEgoData(),

            // 스테이지 데이터 제작
            cantoData = BattleContentManager.instance.CreateCantoData(),

            // 인벤토리 데이터 제작

        };

        // Json 파일로 저장
        try
        {
            // 저장 위치 생성 - 있다면 무시
            Directory.CreateDirectory(directoryPath);

            // 저장
            string save = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, save);
        }
        catch (System.Exception e)
        {
            // 저장 실패 시 호출
            Debug.LogError($"[SaveDataManager] 신규 데이터 저장 실패\n{e}");
        }
    }
    #endregion
}
