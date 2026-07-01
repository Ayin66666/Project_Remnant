using UnityEngine;

public static class SkillUIUtility
{

    /// <summary>
    /// °¢ ÁË¾Ç¿¡ ¸Â´Â »ö»ó Àü´Þ
    /// </summary>
    /// <param name="crime">ÁË¾Ç Á¾·ù</param>
    /// <returns></returns>
    public static Color GetCrimeColor(SinType crime)
    {
        return crime switch
        {
            SinType.Pride => new Color32(0, 0, 255, 255), // Blue
            SinType.Wrath => new Color32(255, 0, 0, 255), // Red
            SinType.Lust => new Color32(255, 128, 0, 255), // Orange
            SinType.Sloth => new Color32(255, 255, 0, 255), // Yellow
            SinType.Gula => new Color32(0, 255, 0, 255), // Green
            SinType.Gloom => new Color32(135, 206, 235, 255), // Sky Blue
            SinType.Envy => new Color32(128, 0, 128, 255), // Purple
            _ => Color.white
        };
    }
}
