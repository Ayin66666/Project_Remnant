using Game.Character;
using Item;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Equip", menuName = "Item/Equip", order = int.MaxValue)]
public class EquipmentSO : ItemSO
{
    [Header("---Equip Setting---")]
    [SerializeField] private CharacterId sinner;
    [SerializeField] private List<ItemEffect> effectList;


    public CharacterId Sinner => sinner;
    public List<ItemEffect> EffectList => effectList;
}
