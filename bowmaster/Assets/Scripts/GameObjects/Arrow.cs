using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Arrow : removeGrabage
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCol;
    /// <summary>
    /// max speed of arrow
    /// </summary>
    private float maxSpeed = 100;
    /// <summary>
    /// 0 - is charged, 1 - is flying, 2 - hit target
    /// </summary>
    private int physicsState = 0;
    /// <summary>
    /// power which arrow has before shoot
    /// </summary>
    private float power = 0;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        boxCol = gameObject.GetComponent<BoxCollider2D>();
        transform.localPosition = Utility.MoveVector(transform.localPosition, -0.2f, 0, 0);
        EnablePhysic(false);
    }


    void Update()
    {
        if (physicsState == 1)
        {
            if (rb.velocity.y != 0 && rb.velocity.x != 0)
            {
                limitSpeed();
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    /// <summary>
    /// limit current speed to maxSpeed
    /// </summary>
    void limitSpeed()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }

    /// <summary>
    /// Shoot arrow by player, trigger event "Shoot"
    /// </summary>
    public void Shoot()
    {
        EnablePhysic(true);
        physicsState = 1;
        transform.parent = null;
        rb.AddForce(transform.right * getStrengthFromPower(power));
        EventManager.TriggerEvent("Shoot", transform);
    }

    /// <summary>
    /// set power which arrow has before shoot
    /// </summary>
    /// <param name="power_">power which will be set</param>
    public void setPower(float power_)
    {
        float deltaPower = power - power_;
        transform.localPosition = Utility.MoveVector(transform.localPosition, -(deltaPower * 0.5f), 0, 0);
        power = power_;
    }

    /// <summary>
    /// get end of arrow
    /// </summary>
    /// <returns>return object which is on the end of arrow object</returns>
    public Transform getEndTransform()
    {
        return Utility.FindChildByName("end", transform);
    }

    /// <summary>
    /// enable or disable gravity and box collider
    /// </summary>
    /// <param name="enable">if true than enable in other wise disable physic</param>
    void EnablePhysic(bool enable)
    {
        if (!enable)
        {
            rb.velocity = Vector3.zero;
            rb.Sleep();
        }
        else
        {
            rb.gravityScale = GameController.gravityScale;
            rb.WakeUp();
        }
        boxCol.enabled = enable;
    }

    public override void DestroyObject()
    {
        EventManager.TriggerEvent("ArrowDisable", transform);
        base.DestroyObject();
    }

    /// <summary>
    /// behave on hit of target or obstacle
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag != "Player"
            && coll.transform.tag != "Arrow"
             && coll.transform.tag != "Teleport")
        {
            EnablePhysic(false);
            EventManager.TriggerEvent("ArrowDisable", transform);
            physicsState = 2;
            if (coll.tag == "Target")
            {
                transform.parent = coll.transform;
                coll.gameObject.GetComponent<Target>().Hit();
            }
        }
    }

    /// <summary>
    /// return strength with which arrow will be shooted based on p (power)
    /// </summary>
    /// <param name="p">power</param>
    /// <returns>return strength with which arrow will be shooted based on p (power)</returns>
    static float getStrengthFromPower(float p)
    {
        return 50f + p * 850f;
    }
}
