using UnityEngine;
using System;

public class Accelerometer : MonoBehaviour
{

    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (playerController == null)
            Debug.LogError("Accelerometer:Start() Couldn't find PlayerController component");
    }

    /// <summary>
    /// by accelerometer input x set angle of camera quateration and arms of player character
    /// </summary>
    void FixedUpdate()
    {
        float angle = 90 - (float)Math.Round((float)MathFuncs.equationX(Input.acceleration.x + 1, 2, 180), 0);
        angle *= 2;
        angle = MathFuncs.forks(angle, 180, -180);
        angle = MathFuncs.getValueInRange(angle, -90, 90);
        float currentAngle = transform.rotation.eulerAngles.z;
        currentAngle = MathFuncs.forks(currentAngle, 180, -180);

        float angleStep = (angle - currentAngle) / 15;
        float targetAngle = currentAngle + angleStep;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
        playerController.setArms(targetAngle);
    }
}
