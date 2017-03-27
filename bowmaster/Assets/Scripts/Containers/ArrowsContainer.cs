using System.Collections;
using UnityEngine;

/// <summary>
/// containes all arrows which are during fly (arrow state 1)
/// </summary>
public class ArrowsContainer : MonoBehaviour
{

    private static ArrayList arrows;

    /// <summary>
    /// contains all active arrows
    /// </summary>
    public static ArrayList arrowsActive
    {
        get
        {
            if (arrows == null)
            {
                arrows = new ArrayList();
            }
            return arrows;
        }
    }

    void Start()
    {
        arrows = new ArrayList();
    }

    /// <summary>
    /// Enable listening triggers
    /// </summary>
    void OnEnable()
    {
        EventManager.StartListening("Shoot", addArrow);
        EventManager.StartListening("ArrowDisable", removeArrow);
    }

    /// <summary>
    /// add arrow to array
    /// </summary>
    /// <param name="arrow"></param>
    public static void addArrow(Transform arrow)
    {
        arrows.Add(arrow);
    }

    /// <summary>
    /// remove arrow from array
    /// </summary>
    /// <param name="arrow"></param>
    public static void removeArrow(Transform arrow)
    {
        arrows.Remove(arrow);
    }
}
