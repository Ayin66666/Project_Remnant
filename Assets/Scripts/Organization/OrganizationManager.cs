using System.Collections.Generic;
using UnityEngine;


public class OrganizationManager : MonoBehaviour
{
    [Header("---Setting / Data---")]
    [SerializeField] private List<EgoInfo> characterEgoInfo;

    [Header("---UI---")]
    [SerializeField] private GameObject characterListSet;
    [SerializeField] private List<CharacterSelectSlot> characterSlot;
}

public enum CharacterId
{
    ch01,
    ch02,
    ch03,
    ch04,
    ch05,
    ch06
}

[System.Serializable]
public class EgoInfo
{
    public CharacterId character;
    public List<EgoData> info;
}
