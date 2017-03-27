using UnityEngine;

/// <summary>
/// Monster which is flying in some distance and turn around
/// </summary>
public class TargetBird : Target
{
    /// <summary>
    /// distance of flying
    /// </summary>
    public float distance;
    /// <summary>
    /// speed of flying
    /// </summary>
    public float speed;
    /// <summary>
    /// Set for first direction. If true fly to right.
    /// </summary>
    public bool rightDirection;


    private float startPosX;
    private float endPosX;
    private int directionForward;

    private bool isDead = false;

    void Start()
    {
        startPosX = transform.position.x;
        if (rightDirection)
        {
            directionForward = 1;
            endPosX = startPosX + distance;
        }
        else
        {
            directionForward = -1;
            endPosX = startPosX;
            startPosX = startPosX - distance;
        }
        transform.localScale = new Vector3(transform.localScale.x * directionForward,
                                           transform.localScale.y,
                                           transform.localScale.z);
    }

    void Update()
    {
        if (!isDead)
            Move();
        else
            deadRotate();
    }

    /// <summary>
    /// Move object
    /// </summary>
    void Move()
    {
        float step = transform.position.x + Time.deltaTime * speed * directionForward;
        transform.position = new Vector3(step, transform.position.y, 0);
        if (transform.position.x > endPosX)
        {
            directionForward *= -1;
            transform.localScale = new Vector3(transform.localScale.x * directionForward,
                                               transform.localScale.y,
                                               transform.localScale.z);
            transform.position = new Vector3(endPosX, transform.position.y, 0);
        }
        else if (transform.position.x < startPosX)
        {
            transform.localScale = new Vector3(transform.localScale.x * directionForward,
                                               transform.localScale.y,
                                               transform.localScale.z);
            directionForward *= -1;
            transform.position = new Vector3(startPosX, transform.position.y, 0);
        }
    }

    /// <summary>
    /// rotate to down on death
    /// </summary>
    void deadRotate()
    {
        if (Mathf.Abs(MathFuncs.forks(transform.rotation.eulerAngles.z, 180, -180)) < 90)
            transform.Rotate(Vector3.forward * Time.deltaTime * -100 * directionForward);
    }

    override public void Hit()
    {
        base.Hit();
        isDead = true;
        Destroy(GetComponent<CircleCollider2D>());
        gameObject.AddComponent<Rigidbody2D>();
        GetComponent<Animator>().SetBool("isDead", true);
        Destroy(Utility.FindChildByName("eye", transform).gameObject);
    }
}
