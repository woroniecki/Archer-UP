using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls gravity level, game end and menuUI
/// </summary>
public class GameController : MonoBehaviour
{

    /// <summary>
    /// gravity scale in game which arrows will have
    /// </summary>
    public float gravityScale_ = 1;
    public static float gravityScaleStatic = 1;
    public static float gravityScale
    {
        get
        {
            return gravityScaleStatic;
        }
    }
    /// <summary>
    /// Time to finnish level
    /// </summary>
    public float timeForLevel = 10;
    private int typeNO;
    private string type = "";
    private int levelNO = 0;

    private bool isNextLevel = true;
    private int typeNONext = 0;
    private string typeNext = "";
    private int levelNONext = 0;

    private int shootedTargetsAmount = 0;
    private int maxShootedTargetsAmount = 0;
    Hourglass hourglass;

    public static bool isGameDone = false;
    public static bool gameDone
    {
        get
        {
            return isGameDone;
        }
        set
        {
            isGameDone = value;
        }
    }

    void Awake()
    {

        if (SaveManager.instance == null)
            SaveManager.Create();

        isGameDone = false;
        gravityScaleStatic = gravityScale_;
    }

    void Start()
    {
        timeForLevel *= LevelMenu.getTimeAmountMultiply();
        SetDetailsLevel();
        Menu.loadMenuType(StateVar.getHeaderColor(type), StateVar.getTextColor(type));
        hourglass = GameObject.FindGameObjectWithTag("Hourglass").GetComponent<Hourglass>();
        hourglass.startTiming(timeForLevel);
        maxShootedTargetsAmount = GameObject.FindGameObjectsWithTag("Target").Length;
    }

    /// <summary>
    /// Listening Endgame
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("Endgame", EndGame);
    }

    /// <summary>
    /// if time is run out trigger endgame
    /// </summary>
    void Update()
    {
        if (!gameDone && (hourglass.refresh() <= 0
                          || shootedTargetsAmount == maxShootedTargetsAmount))
        {
            EventManager.TriggerEvent("Endgame", transform);
        }
    }

    /// <summary>
    /// set type of menu:
    /// "forest", "desert", "winter", "graveyard"
    /// it depeneds on scene name
    /// </summary>
    void SetDetailsLevel()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (!int.TryParse(sceneName[sceneName.Length - 1].ToString(), out levelNO))
        {
            return;
        }
        type = sceneName.Substring(0, sceneName.Length - 1);
        for (int i = 0; i < StateVar.typeAmount; i++)
        {
            if (type == StateVar.levelTypes[i])
                typeNO = i;
        }
        typeNONext = typeNO;
        levelNONext = levelNO + 1;
        if (levelNONext > StateVar.levelAmountPerLevel)
        {
            levelNONext = 1;
            typeNONext += 1;
            if (typeNONext >= StateVar.typeAmount)
                isNextLevel = false;
        }
        if (!(typeNONext >= StateVar.typeAmount))
            typeNext = StateVar.levelTypes[typeNONext];
    }

    /// <summary>
    /// set shooted targets amount
    /// </summary>
    /// <param name="amount">amount to set</param>
    public void SetShootedTargetsAmount(int amount)
    {
        shootedTargetsAmount = amount;
    }

    /// <summary>
    /// set score of level - stars amount and save
    /// active next level button if player can enter
    /// active menu canvas
    /// </summary>
    /// <param name="trigger"></param>
    void EndGame(Transform trigger)
    {
        gameDone = true;
        Transform highScoreT = Utility.FindChildByName("HighScore", transform);
        Transform scoreT = Utility.FindChildByName("Score", transform);
        int score = (int)(((float)shootedTargetsAmount / (float)maxShootedTargetsAmount) * 3);
        for (int i = 0; i < score; i++)
        {
            Transform starTransform = Utility.FindInChildsByName("Star" + (i + 1).ToString(), scoreT.transform);
            Utility.FindChildByName("Star", starTransform).GetComponent<Image>().enabled = true;
        }
        int highScore = SaveManager.instance.data.GetData(type + levelNO.ToString(), 0, SaveData.SaveTypeByLevel());
        if (score > highScore)
        {
            highScore = score;
            SaveManager.instance.data.SetData(type + levelNO.ToString(), highScore, SaveData.SaveTypeByLevel());
        }
        for (int i = 0; i < highScore; i++)
        {
            Transform starTransform = Utility.FindInChildsByName("Star" + (i + 1).ToString(), highScoreT.transform);
            Utility.FindChildByName("Star", starTransform).GetComponent<Image>().enabled = true;
        }
        if (typeNONext * StateVar.levelAmountPerLevel + levelNONext - 1 < StateVar.typeAmount * StateVar.levelAmountPerLevel)
            if (!(Menu.getTotalScore() >= StateVar.requiredScore[typeNONext * StateVar.levelAmountPerLevel + levelNONext - 1]))
            {
                isNextLevel = false;
            }
        if (!isNextLevel)
        {
            Transform nextButton = Utility.FindChildByName("NextLevelButton", transform);
            nextButton.gameObject.SetActive(false);
        }
        Menu.setActiveCanvas_("MainCanvas");
    }

    public void RealoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(typeNext + levelNONext);
    }
}
