using UnityEngine;

[System.Serializable]
public class IdentityData
{
    [Header("---Status---")]
    public bool isUnlocked;
    public int sync;
    public int level;
    public int curExp;

    [Header("---Reference---")]
    public IdentityMasterSO master;
}
