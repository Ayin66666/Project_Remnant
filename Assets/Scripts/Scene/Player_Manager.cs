using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ChapterData
{
    public List<StageData> stageList;
}

[System.Serializable]
public class StageData
{
    public bool isClear;
    public string stageSceneName;
}


public class Player_Manager : MonoBehaviour
{
    /*
     * 1. 메인 - 시작화면의 인격들 표시
     * 2. 하단 버튼 클릭 시 창 넘어가기
     * 3. 데이터 관련 이모저모?
     */
    public static Player_Manager instacne;


    [Header("---Main UI---")]
    [SerializeField] private GameObject[] uiSet;
    [SerializeField] private GameObject exitSet;
    [SerializeField] private GameObject entrycheckSet;


    [Header("---Chapter & Stage Data---")]
    [SerializeField] private List<ChapterData> chapterData;
    [SerializeField] private int curChapterIndex;
    [SerializeField] private int curSTageIndex;


    private void Awake()
    {
        if (instacne == null)
        {
            instacne = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }



    /// <summary>
    /// 메인 화면 하단 버튼 클릭
    /// 0 : 유리창 
    /// 1 : 편성
    /// 2 : 운전석
    /// </summary>
    /// <param name="index"></param>
    public void Click_Button(int index)
    {
        foreach (GameObject item in uiSet)
        {
            item.SetActive(false);
        }

        uiSet[index].SetActive(true);
    }


    #region 게임 종료
    /// <summary>
    /// 게임 종료 클릭
    /// </summary>
    public void Click_Exit()
    {
        exitSet.SetActive(true);
    }

    /// <summary>
    /// 게임 종료
    /// </summary>
    public void Exit(bool isExit)
    {
        if (isExit)
        {
            // 게임 종료
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
        }
        else
        {
            // 종료 UI Off
            exitSet.SetActive(isExit);
        }
    }
    #endregion


    #region 유리창
    /// <summary>
    /// 유리창의 캐릭터 버튼 클릭 (초상화 변경)
    /// </summary>
    /// <param name="index"></param>
    public void Click_CharacterChange(int index)
    {
        // 이거 어차피 인격 1개인데 필요한가?
    }

    /// <summary>
    /// 캐릭터 초상화 클릭 (대사 출력)
    /// </summary>
    /// <param name="index"></param>
    public void Click_CharacterPortrait(int index)
    {
        // 대사 데이터는 Player_Base에서? - 데이터 연결은?
    }
    #endregion


    #region 운전석
    /// <summary>
    /// 스테이지 선택 버튼 클릭
    /// </summary>
    /// <param name="index"></param>
    public void Click_Chapter(int ChapterIndex)
    {
        // 1. 챕터 클릭 시 해당 챕터 UI 표시

        // 2. 챕터 내 스테이지 클릭 시 해당 스테이지 입장 여부 체크

        // 3. 입장 클릭 시 스테이지 입장
    }

    /// <summary>
    /// 챕터 내 스테이지 선택 버튼 클릭
    /// </summary>
    /// <param name="charpterIndex"></param>
    /// <param name="stageIndex"></param>
    public void Click_Stage(int charpterIndex, int stageIndex)
    {
        // 진입 데이터 설정
        curChapterIndex = charpterIndex;
        curSTageIndex = stageIndex;

        // UI 표시
        entrycheckSet.SetActive(true);
    }

    /// <summary>
    /// 스테이지 진입 버튼 클릭
    /// </summary>
    /// <param name="isIn"></param>
    public void Click_Stage(bool isIn)
    {
        if(isIn)
        {
            // 데이터 체크
            if(curChapterIndex == -1)
            {
                Debug.Log("동작 불가 - 데이터 없음!");
                return;
            }

            // 씬 로드 - 매니저 추가 필요
            // Load_Manager.LoadScene(chapterData[curChapterIndex].stageList[curSTageIndex]);
        }
        else
        {
            // 데이터 초기화
            curChapterIndex = -1;
            curSTageIndex = -1;

            // 선택 해제
            entrycheckSet.SetActive(false);
        }
    }
    #endregion
}
