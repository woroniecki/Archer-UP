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
    Transform arrowPoint;

    [SerializeField]
    Transform bowPoint;

    [SerializeField]
    float timeToQuiver = 0.3f, timeArrowUp = 0.2f, timeRotArrow = 0.12f, timeChargeArrow = 0.4f;

    public bool isDone { get { return _isDone; } }

	public void RunAnimation (float time) {
        StartCoroutine(Animation(time));
    }

    IEnumerator Animation(float speed = 1f)
    {
        _isDone = false;
        pc.endArrow = pc.handPointer;

        float timeToQuiver = 0.3f * speed;
        float timeArrowUp = 0.2f * speed;
        float timeRotArrow = 0.12f * speed;
        float timeChargeArrow = 0.4f * speed;

        #region HAND TO QUIVER

        float timeToEnd = Time.time + timeToQuiver;
        Transform movePoint = pc.endArrow;
        Vector3 startPos = movePoint.position;

        while (timeToEnd > Time.time)
        {
            Vector3 pos = GetHandTargetPosition(startPos, quiverPoint.position, 1 - ((timeToEnd - Time.time) / timeToQuiver));
            movePoint.position = pos;
            yield return null;
        }

        movePoint.position = quiverPoint.position;

        #endregion

        #region HAND TAKE ARROW UP

        GameObject arrow = Instantiate(pc.arrowPrefab,
                                       quiverPoint.position,
                                       quiverPoint.rotation,
                                       movePoint) as GameObject;

        arrow.transform.localPosition = Vector3.zero;

        SpriteRenderer sprite = arrow.GetComponentInChildren<SpriteRenderer>();
        sprite.sortingOrder = -1;

        timeToEnd = Time.time + timeArrowUp;
        startPos = movePoint.position;

        while (timeToEnd > Time.time)
        {
            float step = 1 - ((timeToEnd - Time.time) / timeArrowUp);
            Vector3 pos = GetHandTargetPosition(startPos, arrowPoint.position, step);
            movePoint.position = pos;
            arrow.transform.localPosition = Vector3.zero;
            yield return null;
        }

        movePoint.position = arrowPoint.position;

        #endregion

        #region ROTATE ARROW AND MOVE ON FORAWRD VIEW

        Quaternion startRot = quiverPoint.rotation;
        timeToEnd = Time.time + timeRotArrow;
        startPos = movePoint.position;

        while (timeToEnd > Time.time)
        {
            float step = 1 - ((timeToEnd - Time.time) / timeRotArrow);
            arrow.transform.rotation = Quaternion.Lerp(startRot, arrowPoint.rotation, step);
            arrow.transform.localPosition = Vector3.zero;
            yield return null;
        }

        sprite.sortingOrder = 100;

        #endregion

        #region CHARGE ARROW TO BOW

        timeToEnd = Time.time + timeChargeArrow;
        startPos = movePoint.position;
        startRot = arrow.transform.rotation;

        while (timeToEnd > Time.time)
        {
            float step = 1 - ((timeToEnd - Time.time) / timeChargeArrow);
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

        #endregion

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

        if (insideCircle == -1 || insideCircle == 1)
        {
            float d = insideCircle == -1 ? 
                pc.lengthForeArm - pc.lengthBackArm + 0.1f
                : pc.lengthForeArm + pc.lengthBackArm - 0.1f;
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
