public interface IDamageSystem
{
    // 분노, 색욕, 나태, 탐식, 우울, 오만, 질투
    public enum SinType { Anger, Lust, Sloth, Gluttony, Melancholy, Pride, Envy }

    // 참격, 관통, 타격
    public enum DamageType { Slash, Pierce, Strike }

    // 데미지 위치
    public enum DamageTarget { Hp, Groggy }

    public void TakeDamage(SinType sin, DamageType type, bool isCriticl, int hitcount, int damage);
}
