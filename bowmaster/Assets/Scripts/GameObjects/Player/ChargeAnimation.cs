using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeAnimation : MonoBehaviour {

    bool _isDone = true;

    [SerializeField]
    PlayerController pc;

    [SerializeField]
    Transform quiverPoint;

    [SerializeField]
    Transform bowPoint;

    public bool isDone { get { return _isDone; } }

	public void RunAnimation (float time) {
        StartCoroutine(Animation(time));
    }

    IEnumerator Animation(float timePerStep)
    {
        _isDone = false;
        pc.endArrow = pc.handPointer;

        float timeToEnd = Time.time + timePerStep;
        Transform movePoint = pc.endArrow;
        Vector3 startPos = movePoint.position;

        while (timeToEnd > Time.time)
        {
            Vector3 pos = GetHandTargetPosition(startPos, quiverPoint.position, 1 - ((timeToEnd - Time.time) / timePerStep));
            movePoint.position = pos;
            yield return null;
        }

        movePoint.position = quiverPoint.position;

        GameObject arrow = Instantiate(pc.arrowPrefab,
                                       quiverPoint.position,
                                       quiverPoint.rotation,
                                       movePoint) as GameObject;

        timeToEnd = Time.time + timePerStep;
        startPos = movePoint.position;
        Quaternion startRot = quiverPoint.rotation;

        while (timeToEnd > Time.time)
        {
            float step = 1 - ((timeToEnd - Time.time) / timePerStep);
            movePoint.position = GetHandTargetPosition(startPos, bowPoint.position, step);
            arrow.transform.rotation = Quaternion.Lerp(startRot, bowPoint.rotation, step);
            arrow.transform.localPosition = Vector3.zero;
            yield return null;
        }

        movePoint.position = bowPoint.position;
        arrow.transform.rotation = bowPoint.rotation;
        arrow.transform.localPosition = Vector3.zero;

        pc.chargedArrow = arrow.GetComponent<Arrow>();
        pc.endArrow = pc.chargedArrow.getEndTransform();

        pc.isCharged = true;

        _isDone = true;
    }

    private Vector3 GetHandTargetPosition(Vector3 posStart, Vector3 posTarget, float step)
    {
        Vector3 pos = Vector3.Lerp(posStart, posTarget, step);
        int insideCircle = MathFuncs.IsInsideCircle(
            pos.x, pos.y, pc.lengthForeArm,
            pc.arm_back.position.x, pc.arm_back.position.y, pc.lengthBackArm
            );

        if (insideCircle == -1)
        {
            float d = pc.lengthForeArm - pc.lengthBackArm + 0.1f;
            Vector3 dir = new Vector3(pos.x - pc.arm_back.position.x, pos.y - pc.arm_back.position.y, 0);
            return dir.normalized * d + pc.arm_back.position;
        }
        
        return pos;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(quiverPoint.position, bowPoint.position);
    }
}
