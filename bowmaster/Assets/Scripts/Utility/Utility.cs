using UnityEngine;

public class Utility
{

    /// <summary>
    /// looking for child in object_ childs by name. Check only childs which are first node.
    /// </summary>
    /// <param name="name">child's name which is looking for</param>
    /// <param name="object_">transform og object which childs will be searched</param>
    /// <returns>return child if found or null</returns>
    public static Transform FindChildByName(string name, Transform object_)
    {
        Transform childObject = object_.FindChild(name);
        if (childObject != null)
            return childObject;
        Debug.Log("Couldn't find child " + name + " in object " + object_.name);
        return null;
    }

    /// <summary>
    /// Get first child by name in all nodes of object_
    /// </summary>
    /// <param name="name">child's name which is looking for</param>
    /// <param name="object_">transform og object which childs will be searched</param>
    /// <returns>Null or found child</returns>
    public static Transform FindInChildsByName(string name, Transform object_)
    {
        Transform childObject = null;
        foreach (Transform child in object_.gameObject.GetComponentsInChildren<Transform>())
        {
            if (child.name == name)
            {
                return child;
            }
        }
        if (childObject != null)
            return childObject;
        Debug.Log("Couldn't find in childs " + name + " in object " + object_.name);
        return null;
    }

    /// <summary>
    /// return Vector3 v which is moved by x, y and z 
    /// </summary>
    /// <param name="v">vector which will be moved</param>
    /// <param name="x">x value</param>
    /// <param name="y">y value</param>
    /// <param name="z">z value</param>
    /// <returns>return Vector3 v which is moved by x, y and z </returns>
    public static Vector3 MoveVector(Vector3 v, float x, float y, float z)
    {
        return new Vector3(v.x + x, v.y + y, v.z + z);
    }

    /// <summary>
    /// return deep copy of Quateration
    /// </summary>
    /// <param name="q">Quateration which will be copied</param>
    /// <returns>return deep copy of Quateration</returns>
    public static Quaternion copyQuateration(Quaternion q)
    {
        return new Quaternion(q.x, q.y, q.z, q.w);
    }
}

