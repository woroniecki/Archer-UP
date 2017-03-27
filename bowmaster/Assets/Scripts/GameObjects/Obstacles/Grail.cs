using UnityEngine;


/// <summary>
/// Grail obstacle which push or pull arrows in some range
/// </summary>
public class Grail : MonoBehaviour
{

    /// <summary>
    /// rotating speed of fill
    /// </summary>
    public float rotateSpeed = 50;
    /// <summary>
    /// push strength, if lower than 0 grail will pull arrows
    /// </summary>
    public float pushPower = 1;
    /// <summary>
    /// radius of circle in which pushing works
    /// </summary>
    public float radius = 3;


    /// <summary>
    /// fill of grail
    /// </summary>
    private Transform fillT;

    void Start()
    {
        fillT = Utility.FindChildByName("Fill", transform);
    }

    void Update()
    {
        RotateFill();
        PushArrows();
    }

    /// <summary>
    /// rotate fill of grail
    /// </summary>
    void RotateFill()
    {
        fillT.Rotate(Vector3.forward * Time.deltaTime * rotateSpeed);
    }

    /// <summary>
    /// push all active arrows in range
    /// </summary>
    void PushArrows()
    {
        foreach (Transform arrow in ArrowsContainer.arrowsActive)
        {
            Vector3 D = fillT.position - arrow.position;
            float dist = D.magnitude;
            Vector3 pullDir = D.normalized;
            Vector2 pullDir2d = new Vector2(pullDir.x, pullDir.y);
            if (dist < radius)
            {
                dist = (10 - dist);
                arrow.GetComponent<Rigidbody2D>().velocity += pullDir2d * dist * (pushPower * Time.deltaTime);
            }
        }
    }
}
