using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public bool mainMenuLoad = false;

    /// <summary>
    /// text color of headers
    /// </summary>
    private Color headerColor;
    /// <summary>
    /// text color of less important components than header
    /// </summary>
    private Color textColor;
    /// <summary>
    /// type of menu: "forest", "desert", "winter", "graveyard"
    /// </summary>
    public static string menuType = "";
    /// <summary>
    /// total score from all levels
    /// </summary>
    private float score;

    void Awake()
    {
        //resetTotalScore(0);
        score = getTotalScore();
        if (menuType == "")
            setMenuType();
        loadMenuType(menuType);
        if (mainMenuLoad)
        {
            setLevels();
            setBG(menuType);
        }
    }

    /// <summary>
    /// return total score from all maps
    /// </summary>
    /// <returns>return total score from all maps</returns>
    public static float getTotalScore()
    {
        float total = 0;
        for (int y = 0; y < StateVar.typeAmount; y++)
        {
            for (int x = 0; x < StateVar.levelAmountPerLevel; x++)
            {
                total += PlayerPrefs.GetInt(StateVar.levelTypes[y] + (x + 1).ToString(), 0);
            }
        }
        return total;
    }

    /// <summary>
    /// set total score to be on points param
    /// </summary>
    /// <param name="points"></param>
    public static void resetTotalScore(int points)
    {
        for (int y = 0; y < StateVar.typeAmount; y++)
        {
            for (int x = 0; x < StateVar.levelAmountPerLevel; x++)
            {
                if (points > 3)
                    PlayerPrefs.SetInt(StateVar.levelTypes[y] + (x + 1).ToString(), 3);
                else
                    PlayerPrefs.SetInt(StateVar.levelTypes[y] + (x + 1).ToString(), points);
                points -= 3;
                if (points < 0)
                    points = 0;
            }
        }
    }

    void setLevels()
    {
        GameObject iconLevel = Resources.Load("UI/Level/LevelButton", typeof(GameObject)) as GameObject;
        GameObject LevelCanvas = null;
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            if (canvas.name == "PlayCanvas")
                LevelCanvas = canvas;
        }
        if (LevelCanvas == null)
            return;
        for (int y = 0; y < StateVar.typeAmount; y++)
        {
            string imagePath = "Levels/" + StateVar.levelTypes[y] + "/BG" + StateVar.levelTypes[y];
            Sprite image = Resources.Load(imagePath, typeof(Sprite)) as Sprite;
            for (int x = 0; x < StateVar.levelAmountPerLevel; x++)
            {
                GameObject icon = Instantiate(iconLevel) as GameObject;
                icon.transform.SetParent(LevelCanvas.transform);
                RectTransform rectT = icon.GetComponent<RectTransform>();
                rectT.localPosition = new Vector3(x * 120 - 220, -y * 90 + 180, 0);
                rectT.localScale = new Vector3(1, 1, 1);
                Image uiImage = Utility.FindInChildsByName("Image", icon.transform).GetComponent<Image>();
                uiImage.sprite = image;
                Utility.FindInChildsByName("Frame", icon.transform).GetComponent<Image>().color = textColor;
                if (score < StateVar.requiredScore[y * StateVar.levelAmountPerLevel + x])
                {
                    uiImage.color = StateVar.getDarkMaskColor();
                    continue;
                }
                string capturedSceneName = StateVar.levelTypes[y] + (x + 1).ToString();
                icon.GetComponent<Button>().onClick.AddListener(() => loadScene(capturedSceneName));
                for (int i = 0; i < PlayerPrefs.GetInt(StateVar.levelTypes[y] + (x + 1).ToString(), 0); i++)
                {
                    Transform starTransform = Utility.FindInChildsByName("Star" + (i + 1).ToString(), icon.transform);
                    starTransform.GetComponent<Image>().enabled = true;
                    Utility.FindChildByName("Star", starTransform).GetComponent<Image>().enabled = true;
                }
            }
        }
    }

    void setMenuType()
    {
        int indexLevelType = 0;
        for (int y = 0; y < StateVar.typeAmount; y++)
        {
            for (int x = 0; x < StateVar.levelAmountPerLevel; x++)
            {
                if (score < StateVar.requiredScore[y * StateVar.levelAmountPerLevel + x])
                    continue;
                indexLevelType = y;
            }
        }
        menuType = StateVar.levelTypes[indexLevelType];
    }

    void setBG(string type)
    {
        Transform bg = Utility.FindInChildsByName("BG", transform);
        if (bg != null)
        {
            string bgPath = "Levels/" + type + "/BG" + type;
            bg.GetComponent<Image>().sprite = Resources.Load(bgPath, typeof(Sprite)) as Sprite;
        }
    }

    void loadMenuType(string type)
    {
        headerColor = StateVar.getHeaderColor(type);
        textColor = StateVar.getTextColor(type);
        loadMenuType(headerColor, textColor);
    }

    public static void loadMenuType(Color headerC, Color textC)
    {
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            foreach (Transform child in canvas.gameObject.GetComponentsInChildren<Transform>())
            {
                if (child.name == "Header")
                {
                    child.GetComponent<Text>().color = headerC;
                }
                else if (child.name == "Text")
                {
                    child.GetComponent<Text>().color = textC;
                }
            }
        }
    }

    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void realoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void setActiveCanvas(string name)
    {
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        GameObject.Find(name).GetComponent<Canvas>().enabled = true;
    }

    static public void setActiveCanvas_(string name)
    {
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        GameObject.Find(name).GetComponent<Canvas>().enabled = true;
    }
}
