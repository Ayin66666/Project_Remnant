using UnityEngine;

public interface IDamageable
{
    /// <summary>
    /// 피격 시 데미지 계산 함수
    /// </summary>
    /// <param name="info"></param>
    public void TakeDamage(AttackInfo info);

    /// <summary>
    /// 정신력 데미지 계산 로직 / 침잠 등 정신력 관련 데미지를 받았을 때 동작
    /// </summary>
    /// <param name="mDamage"></param>
    public void TakeMDamage(int mDamage);

    /// <summary>
    /// 흐트러짐 게이지 계산 로직 / 진동 등 흐트러짐 게이지를 당기는 효과를 받았을 때 호출
    /// </summary>
    /// <param name="sDamage"></param>
    public void TakeSDamage(int sDamage);

    /// <summary>
    /// 사망 로직
    /// </summary>
    public void Die();
}

/// <summary>
/// 계산 결과 전달용 구조체
/// </summary>
[System.Serializable]
public struct AttackInfo
{
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
    /// 치명타 여부 - UI 표시용
    /// </summary>
    public bool isCritical;
    /// <summary>
    /// 공격 데미지
    /// </summary>
    public int damage;
}

