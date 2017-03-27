using UnityEngine;

/// <summary>
/// element which is moving left
/// e.g. water
/// </summary>
public class MovingEl : MonoBehaviour
{

    /// <summary>
    /// speed of moving
    /// </summary>
    public float speed = 1f;
    /// <summary>
    /// after pass this x is teleport to start position
    /// </summary>
    public float boundX = 0f;
    /// <summary>
    /// x of start position
    /// </summary>
    public float startX = 0f;

    void Update()
    {
        transform.Translate(-Time.deltaTime * speed, 0, 0);
        MoveToStart();
    }

    /// <summary>
    /// move element to start of way
    /// </summary>
    void MoveToStart()
    {
        if (transform.position.x <= boundX)
            transform.position = new Vector3(startX, transform.position.y, transform.position.z);
    }
}
