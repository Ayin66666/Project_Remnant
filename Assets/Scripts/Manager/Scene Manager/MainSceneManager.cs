using System.Collections.Generic;
using UnityEngine;


public class MainSceneManager : MonoBehaviour
{
    [Header("---UI---")]
    [SerializeField] private CurUI curUI;
    [SerializeField] private List<GameObject> mainUI;
    private enum CurUI
    {
        Main,
        Organization,
        Stage,
        Vending
    }

    /// <summary>
    /// 버튼 이벤트
    /// </summary>
    /// <param name="uiNum"></param>
    public void ClickUI(int uiNum)
    {
        // 편성창에서 나가는 경우 데이터 저장
        if(curUI == CurUI.Organization)
            GameManager.instance.saveDataManager.SaveData();

        // UI 변경
        curUI = (CurUI)uiNum;
        foreach (GameObject ui in mainUI)
        {
            ui.SetActive(false);
        }

        mainUI[uiNum].SetActive(true);
    }

    /// <summary>
    /// 인벤토리 버튼 이벤트
    /// </summary>
    public void ClickInventory()
    {
        // 인벤토리는 데이터 저장을 위해 게임 매니저 하위에 들어가있는 DontDestroyOnLoad 오브젝트이므로,
        // MainSceneManager는 instance가 없어서 GameManager.instance.inventory로 접근해야 함

        GameManager.instance.inventory.InventoryUI(true);
    }
}


