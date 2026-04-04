using System.Collections.Generic;
using UnityEngine;
using Item;
using Game.Character;


[CreateAssetMenu(fileName = "ItemSOContainer", menuName = "Item/ItemSOContainer", order = int.MaxValue)]
public class ItemSOContainer : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private List<ItemSO> equipList;
    [SerializeField] private List<ItemSO> useableList;
    [SerializeField] private List<ItemSO> materialList;
    [SerializeField] private List<ItemSO> fragmentList;
    [SerializeField] private List<ItemSO> expTicketList;


    public List<ItemSO> EquipList => equipList;
    public List<ItemSO> UseableList => useableList;
    public List<ItemSO> MaterialList => materialList;
    public List<ItemSO>[] AllItems => new List<ItemSO>[]
    {
        equipList,
        equipList,
        materialList,
        expTicketList,
        fragmentList
    };
}
