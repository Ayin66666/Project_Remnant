using System.Collections.Generic;
using System.IO;
using UnityEngine;


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
    public List<IdentityInfo> ownedIdentity;
    public List<EgoInfo> ownedEgo;

    // [Header("---인벤토리---")]
    // 아직 미구현

    // [Header("---스테이지---")]
    // public List<StageData> stage;
}


public class SaveDataManager : MonoBehaviour
{
    // 역할 : 세이브 & 로드 매니저
    // 저장 방식 : Json 기반 데이터 저장 / 데이터 경로는 유니티 기본 저장 경로 사용

    [Header("---Setting---")]
    [SerializeField] private string path;
    [SerializeField] private string fileName;

    private void Awake()
    {
        path = Path.Combine(Application.persistentDataPath, fileName);
    }

    public void Start()
    {
        if(CheckData())
        {
            // 데이터 로드 후 전달
            SaveData data = LoadData();

        }
        else
        {
            // 신규 데이터 생성
            NewData();
        }
    }


    #region 데이터 저장 & 로드
    /// <summary>
    /// 데이터 존재 여부 체크
    /// </summary>
    public bool CheckData()
    {
        return File.Exists(path);
    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void SaveData()
    {
        // 각 매니저에서 데이터 받아오기

        // 데이터 저장
        SaveData saveData = new SaveData();
    }

    /// <summary>
    /// 데이터 로드
    /// </summary>
    public SaveData LoadData()
    {
        if (!File.Exists(path))
        {
            // 저장된 데이터가 없을 경우 - 생성
            Debug.LogError("저장된 데이터가 없습니다.");
            return NewData();
        }

        // 저장된 데이터가 있을 경우 - 로드
        try
        {
            // 데이터 로드 시도
            string json = File.ReadAllText(path);

            // 데이터가 비어있거나 손상된 경우 체크
            if (string.IsNullOrWhiteSpace(json))
            {
                Debug.LogError("세이브 파일이 비어있거나 손상됨");
                return NewData();
            }
            
            // 데이터 역직렬화
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);

            // 파일 손상 체크
            if (saveData == null)
            {
                Debug.LogError("역직렬화 실패 - 파일 손상 가능성");
                return NewData();
            }

            // 데이터 반환
            return saveData;
        }
        catch (System.Exception e)
        {
            // 로드 중 오류 발생 시
            Debug.LogError("데이터 로드 중 오류 발생: " + e.Message);
            return NewData();
        }
    }

    /// <summary>
    /// 신규 데이터 생성
    /// </summary>
    /// <returns></returns>
    public SaveData NewData()
    {
        SaveData data = new SaveData
        {
            version = Application.version,
            playTutorial = false,
            organizationDatas = new List<OrganizationData>(),
            ownedIdentity = new List<IdentityInfo>(),
            ownedEgo = new List<EgoInfo>(),
        };

        // 초기 편성 데이터 생성
        OrganizationDatabase.instance.CreativeData();

        // 초기 보유 데이터 생성


        return data;
    }
    #endregion
}
