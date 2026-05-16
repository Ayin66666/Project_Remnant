using System.Collections.Generic;
using UnityEngine;


public class MainSceneManager : MonoBehaviour
{
    // 메인 씬은 싱글톤 유지
    public static MainSceneManager instance;

    [Header("---UI---")]
    [SerializeField] private CurUI curUI;
    [SerializeField] private GameObject bottomUI;
    [SerializeField] private List<GameObject> mainUI;
    private enum CurUI
    {
        Main, // 메인 화면
        Organization, // 편성창
        Stage, // 스테이지 선택창
        Vending // 상점
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 하단 UI 제어 (레벨 & 인벤토리 등등)
    /// </summary>
    /// <param name="isOn"></param>
    public void BottomUISetting(bool isOn)
    {
        bottomUI.SetActive(isOn);
    }

    /// <summary>
    /// 버튼 이벤트
    /// </summary>
    /// <param name="uiNum"></param>
    public void ClickUI(int uiNum)
    {
        // 편성창에서 나가는 경우 데이터 저장
        if (curUI == CurUI.Organization)
            GameManager.instance.saveDataManager.SaveData();

        // UI 변경
        curUI = (CurUI)uiNum;
        foreach (GameObject ui in mainUI)
        {
            ui.SetActive(false);
        }

        // 하단 UI가 꺼져있을 경우 켜기
        if (bottomUI.activeSelf == false)
            bottomUI.SetActive(true);

        // 선택한 UI 켜기
        mainUI[uiNum].SetActive(true);
    }

    /// <summary>
    /// 편성창 버튼 이벤트
    /// -> 원래는 ClickUI() 함수에서 제어했으나, bool 조건이 추가되어 분리함
    /// </summary>
    public void ClickOrganization()
    {
        OrganizationManager.instance.OrganizationUI(true, false);
    }

    /// <summary>
    /// 인벤토리 버튼 이벤트
    /// </summary>
    public void ClickInventory()
    {
        // 인벤토리는 데이터 저장을 위해 게임 매니저 하위에 들어가있는 DontDestroyOnLoad 오브젝트이므로,
        // MainSceneManager는 instance가 없어서 GameManager.instance.inventory로 접근해야 함
        // 또한 메인 씬에서 켜져있는 UI 위에 오버레이 되는 UI로, 인벤토리는 curUI를 조절하지 않음

        GameManager.instance.inventory.InventoryUI(true);
    }
}


