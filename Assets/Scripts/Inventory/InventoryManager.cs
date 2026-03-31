using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Item;

public class InventoryManager : MonoBehaviour
{
    /*
    역할 : 인벤토리 관리
        - 인격 전용 장비
        - 경험치 티켓
        - 끈 & 파편
        - 기타 소장 아이템
    */

    public static InventoryManager instance;

    [Header("---Setting---")]
    [SerializeField] private Dictionary<int, ItemSO> itemDic = new Dictionary<int, ItemSO>();
    [SerializeField] private List<InventorySlot> slots;


    #region
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 세이브 데이터 받아오기
    /// </summary>
    public void ApplyInventoryData(SaveData data)
    {

    }
    #endregion


    #region Get Data
    /// <summary>
    /// 경험치 티켓 보유량 반환
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetExpTicketCount(ExpTicketType type)
    {
        return 0;
    }
    #endregion
}
