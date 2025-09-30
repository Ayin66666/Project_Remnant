using UnityEngine;


public class BuffFactory : MonoBehaviour
{
    public static BuffFactory instance;

    // 일반 버프
    // AttackPoint, DefensePoint, DamageIncrease, DamageReduction, minSpeed, maxSpeed

    // 6대 키워드
    // Submersion, Burn, Vibration, Rupture, Bleeding, Charge


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    #region 생성 로직
    public Buff_Status Create_Buff_Status(BuffType type, int buff_Value, int buff_Duration)
    {
        Buff_Status buff = new Buff_Status()
        {
            type = type,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 침잠
    /// </summary>
    /// <param name="type"></param>
    /// <param name="suType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Submersion Create_Buff_Submersion(BuffType type, Buff_Submersion.SubmersionType suType, int buff_Value, int buff_Duration)
    {
        Buff_Submersion buff = new Buff_Submersion()
        {
            type = type,
            submersionType = suType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 화상
    /// </summary>
    /// <param name="type"></param>
    /// <param name="buType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Burn Create_Buff_Burn(BuffType type, Buff_Burn.BurnType buType, int buff_Value, int buff_Duration)
    {
        Buff_Burn buff = new Buff_Burn()
        {
            type = type,
            burnType = buType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 진동
    /// </summary>
    /// <param name="type"></param>
    /// <param name="viType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Vibration Create_Buff_Vibration(BuffType type, Buff_Vibration.VibrationType viType, int buff_Value, int buff_Duration)
    {
        Buff_Vibration buff = new Buff_Vibration()
        {
            type = type,
            vibrationType = viType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 파열
    /// </summary>
    /// <param name="type"></param>
    /// <param name="ruType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Rupture Create_Buff_Rupture(BuffType type, Buff_Rupture.RuptureType ruType, int buff_Value, int buff_Duration)
    {
        Buff_Rupture buff = new Buff_Rupture()
        {
            type = BuffType.Rupture,
            ruptureType = ruType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 출혈
    /// </summary>
    /// <param name="type"></param>
    /// <param name="buType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Bleeding Create_Buff_Bleeding(BuffType type, Buff_Bleeding.BleddingType buType, int buff_Value, int buff_Duration)
    {
        Buff_Bleeding buff = new Buff_Bleeding()
        {
            type = BuffType.Bleeding,
            bleddingType = buType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }

    /// <summary>
    /// 충전
    /// </summary>
    /// <param name="type"></param>
    /// <param name="chType"></param>
    /// <param name="buff_Value"></param>
    /// <param name="buff_Duration"></param>
    /// <returns></returns>
    public Buff_Charge Create_Buff_Charge(BuffType type, Buff_Charge.ChargeType chType, int buff_Value, int buff_Duration)
    {
        Buff_Charge buff = new Buff_Charge()
        {
            type = BuffType.Charge,
            chargeType = chType,
            buff_Value = buff_Value,
            buff_Duration = buff_Duration
        };

        return buff;
    }
    #endregion
}
