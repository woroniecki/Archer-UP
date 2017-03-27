using UnityEngine;

/// <summary>
/// Monster which is appearing and hiding in ground
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
public class Diglet : Target {

    /// <summary>
    /// moving speed
    /// </summary>
    public float speed;
    /// <summary>
    /// break between hiding and showing
    /// </summary>
    public float breakTimeCd = 0.5f;

    private float breakTime = 0;
    private int directionUp = -1;
    private float height;
    private bool isDead = false;
    private BoxCollider2D boxCol;

    void Start ()
    {
        height = GetComponent<SpriteRenderer>().bounds.size.x + 0.05f;
        boxCol = GetComponent<BoxCollider2D>();
    }

	void Update () {
        Move();
    }

    /// <summary>
    /// Hide and show monster
    /// </summary>
    void Move ()
    {
        if (Time.time < breakTime)
           return;
        float step = Time.deltaTime * speed * directionUp;
        transform.position += new Vector3(0, step, 0);
        if (transform.localPosition.y < -height)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, -height, transform.localPosition.z);
            directionUp = 1;
            breakTime = Time.time + breakTimeCd;
            if (isDead)
                Destroy(gameObject);
        }
        if (transform.localPosition.y > 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, 0, transform.localPosition.z);
            directionUp = -1;
            breakTime = Time.time + breakTimeCd;
        }
        if (!isDead)
            setBoxColliderSize();
    }

    /// <summary>
    /// set size of box collider depending on visible part of monster
    /// </summary>
    void setBoxColliderSize()
    {
        // 0 - h = 1.1, y = 0.25
        // -0.78 h = 0.1, y = 0.3
        // -0.875 h = 0, y = 0.39
        float h = MathFuncs.equationX(0.875f + transform.localPosition.y, 0.875f, 1.1f);
        float y = -0.25f + MathFuncs.equationX(-transform.localPosition.y, 0.875f, 0.64f);
        boxCol.size = new Vector2(boxCol.size.x, h);
        boxCol.offset = new Vector2(boxCol.offset.x, y);
    }

    override public void Hit()
    {
        base.Hit();
        isDead = true;
        directionUp = -1;
        breakTime = 0;
        speed = 3;
        Destroy(GetComponent<BoxCollider2D>());
    }
}
