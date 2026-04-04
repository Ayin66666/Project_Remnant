using Item;
using UnityEngine;

[CreateAssetMenu(fileName = "Useable", menuName = "Item/Useable/UseableSO", order = int.MaxValue)]
public class UseableSO : ItemSO
{
    [Header("---Useable Setting---")]
    [SerializeField] private UseableEffect useEffect;

    public void Use()
    {

    }
}
