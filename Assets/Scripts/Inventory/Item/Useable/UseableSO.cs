using Item;
using System.Collections.Generic;
using UnityEngine;
using static UseableSO;


[CreateAssetMenu(fileName = "Useable", menuName = "Item/Useable/UseableSO", order = int.MaxValue)]
public class UseableSO : ItemSO
{
    [Header("---Useable Setting---")]
    [SerializeField] private AddType addType;
    [SerializeField] private List<Effect> effectList;
    private enum AddType { Count, GiveAll }


    [System.Serializable]
    public struct Effect
    {
        public AddType addType;
        public int count;
        public Vector2Int random;
        public ItemSO item;
        public enum AddType { count, random }
    }


    public override void Use()
    {
        switch (addType)
        {
            case AddType.Count:
                int ran = Random.Range(0, effectList.Count);
                int add = effectList[ran].addType == Effect.AddType.count ?
                    effectList[ran].count : Random.Range(effectList[ran].random.x, effectList[ran].random.y);
                
                // 아이템 추가
                GameManager.instance.inventory.AddItem(effectList[ran].item.ItemID, add);

                // 결과창 UI에 데이터 추가
                GameManager.instance.inventory.AddResultIcon(effectList[ran].item, add);
                break;

            case AddType.GiveAll:
                foreach (Effect effect in effectList)
                {
                    // 타입 별 획득 개수 설정
                    add = effect.addType == Effect.AddType.count ?
                        effect.count : Random.Range(effect.random.x, effect.random.y);

                    // 아이템 추가
                    GameManager.instance.inventory.AddItem(effect.item.ItemID, add);

                    // 결과창 UI에 데이터 추가
                    GameManager.instance.inventory.AddResultIcon(effect.item, add);
                }
                break;
        }
    }
}
