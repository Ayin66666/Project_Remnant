using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IdentityData
{
    [Header("---Status---")]
    public bool isUnlocked;
    public int sync;
    public int level;
    public int curExp;
    public List<int> maxExp;

    [Header("---Reference---")]
    public IdentityMasterSO master;
}
