using UnityEngine;


/// <summary>
/// 공격자의 데미지 정보 전달 구조체
/// </summary>
[System.Serializable]
public struct AttackInfo
{
    // 스킬 데이터, 타겟 데이터, 치명타 여부
    [Header("---Setting (데미지 계산에 필요한 값)---")]
    /// <summary>
    /// 데미지의 죄악 종류
    /// </summary>
    public SinType sinType;
    /// <summary>
    /// 공격 타입 - 참관타
    /// </summary>
    public AttackType attackType;
    /// <summary>
    /// 방어력 관련 계산 공식을 적용하는 공격인지 여부
    /// </summary>
    public bool isUseDamageCal;
    /// <summary>
    /// 치명타 여부 - UI 표시용
    /// </summary>
    public bool isCritical;
    /// <summary>
    /// 공격자의 공격 레벨
    /// </summary>
    public int attackPoint;
    /// <summary>
    /// 공격 데미지
    /// </summary>
    public int damage;
}

