using UnityEngine;
using System.Collections;


/// <summary>
/// destroy gameobject after animation and active other object if is necessery
/// is used to teleport arrow
/// and destroy arrow in grail
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationAutoDestroy : MonoBehaviour
{
    /// <summary>
    /// delay before destroy
    /// </summary>
    public float delay = 0f;

    /// <summary>
    /// object which will be activated after this is destroyed
    /// </summary>
    private GameObject objectToActive;

    /// <summary>
    /// velocity of this object
    /// </summary>
    private Vector3 velocity;

    void Start()
    {
        StartCoroutine(ExecuteAfterTime(this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay));
    }

    /// <summary>
    /// active apperaing gameobject and give it velocity, after destroy this
    /// </summary>
    /// <param name="t">gameobject</param>
    /// <param name="v">velocity</param>
    public void activeArrowApper(GameObject t, Vector3 v)
    {
        velocity = v;
        objectToActive = t;
    }

    /// <summary>
    /// activate objectToActivate and give it velocity, destroy this gameobject
    /// </summary>
    /// <param name="time">time after which will be executed</param>
    /// <returns></returns>
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        if (objectToActive)
        {
            objectToActive.SetActive(true);
            objectToActive.transform.GetComponent<Rigidbody2D>().velocity = velocity;
        }
        Destroy(gameObject);
    }
}