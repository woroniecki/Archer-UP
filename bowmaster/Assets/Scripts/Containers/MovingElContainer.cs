using UnityEngine;

/// <summary>
/// Container of elements which move state
/// e.g. water
/// all childs of gameobject should be moving elements which have component MovingEl
/// </summary>
public class MovingElContainer : MonoBehaviour
{

    private float widthEl = 1f;
    private float boundX = 50000f;
    private float startX = -50000f;

    /// <summary>
    /// set width of moving elements, x of start bound and x of end bound
    /// it set bounds for all childs of gameobject which have MovingEl component
    /// </summary>
    void Start()
    {
        widthEl = transform.GetChild(0).GetComponent<SpriteRenderer>().bounds.size.x;
        foreach (Transform child in transform)
        {
            if (child.position.x < boundX)
                boundX = child.position.x;
            if (child.position.x > startX)
                startX = child.position.x;
        }
        boundX -= widthEl;
        startX -= 0.1f;
        foreach (Transform child in transform)
        {
            MovingEl movingEl = child.gameObject.AddComponent<MovingEl>();
            movingEl.boundX = boundX;
            movingEl.startX = startX;
        }
    }
}
