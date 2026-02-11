using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EGO_CharacterName_Rank_EgoName", menuName = "EGO/EGOMaster", order = int.MaxValue)]
public class EgoMasterSO : ScriptableObject
{
    [Header("---Data---")]
    public CharacterId sinner;
    public string egoId;
    public Rank egoRank;
    public int coinPower;
    public int targetCount;
    public List<CoinInfo> coins;
    [Header("자원 순서 = 분노, 색욕, 나태, 탐식, 우울, 오만, 질투")]
    public List<int> egoCost = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };


    [Header("---UI---")]
    public string egoName;
    [TextArea] public string egoDescription;
    public Sprite egoIcon;


    [System.Serializable]
    public struct CoinInfo
    {
        /// <summary>
        /// 스킬 배율
        /// </summary>
        public float value;
        /// <summary>
        /// 타격 횟수 (데미지 / hitCount)
        /// </summary>
        public int hitCount;
    }
}


/// <summary>
/// 에고 런타임 데이터
/// </summary>
[System.Serializable]
public class EgoInfo
{
    public CharacterId sinner;
    public List<EgoData> info;
}

[System.Serializable]
public class EgoData
{
    [Header("---Status---")]
    public bool isUnlocked;
    public int sync;

    [Header("---Reference---")]
    public EgoMasterSO master;
}

public enum Rank
{
    ZAYIN,
    TETH,
    HE,
    WAW,
    ALEPH
}
