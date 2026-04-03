using UnityEngine;


namespace Item
{
    public abstract class ItemSO : ScriptableObject
    {
        [Header("---Setting---")]
        /// <summary>
        /// 아이템의 타입
        /// </summary>
        [SerializeField] private ItemType itemType;
        /// <summary>
        /// 아이템의 ID
        /// </summary>
        [SerializeField] private int itemID;
        /// <summary>
        /// 아이템의 중첩가능여부
        /// </summary>
        private int maxStack = 999;

        [Header("---UI---")]
        /// <summary>
        /// 아이템의 아이콘
        /// </summary>
        [SerializeField] private Sprite itemIcon;
        /// <summary>
        /// 아이템의 이름
        /// </summary>
        [SerializeField] private string itemName;
        /// <summary>
        /// 아이템의 설명
        /// </summary>
        [SerializeField, TextArea] private string itemDescription;


        public ItemType ItemType => itemType;
        public int ItemID => itemID;
        public int MaxStack => maxStack;
        public string ItemName => itemName;
        public string ItemDescription => itemDescription;
        public Sprite ItemIcon => itemIcon;
    }


    /// <summary>
    /// 아이템 타입
    /// </summary>
    public enum ItemType 
    { 
        Equip, 
        Useable,
        Material, 
        Other 
    }

    /// <summary>
    /// 경험치 티켓 타입
    /// </summary>
    public enum ExpTicketType
    {
        ExpTicket_100,
        ExpTicket_500,
        ExpTicket_1000,
        ExpTicket_5000,
    }
}

