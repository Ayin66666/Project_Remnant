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

public class ChapterData
{
    public List<StageData> stageData;
}

public class StageData
{
    public bool isClear;
    public bool isExClear;
}


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

    public void SaveData()
    {

    }

    public void LoadData()
    {

    }
}
