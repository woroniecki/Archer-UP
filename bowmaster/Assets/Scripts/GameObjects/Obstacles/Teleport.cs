using UnityEngine;


/// <summary>
/// teleport script, use with two objects then arrow can teleport between them
/// </summary>
public class Teleport : MonoBehaviour
{
    /// <summary>
    /// max width of teleport, animation effect
    /// </summary>
    public float maxWidth = 0.8f;
    /// <summary>
    /// min width of teleport, animation effect
    /// </summary>
    public float minWidth = 0.4f;
    /// <summary>
    /// speed of animation
    /// </summary>
    public float speed = 1;
    /// <summary>
    /// second connected teleport
    /// </summary>
    public Teleport targetTeleport;

    /// <summary>
    /// state of animation, 1 - is extending, -1 - is narrowing
    /// </summary>
    private int isExtending = 1;

    void Update()
    {
        Animation();
    }

    /// <summary>
    /// Animation extending of core
    /// </summary>
    void Animation()
    {
        float step = transform.localScale.x + Time.deltaTime * speed * isExtending;
        transform.localScale = new Vector3(step, transform.localScale.y, 0);
        if (transform.localScale.x > maxWidth)
        {
            isExtending *= -1;
            transform.localScale = new Vector3(maxWidth, transform.localScale.y, 1);
        }
        else if (transform.localScale.x < minWidth)
        {
            isExtending *= -1;
            transform.localScale = new Vector3(minWidth, transform.localScale.y, 1);
        }
    }

    /// <summary>
    /// return transform of teleport
    /// </summary>
    /// <returns>return transform of teleport</returns>
    public Transform getTransform()
    {
        return transform;
    }

    /// <summary>
    /// Destroy arrow object on trigger enter and create new in target teleport
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Arrow")
        {
            Vector3 offset = coll.transform.position - transform.position;
            GameObject explo = Instantiate(Resources.Load("Arrow/ArrowDestroy"),
                                           coll.transform.position,
                                           coll.transform.rotation) as GameObject;
            if (targetTeleport == null)
                Debug.LogError("Teleport:OnTriggerEnter2D(Collider2D coll) targetTeleport is not assgined)");
            coll.transform.position = targetTeleport.getTransform().position - offset;
            GameObject exploAfterTp = Instantiate(Resources.Load("Arrow/ArrowAppear"),
                                           coll.transform.position,
                                           coll.transform.rotation) as GameObject;
            Vector3 velocity = coll.transform.GetComponent<Rigidbody2D>().velocity;
            exploAfterTp.GetComponent<AnimationAutoDestroy>().activeArrowApper(coll.gameObject, velocity);
            coll.gameObject.SetActive(false);
        }
    }
}
