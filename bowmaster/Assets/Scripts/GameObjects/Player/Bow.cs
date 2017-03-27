using UnityEngine;


public class Bow : MonoBehaviour
{
    /// <summary>
    /// bottom part of bow string
    /// </summary>
    LineRenderer stringBot;
    /// <summary>
    /// top part of bow string
    /// </summary>
    LineRenderer stringTop;
    /// <summary>
    /// joint of bottom string with bow
    /// </summary>
    Transform pointStringBot;
    /// <summary>
    /// joint of top string with bow
    /// </summary>
    Transform pointStringTop;

    void Start()
    {
        Transform stringBot_ = Utility.FindChildByName("stringBot", transform);
        if (stringBot_ == null)
            Debug.LogError("Bow:Start() Couldn't find child stringBot");

        Transform stringTop_ = Utility.FindChildByName("stringTop", transform);
        if (stringBot_ == null)
            Debug.LogError("Bow:Start() Couldn't find child stringTop");

        stringBot = stringBot_.GetComponent<LineRenderer>();
        stringTop = stringTop_.GetComponent<LineRenderer>();

        pointStringBot = Utility.FindChildByName("pointStringBot", transform);
        if (pointStringBot == null)
            Debug.LogError("Bow:Start() Couldn't find child pointStringBot");

        pointStringTop = Utility.FindChildByName("pointStringTop", transform);
        if (pointStringTop == null)
            Debug.LogError("Bow:Start() Couldn't find child pointStringTop");
    }

    /// <summary>
    /// set position of strings relative to x, y point
    /// </summary>
    /// <param name="x">x of point</param>
    /// <param name="y">y of point</param>
    public void setStrings(float x, float y)
    {
        stringBot.SetPosition(0, new Vector3(x, y, 0));
        stringBot.SetPosition(1, pointStringBot.position);
        stringTop.SetPosition(0, new Vector3(x, y, 0));
        stringTop.SetPosition(1, pointStringTop.position);
    }
}
