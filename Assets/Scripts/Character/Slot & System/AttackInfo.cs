using UnityEngine;


/// <summary>
/// 공격자의 데미지 정보 전달 구조체
/// </summary>
[System.Serializable]
public struct AttackInfo
{
    // 스킬 데이터, 타겟 데이터, 치명타 여부
    [Header("---Setting---")]
    /// <summary>
    /// 데미지의 죄악 종류
    /// </summary>
    public SinType sinType;
    /// <summary>
    /// 공격 타입 - 참관타
    /// </summary>
    public AttackType attackType;

    /// <summary>
    /// 공격자의 공격 레벨
    /// </summary>
    public int attackPoint;
    /// <summary>
    /// 공격의 배율
    /// </summary>
    public float motionValue;
    /// <summary>
    /// 치명타 여부 - true면 치명타, false면 일반 공격
    /// </summary>
    public bool isCritical;
    /// <summary>
    /// 치명타 배율 - 기본 배율은 1.5
    /// </summary>
    public float critMultiplier;
}

[System.Serializable]
/// <summary>
/// 키워드 데미지 정보 구조체
/// </summary>
public struct DamageInfo
{
    [Header("---Setting---")]
    public SinType sinType;
    public AttackType attackType;
    public int damage;
}

