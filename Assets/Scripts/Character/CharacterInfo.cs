using UnityEngine;

[System.Serializable]
public class CharacterInfo
{
    [Header("---Status---")]
    public string characterId;
    public bool isUnlock;
    public int level;
    public int sync;
    public GameObject prefab;


    public async void Spawn()
    {

    }
}
