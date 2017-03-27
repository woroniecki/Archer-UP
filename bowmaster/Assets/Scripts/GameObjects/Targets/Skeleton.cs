using UnityEngine;

/// <summary>
/// Monster is walking in one direction
/// </summary>
public class Skeleton : Target {
    /// <summary>
    /// moving speed
    /// </summary>
    public float speed;
    /// <summary>
    /// if true walk to right
    /// </summary>
    public bool forwardDirection = true;

    private int direction;
    private bool isDead = false;

    void Start () {
        if (forwardDirection)
            direction = 1;
        else
            direction = -1;
        transform.localScale = new Vector3 (transform.localScale.x * direction,
                                            transform.localScale.y,
                                            transform.localScale.z);
    }
	
	void Update () {
        if (!isDead)
            Move();
    }

    /// <summary>
    /// Move object
    /// </summary>
    void Move()
    {
        float step = transform.position.x + Time.deltaTime * speed * direction;
        transform.position = new Vector3(step, transform.position.y, 0);
    }

    override public void Hit()
    {
        base.Hit();
        isDead = true;
        Destroy(GetComponent<CircleCollider2D>());
        Destroy(GetComponent<BoxCollider2D>());
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Animator>().SetBool("isDead", true);
    }
}
