using UnityEngine;

/// <summary>
/// contains all ui stars and control target points of stars in real world(which are flying)
/// </summary>
public class StarsContainer : MonoBehaviour
{

    /// <summary>
    /// transform of object which represents point where stars should go in unscaled canvas
    /// </summary>
    public Transform notScaledPos;

    private int amount = 0;
    // width of star in ui
    private float widthUI = 0;
    // width of star in real world
    private float widthRealWorld = 1;
    GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("MenuCanvas").GetComponent<GameController>();
        widthUI = Utility.FindChildByName("StarI_comparator", transform).GetComponent<RectTransform>().rect.width;
        Vector3 realSize = (Camera.main.ScreenPointToRay(new Vector3(widthUI, 0, 0)).origin
                           - Camera.main.ScreenPointToRay(new Vector3(0, 0, 0)).origin);
        widthRealWorld = realSize.x;
    }

    /// <summary>
    /// increase star amount and set amount of shooted targets in gamecontroller
    /// </summary>
    /// <returns>return amount of stars decreased by one - index of added star</returns>
    public int addStar()
    {
        amount += 1;
        gameController.setShootedTargetsAmount(amount);
        return amount - 1;
    }

    /// <summary>
    /// create star as ui component in position set by number of star
    /// </summary>
    /// <param name="starNo"></param>
    public void createUiStar(int starNo)
    {
        GameObject star = Instantiate(Resources.Load("UI/Star/StarImage"),
                                      new Vector3(notScaledPos.transform.position.x - ((widthUI + 4) * starNo),
                                                  notScaledPos.transform.position.y,
                                                  notScaledPos.transform.position.z),
                                      Quaternion.identity,
                                      transform) as GameObject;
    }

    /// <summary>
    /// get size of ui star in real world
    /// </summary>
    /// <returns>return size of ui star in real world</returns>
    public float getRealStarSize()
    {
        return widthRealWorld;
    }

    /// <summary>
    /// return target position of star in real world
    /// </summary>
    /// <param name="starNo">position is set by no of star</param>
    /// <returns>return target position of star in real world</returns>
    public Vector3 getWorldPosStar(int starNo)
    {
        Vector3 pos = new Vector3(notScaledPos.transform.position.x - ((widthUI + 4) * starNo),
                                  notScaledPos.transform.position.y,
                                  notScaledPos.transform.position.z);
        pos = Camera.main.ScreenPointToRay(pos).origin;
        return new Vector3(pos.x, pos.y, 0);
    }
}
