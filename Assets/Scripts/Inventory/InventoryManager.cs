using Item;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    [Header("---Setting---")]
    [SerializeField] private Dictionary<int, InventorySlot> slotDic;
    [SerializeField] private Dictionary<int, ItemStack> itemDic;
    [SerializeField] private int selectedItemId;
    private Dictionary<int, int> addDic;

    [Header("---Prefab---")]
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private GameObject resultIconPrefab;

    [Header("---UI---")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private RectTransform slotRect;
    [SerializeField] private GameObject descriptionLUI;
    [SerializeField] private RectTransform summaryUI;

    [Header("---Summary UI---")]
    [SerializeField] private TextMeshProUGUI summaryNameText;
    [SerializeField] private TextMeshProUGUI summaryText;

    [Header("---Description UI---")]
    [SerializeField] private Image desIcon;
    [SerializeField] private TextMeshProUGUI desNameText;
    [SerializeField] private TextMeshProUGUI countText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TMP_InputField inputfield;
    [SerializeField] private GameObject useButton;
    [SerializeField] private GameObject inputfieldObj;

    [Header("---Result UI---")]
    [SerializeField] private GameObject resultUI;
    [SerializeField] private RectTransform resultRect;
    [SerializeField] private List<GameObject> resultIconList;


    /// <summary>
    /// 아이템 추가 테스트용 코드 - 추후 제거 예정
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(200002, 1);
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
    /// 기본 런타임 데이터 생성
    /// </summary>
    public void SetUp()
    {
        itemDic = new Dictionary<int, ItemStack>();
        slotDic = new Dictionary<int, InventorySlot>();
        addDic = new Dictionary<int, int>();
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
            itemDic[id].count -= count;

            // 아이템 사용 효과 호출
            for(int i = 0; i < count; i++)
            {
                itemDic[id].item.Use();
            }

            // 결과 UI 표시
            ResultUI(true);

            // 전부 소모했다면 데이터 제거
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
    /// 아이템 사용 결과 표시를 위한 데이터 취합 함수
    /// </summary>
    /// <param name="id"></param>
    /// <param name="count"></param>
    public void AddResultData(ItemSO item, int count)
    {
        // 이미 동일한 아이템을 추가한 적 있는지 체크
        if (addDic.ContainsKey(item.ItemID))
        {
            // 있다면 -> 기존 데이터에 개수 추가
            addDic[item.ItemID] += count;
        }
        else
        {
            // 없다면 -> 새로 데이터 추가
            addDic.Add(item.ItemID, count);
        }

        // 아이템 추가
        AddItem(item.ItemID, count);
    }
    #endregion


    #region UI
    /// <summary>
    /// 인벤토리 UI On/Off
    /// </summary>
    /// <param name="isOn"></param>
    public void InventoryUI(bool isOn)
    {
        inventoryUI.SetActive(isOn);
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
        inputfieldObj.SetActive(so.ItemType == ItemType.Useable ? true : false);
    }

    /// <summary>
    /// 아이템 상세 설명 UI On/Off
    /// </summary>
    public void DescriptionUI(bool isOn)
    {
        inputfield.text = string.Empty;
        descriptionLUI.SetActive(isOn);
    }

    /// <summary>
    /// 아이템 요약 설명 UI
    /// </summary>
    /// <param name="so"></param>
    /// <param name="isOn"></param>
    public void SummaryUI(ItemSO so, bool isOn)
    {
        if(isOn)
        {
            summaryNameText.text = $"{so.ItemName} <size=20>(보유 수 : {itemDic[so.ItemID].count})</size>";
            summaryText.text = so.ItemDescription; 
            Vector2 pos = Input.mousePosition + new Vector3(325, 0, 0);
            summaryUI.position = pos;
        }
        else
        {
            summaryNameText.text = string.Empty;
            summaryText.text = string.Empty;
        }

        summaryUI.gameObject.SetActive(isOn);
    }

    /// <summary>
    /// 아이템 사용 후 결과 아이콘을 결과창에 추가
    /// </summary>
    /// <param name="so"></param>
    /// <param name="count"></param>
    public void AddResultIcon(ItemSO so, int count)
    {
        Debug.Log($"획득 데이터 전달 : {so} / {count}");

        // 획득한 아이템을 아이콘에 데이터 전달
        GameObject obj = Instantiate(resultIconPrefab, resultRect);
        ResultIcon icon = obj.GetComponent<ResultIcon>();
        icon.SetUp(so, count);
        resultIconList.Add(obj);
    }

    /// <summary>
    /// 아이템 획득 결과창 UI On/Off
    /// </summary>
    public void ResultUI(bool isOn)
    {
        if (!isOn)
        {
            // 리스트 및 데이터 정리
            resultIconList.ForEach(Destroy);
            resultIconList.Clear();
            addDic.Clear();
        }
        else
        {
            // 결과 UI 아이콘 추가
            foreach(var data in addDic)
            {
                ItemSO so = DataLoader.instance.ItemDic[data.Key];
                AddResultIcon(so, data.Value);
            }
        }

        // UI On/Off
        resultUI.SetActive(isOn);
    }
    #endregion


    #region Button
    /// <summary>
    /// 아이템 사용 - id 기반 아이템 검색
    /// </summary>
    public void ClickUseButton()
    {
        // 인풋 필드의 데이터 받아오기
        if (int.TryParse(inputfield.text, out int value))
        {
            // 아이템 사용
            Debug.Log($"입력된 값 : {value}");
            UseItem(selectedItemId, value);

            // UI 종료
            DescriptionUI(false);
        }
        else
        {
            // 모종의 이유로 인풋필드 값 전환 실패 시
            Debug.LogError("모종의 오류로 인풋 필드 값 전환 실패!");
        }
    }

    // 이 아래 코드는 인풋 필드 코드
    /// <summary>
    /// 가지고 있는 재화를 모두 사용 버튼
    /// </summary>
    public void ClickMaxButton()
    {
        // 잘못된 id인지 체크
        if (!itemDic.ContainsKey(selectedItemId))
            return;

        int inputValue = itemDic[selectedItemId].count;
        inputfield.text = inputValue.ToString();
    }

    /// <summary>
    /// 입력값 0으로 초기화 버튼
    /// </summary>
    public void ClickMinButton()
    {
        Debug.Log("제거호출");
        // 잘못된 id인지 체크
        if (!itemDic.ContainsKey(selectedItemId))
            return;

        int inputValue = 0;
        Debug.Log($"제거호출2 {inputValue}");
        inputfield.text = inputValue.ToString();
    }

    /// <summary>
    /// 증가 버튼 클릭
    /// </summary>
    public void ClickPlus()
    {
        // 잘못된 id인지 체크
        if (!itemDic.ContainsKey(selectedItemId))
            return;

        // 입력값 전환 성공 여부 체크
        if (!int.TryParse(inputfield.text, out int value))
            value = 0;

        value = Mathf.Clamp(value + 1, 0, itemDic[selectedItemId].count);
        inputfield.SetTextWithoutNotify(value.ToString());
    }

    /// <summary>
    /// 감소 버튼 클릭
    /// </summary>
    public void ClickMinus()
    {
        // 잘못된 id인지 체크
        if (!itemDic.ContainsKey(selectedItemId))
            return;

        // 입력값 전환 성공 여부 체크
        if (!int.TryParse(inputfield.text, out int value))
            value = 0;

        value = Mathf.Clamp(value - 1, 0, itemDic[selectedItemId].count);
        inputfield.SetTextWithoutNotify(value.ToString());
    }

    /// <summary>
    /// 인풋 필드의 입력값을 체크하고 가진 아이템 이상을 입력했을 경우 최대치로 자동전환
    /// </summary>
    public void InputField()
    {
        // 잘못된 id인지 체크
        if (!itemDic.ContainsKey(selectedItemId))
            return;

        // 입력값 전환 성공 여부 체크
        if (!int.TryParse(inputfield.text, out int inputValue))
            return;

        // 값 전환
        inputValue = Mathf.Clamp(inputValue, 0, itemDic[selectedItemId].count);
        inputfield.SetTextWithoutNotify(inputValue.ToString());
    }
    #endregion
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
