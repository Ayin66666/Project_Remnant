using UnityEngine;

public static class SkillUIUtility
{

    /// <summary>
    /// °¢ ÁË¾Ç¿¡ ¸Â´Â »ö»ó Àü´Þ
    /// </summary>
    /// <param name="crime">ÁË¾Ç Á¾·ù</param>
    /// <returns></returns>
    public static Color GetCrimeColor(Crime crime)
    {
        return crime switch
        {
            Crime.Pride => new Color32(0, 0, 255, 255), // Blue
            Crime.Wrath => new Color32(255, 0, 0, 255), // Red
            Crime.Lust => new Color32(255, 128, 0, 255), // Orange
            Crime.Sloth => new Color32(255, 255, 0, 255), // Yellow
            Crime.Gula => new Color32(0, 255, 0, 255), // Green
            Crime.Gloom => new Color32(135, 206, 235, 255), // Sky Blue
            Crime.Envy => new Color32(128, 0, 128, 255), // Purple
            _ => Color.white
        };
    }
}
