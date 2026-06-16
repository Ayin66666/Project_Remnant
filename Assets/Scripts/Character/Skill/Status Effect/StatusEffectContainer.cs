using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 런타임에서 버프 & 디버프 정보 리스트를 담아둔 클래스
/// </summary>
[System.Serializable]
public class StatusEffectContainer
{
    [SerializeField] private CharacterBase owner;
    public List<StatusEffectInfo> effectlist;
}

/// <summary>
/// 런타임에서 버프 & 디버프의 종류, 위력, 횟수를 담은 클래스
/// </summary>
[System.Serializable]
public class StatusEffectInfo
{
    public EffectBaseSO effectSO;
    public int power;
    public int count;
}
