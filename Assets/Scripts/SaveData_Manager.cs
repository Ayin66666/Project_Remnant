using UnityEngine;
using System.IO;
using System.Collections.Generic;


public class SaveData
{
    // 저장해야하는 데이터
    // 1. 튜토리얼 여부
    // 2. 스테이지 클리어 여부
    // 3. 캐릭터 스테이터스
    // 4. 편성 데이터
    // 5. 인벤토리

    public int version;
    public bool playTutorial;
    public List<ChapterData> chapterData;
}

#region Chapter
public class ChapterData
{
    public List<StageData> stageData;
}

public class StageData
{
    public bool isClear;
    public bool isExClear;
}
#endregion


public class SaveData_Manager : MonoBehaviour
{
    // 역할 : 세이브 & 로드 매니저
    // 저장 방식 : Json 기반 데이터 저장 / 데이터 경로는 유니티 기본 저장 경로 사용



    [Header("---Setting---")]
    [SerializeField] private string path;
    [SerializeField] private string fileName;

    private void Awake()
    {
        path = Path.Combine(Application.dataPath, fileName);
    }

    /// <summary>
    /// 데이터 존재 여부 체크
    /// </summary>
    public void CheckData()
    {

    }

    /// <summary>
    /// 데이터 저장
    /// </summary>
    public void SaveData()
    {
        // 각 매니저에서 데이터 받아오기
        // 1. 인벤토리 (레벨업 재화 & 동기화 재화) -> Inventory_Manager
        // 2. 캐릭터 편성 데이터 (인격 & 에고) -> Organization_Manager
        // 3. 캐릭터 보유 여부 & 스테이터스(레벨 & 동기화) -> CharacterData_Manager
        // 4. 스테이지 데이터 -> ChapterData_Manager
        // 5. 튜토리얼 클리어 여부 -> ChapterData_Manager(튜토리얼 종료 후 체크)
    }

    /// <summary>
    /// 데이터 로드
    /// </summary>
    public void LoadData()
    {

    }
}
