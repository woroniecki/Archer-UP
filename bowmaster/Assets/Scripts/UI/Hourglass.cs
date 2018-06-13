using UnityEngine;
using UnityEngine.UI;

public class Hourglass : Singleton<Hourglass>
{

    private Scrollbar topSand;
    private Scrollbar botSand;
    private Scrollbar fallingSand;
    private float time;
    private float timeStart;

    // Use this for initialization
    void Start()
    {
        topSand = Utility.FindChildByName("TopSand", transform).GetComponent<Scrollbar>();
        botSand = Utility.FindChildByName("BotSand", transform).GetComponent<Scrollbar>();
        fallingSand = Utility.FindChildByName("FallingSand", transform).GetComponent<Scrollbar>();
    }

    /// <summary>
    /// set time in timer
    /// </summary>
    /// <param name="time_">time which will set for hourglass</param>
    public void startTiming(float time_)
    {
        timeStart = Time.time;
        time = time_;
    }

    /// <summary>
    /// refresh state of hourglass
    /// </summary>
    /// <returns>return time left to end counting, if lower than zero is time which expired after finnish</returns>
    public float refresh()
    {
        float spentTime = Time.time - timeStart;
        float valueTopSand = MathFuncs.equationX(spentTime, time, 1);
        if (valueTopSand > 1)
            valueTopSand = 1;
        topSand.size = 1 - valueTopSand;
        botSand.size = valueTopSand;
        fallingSand.size = 1 - valueTopSand;
        topSand.value = 0;
        botSand.value = 0;
        fallingSand.value = 0;
        return time - spentTime;
    }

    /// <summary>
    /// end level on click hourglass
    /// </summary>
    public void clickButton()
    {
        time = 0;
        refresh();
        EventManager.TriggerEvent("Endgame", transform);
    }

}
