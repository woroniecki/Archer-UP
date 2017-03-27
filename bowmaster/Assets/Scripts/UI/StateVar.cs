using UnityEngine;

/// <summary>
/// Contains const variables, and 
/// </summary>
public class StateVar
{

    public static int typeAmount = 4;
    public static int levelAmountPerLevel = 5;
    public static string[] levelTypes = new string[4] { "forest", "desert", "winter", "graveyard" };
    public static int[] requiredScore = new int[20] {0, 1, 2, 3, 5, // 0, 3, 6, 9, 12
                                                     7, 9, 11, 14, 17, // 15, 18, 21, 24, 27
                                                     20, 23, 27, 31, 35, // 30, 33, 36, 39, 42
                                                     39, 43, 47, 52, 57}; // 45, 48, 51, 54, 57
    public static string prefLevelName = "gameLevel";

    public static Color getHeaderColor(string levelName)
    {
        Color color = Color.black;
        if (levelName == "forest")
            ColorUtility.TryParseHtmlString("#06AB00FF", out color);
        if (levelName == "desert")
            ColorUtility.TryParseHtmlString("#CA7900FF", out color);
        if (levelName == "winter")
            ColorUtility.TryParseHtmlString("#1C00B2FF", out color);
        if (levelName == "graveyard")
            ColorUtility.TryParseHtmlString("#000000FF", out color);
        return color;
    }

    public static Color getTextColor(string levelName)
    {
        Color color = Color.black;
        if (levelName == "forest")
            ColorUtility.TryParseHtmlString("#007C10FF", out color);
        if (levelName == "desert")
            ColorUtility.TryParseHtmlString("#6D4100FF", out color);
        if (levelName == "winter")
            ColorUtility.TryParseHtmlString("#002478FF", out color);
        if (levelName == "graveyard")
            ColorUtility.TryParseHtmlString("#0F0F0FFF", out color);
        return color;
    }

    public static Color getDarkMaskColor()
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString("#707070FF", out color);
        return color;
    }

}
