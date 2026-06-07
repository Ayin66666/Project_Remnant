/// <summary>
/// 공격자의 데미지 정보 전달 구조체
/// </summary>
public struct DamageInfo
{
    /// <summary>
    /// 데미지 종류 - 일반(기존 계산식), 키워드(고정피해)
    /// </summary>
    public DamageType damageType;
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
    /// <summary>
    /// 키워드 데미지 - damageType이 Keyword일 때 사용
    /// </summary>
    public int keywordDamage;
}
