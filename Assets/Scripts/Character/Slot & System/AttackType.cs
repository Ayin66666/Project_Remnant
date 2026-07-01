/// <summary>
/// 참격  , 관통, 타격 - 공격 타입
/// </summary>
public enum AttackType 
{ 
    None,
    Slash, 
    Pierce, 
    Blunt 
}

/// <summary>
/// 분노, 색욕, 나태, 탐식, 우울, 오만, 질투 - 에고 자원 & 스킬 유형
/// </summary>
public enum SinType
{
    None, // 없음 - 무속성
    Wrath, // 분노
    Lust, // 색욕
    Sloth, // 나태
    Gula, // 탐식
    Gloom, // 우울
    Pride, // 오만
    Envy // 질투
}

/// <summary>
/// 스킬 1~3, 가드, 몬스터 공격(Attack)
/// </summary>
public enum SkillType 
{ 
    Skill1, 
    Skill2, 
    Skil3, 
    Guard, 
    Attack 
};