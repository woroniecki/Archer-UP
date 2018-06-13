using UnityEngine;

/// <summary>
/// Controls player body
/// </summary>
public class PlayerController : ArrowShooter
{
    [Header("Arms animation")]
    public Transform arm_front;
    public Transform arm_back;
    public Transform forearm_back;
    public Transform cross_arm_back;
    float armB_len;
    float fore_armB_len;

    float _lengthForeArm = 1f;
    public float lengthForeArm { get { return _lengthForeArm; } }
    float _lengthBackArm = 1f;
    public float lengthBackArm { get { return _lengthBackArm; } }

    public Transform defaultStringPosPointer;

    void Start()
    {
        base.DoInitialization();
        _lengthForeArm = Vector2.Distance(cross_arm_back.position, forearm_back.position);
        _lengthBackArm = Vector2.Distance(cross_arm_back.position, arm_back.position);
    }

    /// <summary>
    /// set arms rotation and bow strings position
    /// </summary>
    /// <param name="degree">angle which arms rotation will be set</param>
    public void setArms(float degree)
    {
        setFrontArm(degree);
        setBackArm(degree);
        float x, y;
        GetHandPosition(out x, out y);
        bowScript.setStrings(x, y);
    }

    void GetHandPosition (out float x, out float y)
    {
        if (chargeAnimation.isDone)
        {
            x = endArrow.position.x;
            y = endArrow.position.y;
        }
        else
        {
            x = defaultStringPosPointer.position.x;
            y = defaultStringPosPointer.position.y;
        }
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
        Vector3 centerTarget = endArrow.position;
        Vector3 centerArm = arm_back.position;

        Vector2 out1, out2;

        int a = MathFuncs.FindCircleCircleIntersections(
            endArrow.position.x, endArrow.position.y, _lengthForeArm,
            arm_back.position.x, arm_back.position.y, _lengthBackArm,
            out out1, out out2
            );

        if (float.IsNaN(out1.x))
        {
            Debug.LogError(a);
            return;
        }
        
        arm_back.LookAt2D (out1);
        forearm_back.position = endArrow.position;
        forearm_back.LookAt2D (out1);
        cross_arm_back.position = out1;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.DrawWireDisc(arm_back.position, Vector3.forward, _lengthBackArm);
        if(endArrow)
            UnityEditor.Handles.DrawWireDisc(endArrow.position, Vector3.forward, _lengthForeArm);
    }
#endif
}
