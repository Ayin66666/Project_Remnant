/// <summary>
/// 공격자의 데미지 정보 전달 구조체
/// </summary>
public struct DamageInfo
{
    /// <summary>
    /// 공격 타입
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
    /// 데미지 출력을 몇번 쪼개서 보여주는지
    /// </summary>
    public int attackCount;
    /// <summary>
    /// 치명타 확률 - 광역 공격의 경우 개별 확률 계산
    /// </summary>
    public float critChance;
    /// <summary>
    /// 치명타 배율 - 기본 배율은 1.5
    /// </summary>
    public float critMultiplier;
}
