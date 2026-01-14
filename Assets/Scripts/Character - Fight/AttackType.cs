/// <summary>
/// 참격  , 관통, 타격 - 공격 타입
/// </summary>
public enum AttackType 
{ 
    Slash, 
    Pierce, 
    Blunt 
}

/// <summary>
/// 일반 데미지, 키워드 데미지
/// </summary>
public enum DamageType
{ 
    Normal, 
    Keyword 
}

/// <summary>
/// 분노, 색욕, 나태, 탐식, 우울, 오만, 질투 - 에고 자원 & 스킬 유형
/// </summary>
public enum Crime
{
    Wrath, // 분노
    Lust, // 색욕
    Sloth, // 나태
    Gula, // 탐식
    Gloom, // 우울
    Pride, // 오만
    Envy // 질투
}