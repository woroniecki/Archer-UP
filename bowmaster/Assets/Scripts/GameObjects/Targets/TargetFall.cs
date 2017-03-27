using UnityEngine;

/// <summary>
/// Used for jumper monster
/// Object jump every time it touch ground
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class TargetFall : Target
{
    /// <summary>
    /// strength of jump
    /// </summary>
    public float jumpStrength = 100f;
    /// <summary>
    /// rigidbody
    /// </summary>
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * jumpStrength);
    }

    /// <summary>
    /// Jump on collision with ground
    /// </summary>
    /// <param name="coll"></param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.tag == "Ground")
            rb.AddForce(transform.up * jumpStrength);
    }

    /// <summary>
    /// fall down on hit
    /// </summary>
    override public void Hit()
    {
        base.Hit();
        if (rb.velocity.y > 0)
            rb.velocity = Vector3.zero;
        Destroy(GetComponent<CircleCollider2D>());
    }
}
