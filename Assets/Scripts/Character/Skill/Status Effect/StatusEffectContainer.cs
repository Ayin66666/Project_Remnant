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


    /// <summary>
    /// 최초 1회 호출 - 호출 위치 CharacterBase / 소환 직후
    /// </summary>
    /// <param name="owner"></param>
    public void SetUp(CharacterBase owner)
    {
        this.owner = owner;
    }

    /// <summary>
    /// 버프 & 디버프 추가
    /// </summary>
    /// <param name="newEffect"></param>
    /// <param name="power"></param>
    /// <param name="count"></param>
    public void Add(StatusEffectSO newEffect, int power, int count)
    {

    }

    /// <summary>
    /// 버프 & 디버프 제거
    /// </summary>
    public void Remove()
    {

    }
}

/// <summary>
/// 런타임에서 버프 & 디버프의 종류, 위력, 횟수를 담은 클래스
/// </summary>
[System.Serializable]
public class StatusEffectInfo
{
    public StatusEffectSO effectSO;
    public int power;
    public int count;
}
