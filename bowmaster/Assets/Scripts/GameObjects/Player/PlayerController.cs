using UnityEngine;

/// <summary>
/// Controls player body
/// </summary>
public class PlayerController : ArrowShooter
{
    Transform arm_front;
    Transform arm_back;
    Transform forearm_back;
    float armB_len;
    float fore_armB_len;

    void Start()
    {
        base.DoInitialization();
        arm_front = Utility.FindChildByName("arm_f", transform);
        arm_back = Utility.FindChildByName("arm_b", transform);
        if (arm_back == null)
            Debug.LogError("PlayerController:Start() arm_back is not assigned");

        forearm_back = Utility.FindChildByName("forearm_b", arm_back);
        if (forearm_back == null)
            Debug.LogError("PlayerController:Start() forearm_back is not assigned");

        armB_len = MathFuncs.lengthBetweenPoints(arm_back.position.x, arm_back.position.y,
                                                 forearm_back.position.x, forearm_back.position.y);
        Transform hand = Utility.FindChildByName("arrow_point", forearm_back);
        fore_armB_len = MathFuncs.lengthBetweenPoints(forearm_back.position.x, forearm_back.position.y,
                                                      hand.position.x, hand.position.y) + 0.1f;
    }

    /// <summary>
    /// set arms rotation and bow strings position
    /// </summary>
    /// <param name="degree">angle which arms rotation will be set</param>
    public void setArms(float degree)
    {
        setFrontArm(degree);
        setBackArm(degree);
        bowScript.setStrings(endArrow.position.x, endArrow.position.y);
    }

    /// <summary>
    /// set rotation front arm
    /// </summary>
    /// <param name="degree">angle which front arm rotation will be set</param>
	public void setFrontArm(float degree)
    {
        if (arm_front == null)
        {
            Debug.LogError("PlayerController:setFrontArm(float degree) arm_front is not assigned");
            return;
        }
        arm_front.rotation = Quaternion.Euler(new Vector3(0, 0, degree));
    }

    /// <summary>
    /// set rotation of back arm and position relative to arrow
    /// </summary>
    /// <param name="degree">angle which back arm rotation will be set</param>
    public void setBackArm(float degree)
    {
        float length_a = MathFuncs.lengthBetweenPoints(arm_back.position.x, arm_back.position.y,
                                                       endArrow.position.x, endArrow.position.y);
        if (length_a > armB_len + fore_armB_len)
            return;
        if (armB_len > length_a + fore_armB_len)
            return;
        if (fore_armB_len > armB_len + length_a)
            return;
        float angle_zero = MathFuncs.angleBetweenThreePoints(endArrow.position.x, endArrow.position.y,
                                                             arm_back.position.x, arm_back.position.y,
                                                             arm_back.position.x - 10, arm_back.position.y) * Mathf.Rad2Deg;
        Vector3 triangle_angles = MathFuncs.getAnglesFromTriangle(length_a, armB_len, fore_armB_len);
        if (endArrow.position.y >= arm_back.position.y)
            arm_back.rotation = Quaternion.Euler(new Vector3(0, 0, -(angle_zero - triangle_angles.x) + 180));
        else
            arm_back.rotation = Quaternion.Euler(new Vector3(0, 0, triangle_angles.x + 180 + angle_zero));
        forearm_back.localRotation = Quaternion.Euler(new Vector3(0, 0, -triangle_angles.y));
    }
}
