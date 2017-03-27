using UnityEngine;

/// <summary>
/// Destroy arrow objects on trigger enter
/// </summary>
public class DarkGrail : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.transform.tag == "Arrow")
        {
            GameObject explo = Instantiate(Resources.Load("Arrow/ArrowDestroy"),
                                           coll.transform.position,
                                           coll.transform.rotation) as GameObject;
            coll.GetComponent<Arrow>().DestroyObject();
        }
    }
}
