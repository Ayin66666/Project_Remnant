using UnityEngine;


public enum BuffType
{
    // �Ϲ� ���� & �����
    AttackPoint, DefensePoint, DamageIncrease, DamageReduction, minSpeed, maxSpeed,

    // 6�� Ű����
    Submersion, Burn, Vibration, Rupture, Bleeding, Charge
}


public abstract class Buff_Base : MonoBehaviour, IBuffEffect
{
    [Header("---Data---")]
    public BuffType type;
    public int buff_Value;
    public int buff_Duration;


    public abstract void Use();
    public abstract void UpdateBuff();
    public abstract void Type_Change(Buff_Base buff);
}
