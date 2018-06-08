using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// menu where player choose difficult level of game
/// </summary>
public class LevelMenu : MonoBehaviour
{

    public enum difficultyLevel {
        Easy = 0,
        Medium = 1,
        Difficult = 2
    };

    public Transform difficultButton;
    public Transform mediumButton;
    public Transform easyButton;

    void Start()
    {
        SetLevel(SaveManager.instance.data.GetData(
            StateVar.prefLevelName, (int)difficultyLevel.Medium, SaveData.saveDictionariesTypes.options)
            );
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
    public void SetLevel(int difficultLevel)
    {
        setButtonState(difficultButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(mediumButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(easyButton, StateVar.getTextColor(Menu.menuType), 30);
        setButtonState(Utility.FindChildByName(((difficultyLevel)difficultLevel).ToString(), transform),
                       StateVar.getHeaderColor(Menu.menuType), 50);
        SaveManager.instance.data.SetData(StateVar.prefLevelName, (int)difficultLevel, SaveData.saveDictionariesTypes.options);
    }

    /// <summary>
    /// get amount to multiply time in level which depends of difficult level set
    /// Easy - 0
    /// Medium - 1
    /// Difficult - 2
    /// </summary>
    /// <returns>amount to multiply level time</returns>
    public static float getTimeAmountMultiply()
    {
        difficultyLevel difficultLevel = GetCurrentDifficultyLevel();
        switch (difficultLevel) {
            case difficultyLevel.Easy:
                return 4;
            case difficultyLevel.Medium:
                return 2;
            case difficultyLevel.Difficult:
                return 1;
        }
        Debug.Log("LevelMenu:getTimeAmountMultiply() Difficult level not exist");
        return 2;
    }

    public static difficultyLevel GetCurrentDifficultyLevel()
    {
        return (difficultyLevel)SaveManager.instance.data.GetData(
            StateVar.prefLevelName, 
            (int)difficultyLevel.Medium, 
            SaveData.saveDictionariesTypes.options
            );
    }
}
