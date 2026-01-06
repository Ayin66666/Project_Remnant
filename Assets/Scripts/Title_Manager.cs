using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title_Manager : MonoBehaviour
{
    // 역할
    // 시작 씬 관리
    // 데이터 관리는 GameData_Manager로?

    [Header("---Setting---")]
    [SerializeField] private GameObject[] uiset;


    public void Click_Start()
    {
        // 게임 진입 로직

        // 데이터 체크
        // -> 데이터가 없다면 생성 후 튜토리얼로
        // -> 데이터가 있다면 로드 후 메인화면으로

        /*
        // 데이터 체크
        SaveData_Manager.instance.LoadData();
        if()
        {
            // 저장 데이터가 있다면 - 메인화면 이동
        }
        else
        {
            // 저장 데이터가 없다면 - 튜토리얼 이동
        }
        */
    }

    public void Click_Option()
    {
        // 이벤트 호출만 알려주고 기능은 Option_Manager에서?
    }

    public void Click_RemoveData()
    {
        // 기존 데이터 삭제
    }

    public void Click_Exit()
    {
        // 게임 종료
    }
}
