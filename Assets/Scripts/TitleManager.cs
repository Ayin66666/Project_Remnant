using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TitleManager : MonoBehaviour
{
    // 역할
    // 시작 씬 관리
    // 데이터 관리는 GameData_Manager로?

    [Header("---Setting---")]
    [SerializeField] private GameObject[] uiset;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIOff();
        }
    }

    public void UIOff()
    {
        foreach (GameObject obj in uiset)
        {
            if (obj.activeSelf) obj.SetActive(false);
        }
    }

    public void Click_Start()
    {
        // 게임 진입 로직

        // 데이터 체크
        // -> 데이터가 없다면 생성 후 튜토리얼로
        // -> 데이터가 있다면 로드 후 메인화면으로

        // 데이터 체크
        if(true/*SaveData_Manager.CheckData()*/)
        {
            // 저장 데이터가 있다면 - 메인화면 이동
            SceneLoadManager.LoadScene("Main_Scene", "임시 피난처");
        }
        else
        {
            // 저장 데이터가 없다면 - 튜토리얼 이동
            SceneLoadManager.LoadScene("Tutorial_Scene", "튜토리얼 \n- 엄지 산하 지부");
        }

    }

    public void Click_Option()
    {
        // 이벤트 호출만 알려주고 기능은 Option_Manager에서
    }


    public void Click_RemoveData()
    {
        uiset[0].SetActive(true);
    }

    public void RemoveData()
    {

    }


    public void Click_Exit()
    {
        uiset[1].SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
