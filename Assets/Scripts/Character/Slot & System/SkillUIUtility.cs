using UnityEngine;

public static class SkillUIUtility
{

    /// <summary>
    /// °¢ ÁË¾Ç¿¡ ¸Â´Â »ö»ó Àü´Þ
    /// </summary>
    /// <param name="crime">ÁË¾Ç Á¾·ù</param>
    /// <returns></returns>
    public static Color GetCrimeColor(Sin crime)
    {
        return crime switch
        {
            Sin.Pride => new Color32(0, 0, 255, 255), // Blue
            Sin.Wrath => new Color32(255, 0, 0, 255), // Red
            Sin.Lust => new Color32(255, 128, 0, 255), // Orange
            Sin.Sloth => new Color32(255, 255, 0, 255), // Yellow
            Sin.Gula => new Color32(0, 255, 0, 255), // Green
            Sin.Gloom => new Color32(135, 206, 235, 255), // Sky Blue
            Sin.Envy => new Color32(128, 0, 128, 255), // Purple
            _ => Color.white
        };
    }
}
