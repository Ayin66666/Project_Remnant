public interface IDamageSystem
{
    // �г�, ����, ����, Ž��, ���, ����, ����
    public enum SinType { Anger, Lust, Sloth, Gluttony, Melancholy, Pride, Envy }

    // ����, ����, Ÿ��
    public enum DamageType { Slash, Pierce, Strike }

    // ������ ��ġ
    public enum DamageTarget { Hp, Groggy }

    public void TakeDamage(SinType sin, DamageType type, bool isCriticl, int hitcount, int damage);
}
