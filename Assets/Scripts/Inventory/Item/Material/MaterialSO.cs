using Item;
using UnityEngine;


[CreateAssetMenu(fileName = "MaterialSO", menuName = "Item/Material/MaterialSO", order = int.MaxValue)]
public class MaterialSO : ItemSO
{
    [Header("---Material Setting---")]
    [SerializeField, TextArea] private string descriptionText; // 소모처 메모용
}
