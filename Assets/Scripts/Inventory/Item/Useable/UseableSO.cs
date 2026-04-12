using Item;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Useable", menuName = "Item/Useable/UseableSO", order = int.MaxValue)]
public class UseableSO : ItemSO
{
    [Header("---Useable Setting---")]
    [SerializeField] private AddType addType;
    [SerializeField] private List<Effect> effectList;
    private enum AddType { JustOne, GiveAll }


    [System.Serializable]
    public struct Effect
    {
        public AddType addType;
        public ItemSO item;
        public int value;
        public Vector2Int random;
        public enum AddType { Value, Random }
    }


    public override void Use()
    {
        switch (addType)
        {
            case AddType.JustOne:
                int ran = Random.Range(0, effectList.Count);
                int add = effectList[ran].addType == Effect.AddType.Value ?
                    effectList[ran].value : Random.Range(effectList[ran].random.x, effectList[ran].random.y);
                
                // 아이템 추가
                GameManager.instance.inventory.AddResultData(effectList[ran].item, add);
                break;

            case AddType.GiveAll:
                foreach (Effect effect in effectList)
                {
                    // 타입 별 획득 개수 설정
                    add = effect.addType == Effect.AddType.Value ?
                        effect.value : Random.Range(effect.random.x, effect.random.y);

                    // 아이템 추가
                    GameManager.instance.inventory.AddResultData(effect.item, add);
                }
                break;
        }
    }
}
