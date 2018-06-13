using UnityEngine;
using System;

public class Accelerometer : Singleton<Accelerometer>
{
    static public float accelerometerInputMultiplier;

    public static Accelerometer instance;

    [Range(-0.01f, 2f)]
    public float instantValue = -0.01f;
    public PlayerController playerController;

    private void OnEnable()
    {
        if(instance == null)
            instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    /// <summary>
    /// by accelerometer input x set angle of camera quateration and arms of player character
    /// </summary>
    void FixedUpdate()
    {

        float accelerationValue = (Input.acceleration.x * accelerometerInputMultiplier) + 1;

#if UNITY_EDITOR
        if (Input.GetJoystickNames().Length > 0)
        {
            accelerationValue = -1 * Input.GetAxis("Vertical2") + 1;
        }
        else {
            if (Input.GetAxis("Vertical") != 0) {
                accelerationValue = Input.GetAxis("Vertical") + 1;
            }
        }
        if(instantValue != -0.01f)
        {
            accelerationValue = instantValue;
        }
#endif

        float angle = 90 - (float)Math.Round((float)MathFuncs.equationX(accelerationValue, 2, 180), 0);
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
