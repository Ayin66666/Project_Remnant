using Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;


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
    [SerializeField] private List<InventorySlot> slots;
    [SerializeField] private Dictionary<int, ItemStack> itemDic;

    [Header("---Prefab---")]
    [SerializeField] private GameObject slot;

    [Header("---UI---")]
    [SerializeField] private RectTransform slotRect;
    [SerializeField] private GameObject descriptionUI;

    [Header("---Description UI---")]
    [SerializeField] private Image desIcon;
    [SerializeField] private TextMeshProUGUI desNameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// 세이브 데이터 받아오기
    /// </summary>
    public void ApplyInventoryData(SaveData saveData)
    {
        // 세이브 데이터를 읽은 후 세팅
        foreach(var itemSave in saveData.inventoryData)
        {
            // 데이터 생성
            ItemSO so = DataLoader.instance.ItemDic[itemSave.itemId];
            ItemStack stack = new ItemStack(so, itemSave.count);

            // 런타임 데이터 딕셔너리에 데이터 저장
            itemDic.Add(so.ItemID, stack);

            // 슬롯 생성
            GameObject slot = Instantiate(this.slot, slotRect.transform);
            InventorySlot invenSlot = slot.GetComponent<InventorySlot>();

            // 슬롯 데이터 세팅
            invenSlot.SetUp(so, stack.count);
            slots.Add(invenSlot);
        }
    }

    /// <summary>
    /// 신규 데이터 생성
    /// </summary>
    public void CreateInventoryData()
    {

    }

    /// <summary>
    /// 경험치 티켓 보유량 반환
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetExpTicketCount(ExpTicketType type)
    {
        return 0;
    }

    /// <summary>
    /// 아이템 설명 UI On/Off
    /// </summary>
    public void DescriptionUI(ItemSO so, bool isOn)
    {
        if (isOn)
        {
            // 데이터 세팅
            desNameText.text = so.ItemName;
            desIcon.sprite = so.ItemIcon;
            countText.text = $"소지 수 : <size=50>{itemDic[so.ItemID].count}</size>";
            descriptionText.text = so.ItemDescription;

            // 사용 가능한 아이템이라면 -> 사용 버튼 활성화
            if (so.ItemType == ItemType.Useable)
                useButton.SetActive(true);
        }

        descriptionUI.SetActive(isOn);
    }
}


[System.Serializable]
/// <summary>
/// 인벤토리용 데이터 (딕셔너리)
/// </summary>
public class ItemStack
{
    [Header("---Data---")]
    public ItemSO item;
    public int count;


    public ItemStack(ItemSO so, int co)
    {
        item = so;
        count = co;
    }
}
