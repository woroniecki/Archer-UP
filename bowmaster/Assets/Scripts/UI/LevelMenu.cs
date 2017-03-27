using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// menu where player choose difficult level of game
/// </summary>
public class LevelMenu : MonoBehaviour
{

    public Transform difficultButton;
    public Transform mediumButton;
    public Transform easyButton;

    void Start()
    {
        setLevel(PlayerPrefs.GetString(StateVar.prefLevelName, "Medium"));
    }

    /// <summary>
    /// set button level look
    /// </summary>
    /// <param name="button"></param>
    /// <param name="color">text color of button</param>
    /// <param name="fontSize">font size of button</param>
    void setButtonState(Transform button, Color color, int fontSize)
    {
        Text t = Utility.FindChildByName("Text", button).GetComponent<Text>();
        if (t == null)
            Debug.LogError("LevelMenu:setButtonState(Transform button, Color color, int fontSize)\n couldn't find child Text or component Text in child");
        t.color = color;
        t.fontSize = fontSize;
    }

    /// <summary>
    /// Set color of button which should be active (this difficult level is sets)
    /// </summary>
    /// <param name="levelName">name of level (Difficult, Medium, Easy) must be same as a button</param>
    public void setLevel(string levelName)
    {
        setButtonState(difficultButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(mediumButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(easyButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(Utility.FindChildByName(levelName, transform),
                       StateVar.getHeaderColor(Menu.menuType), 50);
        PlayerPrefs.SetString(StateVar.prefLevelName, levelName);
    }

    /// <summary>
    /// get amount to multiply time in level which depends of difficult level set
    /// </summary>
    /// <returns>amount to multiply level time</returns>
    public static float getTimeAmountMultiply()
    {
        string difficultLevel = PlayerPrefs.GetString(StateVar.prefLevelName);
        if (difficultLevel == "Difficult")
            return 1;
        if (difficultLevel == "Medium")
            return 2;
        if (difficultLevel == "Easy")
            return 4;
        Debug.Log("LevelMenu:getTimeAmountMultiply() Difficult level not exist");
        return 2;
    }
}
