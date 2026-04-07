using Item;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("---Setting---")]
    [SerializeField] private Dictionary<int, InventorySlot> slotDic;
    [SerializeField] private Dictionary<int, ItemStack> itemDic;
    [SerializeField] private int selectedItemId;

    [Header("---Prefab---")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject resultIconPrefab;

    [Header("---UI---")]
    [SerializeField] private RectTransform slotRect;
    [SerializeField] private GameObject descriptionUI;

    [Header("---Description UI---")]
    [SerializeField] private Image desIcon;
    [SerializeField] private TextMeshProUGUI desNameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject inputfield;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(90500, 1);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            UseItem(90500, 1);
        }
    }

    #region 시작 로직
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        SetUp();
    }

    /// <summary>
    /// 기본 런타임 데이터 생성 - 경험치 티켓(500) 1개, 끈 50개 제공
    /// </summary>
    public void SetUp()
    {
        itemDic = new Dictionary<int, ItemStack>();
        slotDic = new Dictionary<int, InventorySlot>();

        /* 기본 아이템 지급 로직 -> 필요한가?
        // 경험치 티켓(500) 1개
        ItemStack stack = new ItemStack(DataLoader.instance.ItemDic[90500], 1);
        itemDic.Add(stack.item.ItemID, stack);

        // 끈 50개
        stack = new ItemStack(DataLoader.instance.ItemDic[90000], 50);
        itemDic.Add(stack.item.ItemID, stack);
        */
    }

    /// <summary>
    /// 세이브 데이터 받아오기
    /// </summary>
    public void ApplyInventoryData(SaveData saveData)
    {
        // 세이브 데이터를 읽은 후 세팅
        foreach (var itemSave in saveData.inventoryData)
        {
            // 데이터 생성
            ItemSO so = DataLoader.instance.ItemDic[itemSave.itemId];
            ItemStack stack = new ItemStack(so, itemSave.count);

            // 런타임 데이터 딕셔너리에 데이터 저장
            itemDic.Add(so.ItemID, stack);

            // 슬롯 생성
            GameObject slot = Instantiate(this.slotPrefab, slotRect.transform);
            InventorySlot invenSlot = slot.GetComponent<InventorySlot>();

            // 슬롯 데이터 세팅
            invenSlot.SetUp(so, stack.count);
            slotDic.Add(so.ItemID, invenSlot);
        }
    }
    #endregion


    #region 데이터 로직
    /// <summary>
    /// 아이템 추가
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void AddItem(int id, int count)
    {
        if (itemDic.ContainsKey(id))
        {
            // 기존 데이터 & 슬롯에 데이터 업데이트
            itemDic[id].count = itemDic[id].count + count;
            slotDic[id].SetUp(itemDic[id].item, itemDic[id].count);
        }
        else
        {
            // 아이템 추가
            ItemStack stack = new ItemStack(DataLoader.instance.ItemDic[id], count);
            itemDic.Add(id, stack);

            // 슬롯 추가 & 데이터 할당
            GameObject slot = Instantiate(slotPrefab, slotRect);
            InventorySlot invenSlot = slot.GetComponent<InventorySlot>();
            invenSlot.SetUp(stack.item, stack.count);
            slotDic.Add(id, invenSlot);
        }
    }

    /// <summary>
    /// 아이템 사용
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void UseItem(int id, int count)
    {
        if (itemDic.ContainsKey(id))
        {
            // 개수 차감
            itemDic[id].count = itemDic[id].count - count;
            if (itemDic[id].count <= 0)
            {
                GameObject obj = slotDic[id].gameObject;
                slotDic.Remove(id);
                itemDic.Remove(id);
                Destroy(obj);
            }
        }
    }

    /// <summary>
    ///  아이템 보유량 반환
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetItemCount(int id)
    {
        if (itemDic.ContainsKey(id))
        {
            return itemDic[id].count;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 설명 UI 데이터 세팅
    /// </summary>
    /// <param name="so"></param>
    public void DescriptionUIDataSetting(ItemSO so)
    {
        // 데이터 세팅
        selectedItemId = so.ItemID;
        desNameText.text = so.ItemName;
        desIcon.sprite = so.ItemIcon;
        countText.text = $"소지 수 : <size=50>{itemDic[so.ItemID].count}</size>";
        descriptionText.text = so.ItemDescription;

        // 사용 가능한 아이템이라면 -> 사용 버튼 & 인풋필드 활성화
        useButton.SetActive(so.ItemType == ItemType.Useable ? true : false);
        inputfield.SetActive(so.ItemType == ItemType.Useable ? true : false);
    }

    /// <summary>
    /// 아이템 설명 UI On/Off
    /// </summary>
    public void DescriptionUI(bool isOn)
    {
        descriptionUI.SetActive(isOn);
    }

    /// <summary>
    /// 아이템 사용 후 결과 표시
    /// </summary>
    /// <param name="so"></param>
    /// <param name="count"></param>
    public void ResultUI(ItemSO so, int count)
    {
        
    }
    #endregion

    /// <summary>
    /// 아이템 사용 - id 기반 아이템 검색
    /// </summary>
    public void ClickUseButton()
    {
        // 인풋 필드의 데이터 받아오기
        int value = int.Parse(inputField.text);

        // 아이템 사용
        UseItem(selectedItemId, value);

        // UI 종료
        DescriptionUI(false);

        // 결과 UI 표시
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
