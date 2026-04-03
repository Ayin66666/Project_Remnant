using System.Collections.Generic;
using UnityEngine;
using Item;


[CreateAssetMenu(fileName = "ItemSOContainer", menuName = "Item/ItemSOContainer", order = int.MaxValue)]
public class ItemSOContainer : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private List<ItemSO> equipList;
    [SerializeField] private List<ItemSO> useableList;
    [SerializeField] private List<ItemSO> materialList;


    public List<ItemSO> EquipList => equipList;
    public List<ItemSO> UseableList => useableList;
    public List<ItemSO> MaterialList => materialList;
    public List<ItemSO>[] AllItems => new List<ItemSO>[]
    {
        equipList,
        equipList,
        materialList
    };
}
