using UnityEngine;


public enum BuffType
{
    // 일반 버프 & 디버프
    AttackPoint, DefensePoint, DamageIncrease, DamageReduction, minSpeed, maxSpeed,

    // 6대 키워드
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
