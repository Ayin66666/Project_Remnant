using Game.Canto;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CantoDatabase", menuName ="Canto/Database", order = int.MaxValue)]
public class CantoDatabaseSO : ScriptableObject
{
    [Header("---Setting---")]
    [SerializeField] private List<CantoMasterSO> cantoData;
    public IReadOnlyList<CantoMasterSO> CantoData => cantoData;
}
