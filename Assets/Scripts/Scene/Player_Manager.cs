using System.Collections.Generic;
using UnityEngine;


public class Player_Manager : MonoBehaviour
{
    /*
     * 1. 메인 - 시작화면의 인격들 표시
     * 2. 하단 버튼 클릭 시 창 넘어가기
     * 3. 데이터 관련 이모저모?
     */
    public static Player_Manager instacne;

    [Header("---Chapter & Stage Data---")]
    [SerializeField] private List<ChapterData_SO> chapterData;

    [Header("---Selected Stage---")]
    [SerializeField] private StageData stageData;


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
    public void Click_Chapter(int chapterIndex)
    {
        // 1. 챕터 클릭 시 해당 챕터 UI 표시
        UI_Manager.instance.ChapterUI(chapterData[chapterIndex], chapterIndex);
    }

    /// <summary>
    /// 챕터 내 스테이지 선택 버튼 클릭
    /// </summary>
    /// <param name="charpterIndex"></param>
    /// <param name="stageIndex"></param>
    public void Click_Stage(int charpterIndex, int stageIndex)
    {
        // 진입 데이터 설정
        stageData = chapterData[charpterIndex].stageList[stageIndex];

        // UI 표시
        UI_Manager.instance.EntrycheckUI(true);
    }

    /// <summary>
    /// 스테이지 진입 버튼 클릭
    /// </summary>
    /// <param name="isIn"></param>
    public void Click_StageIn(bool isIn)
    {
        if(isIn)
        {
            // 데이터 체크
            if(stageData != null)
            {
                Debug.Log("동작 불가 - 데이터 없음!");
                return;
            }

            // 씬 로드 - 매니저 추가
            Load_Manager.LoadScene(stageData.stageSceneName);
        }
        else
        {
            // 데이터 초기화
            stageData = null;

            // 선택 해제
            UI_Manager.instance.EntrycheckUI(false);
        }
    }
    #endregion
}
