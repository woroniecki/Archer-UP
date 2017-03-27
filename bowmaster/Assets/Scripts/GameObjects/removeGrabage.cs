using UnityEngine;

/// <summary>
/// destroy element if is out of map
/// </summary>
public class removeGrabage : MonoBehaviour
{

    private Transform cameraTransform;

    void Start()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void FixedUpdate()
    {
        if (!MathFuncs.isValueInRange(gameObject.transform.position.y, cameraTransform.position.y - 70, cameraTransform.position.y + 70) ||
            !MathFuncs.isValueInRange(gameObject.transform.position.x, cameraTransform.position.x - 70, cameraTransform.position.x + 70))
        {
            DestroyObject();
        }
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
