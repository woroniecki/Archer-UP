using UnityEngine;

public static class MathFuncs
{

    /// <summary>
    /// return value of equation (returned var)/x2Max = x/maxX
    /// </summary>
    /// <param name="x">value of known side in equation</param>
    /// <param name="maxX">max value of known side in equation</param>
    /// <param name="x2Max">max value of side where is unknown var</param>
    /// <returns></returns>
    public static float equationX(float x, float maxX, float x2Max)
    {
        return (x * x2Max) / maxX;
    }

    /// <summary>
    /// check if number is in range between min and max 
    /// </summary>
    /// <param name="value">checked value</param>
    /// <param name="min">min value in range</param>
    /// <param name="max">max value in range</param>
    /// <returns>return true if value is lower than max and higher than min</returns>
    public static bool isValueInRange(float value, float min, float max)
    {
        if (value < min)
            return false;
        if (value > max)
            return false;
        return true;
    }

    /// <summary>
    /// If value is lower than min return min, if higher than max return max
    /// </summary>
    /// <param name="value">checked value</param>
    /// <param name="min">min returned value</param>
    /// <param name="max">max returned value</param>
    /// <returns>If value is lower than min return min, if higher than max return max or return value</returns>
    public static float getValueInRange(float value, float min, float max)
    {
        if (value < min)
            return min;
        if (value > max)
            return max;
        return value;
    }

    /// <summary>
    /// Return angle in radians between 3 ponits ABC in cartesian coordinate system 
    /// </summary>
    /// <param name="x1">x of point A</param>
    /// <param name="y1">y of point A</param>
    /// <param name="x2">x of point B</param>
    /// <param name="y2">y of point B</param>
    /// <param name="x3">x of point C</param>
    /// <param name="y3">y of point C</param>
    /// <returns>Return angle in radians between 3 ponits ABC in cartesian coordinate system </returns>
    public static float angleBetweenThreePoints(float x1, float y1, float x2, float y2, float x3, float y3)
    {
        // a
        float p12 = lengthBetweenPoints(x1, y1, x2, y2);
        // b
        float p23 = lengthBetweenPoints(x2, y2, x3, y3);
        // c
        float p13 = lengthBetweenPoints(x1, y1, x3, y3);

        return Mathf.Acos((Mathf.Pow(p12, 2) + Mathf.Pow(p23, 2) - Mathf.Pow(p13, 2)) / (2 * p12 * p23));
    }

    /// <summary>
    /// Return length between two points AB in cartesian coordinate system
    /// </summary>
    /// <param name="x1">x of point A</param>
    /// <param name="y1">y of point A</param>
    /// <param name="x2">x of point B</param>
    /// <param name="y2">y of point B</param>
    /// <returns>Return length between two points in cartesian coordinate system</returns>
	public static float lengthBetweenPoints(float x1, float y1, float x2, float y2)
    {
        return Mathf.Sqrt(Mathf.Pow(x1 - x2, 2) + Mathf.Pow(y1 - y2, 2));
    }

    /// <summary>
    /// return value greater or reduced by range between higher and lower, if value is out of this range
    /// </summary>
    /// <param name="x">checked value</param>
    /// <param name="higher">higher bound range</param>
    /// <param name="lower">lower bound range</param>
    /// <returns>return value greater or reduced by range between higher and lower, if value is out of this range</returns>
    public static float forks(float x, float higher, float lower)
    {
        if (x > higher)
            return x - (higher - lower);
        if (x < lower)
            return x + (higher - lower);
        return x;
    }

    /// <summary>
    /// return angles of in triangle by length of triangle sides
    /// </summary>
    /// <param name="a">length of side a</param>
    /// <param name="b">length of side b</param>
    /// <param name="c">length of side c</param>
    /// <returns>return Vector3 with angles in degrees, where x is angle in A corner(BA), y in B corner(BC), z in C corner(CA)</returns>
    public static Vector3 getAnglesFromTriangle(float a, float b, float c)
    {
        float cos_ba = (Mathf.Pow(a, 2) + Mathf.Pow(b, 2) - Mathf.Pow(c, 2)) / (2 * a * b);
        float cos_bc = (Mathf.Pow(b, 2) + Mathf.Pow(c, 2) - Mathf.Pow(a, 2)) / (2 * b * c);
        float cos_ca = (Mathf.Pow(c, 2) + Mathf.Pow(a, 2) - Mathf.Pow(b, 2)) / (2 * c * a);
        return new Vector3(Mathf.Acos(cos_ba) * Mathf.Rad2Deg,
                           Mathf.Acos(cos_bc) * Mathf.Rad2Deg,
                           Mathf.Acos(cos_ca) * Mathf.Rad2Deg);
    }

    // Find the points where the two circles intersect.
    public static int FindCircleCircleIntersections(
        float cx0, float cy0, float radius0,
        float cx1, float cy1, float radius1,
        out Vector2 intersection1, out Vector2 intersection2)
    {
        // Find the distance between the centers.
        float dx = cx0 - cx1;
        float dy = cy0 - cy1;
        float dist = Mathf.Sqrt(dx * dx + dy * dy);

        // See how many solutions there are.
        if (dist > radius0 + radius1)
        {
            // No solutions, the circles are too far apart.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return -1;
        }
        else if (dist < Mathf.Abs(radius0 - radius1))
        {
            // No solutions, one circle contains the other.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return -2;
        }
        else if ((dist == 0) && (radius0 == radius1))
        {
            // No solutions, the circles coincide.
            intersection1 = new Vector2(float.NaN, float.NaN);
            intersection2 = new Vector2(float.NaN, float.NaN);
            return -3;
        }
        else
        {
            // Find a and h.
            float a = (radius0 * radius0 -
                radius1 * radius1 + dist * dist) / (2 * dist);
            float h = Mathf.Sqrt(radius0 * radius0 - a * a);

            // Find P2.
            float cx2 = cx0 + a * (cx1 - cx0) / dist;
            float cy2 = cy0 + a * (cy1 - cy0) / dist;

            // Get the points P3.
            intersection1 = new Vector2(
                (float)(cx2 + h * (cy1 - cy0) / dist),
                (float)(cy2 - h * (cx1 - cx0) / dist));
            intersection2 = new Vector2(
                (float)(cx2 - h * (cy1 - cy0) / dist),
                (float)(cy2 + h * (cx1 - cx0) / dist));

            if (dist == radius0 + radius1) return 1;
            return 2;
        }
    }

    public static int IsInsideCircle(float cx0, float cy0, float radius0,
        float cx1, float cy1, float radius1)
    {
        float dx = cx0 - cx1;
        float dy = cy0 - cy1;
        float dist = Mathf.Sqrt(dx * dx + dy * dy);

        // See how many solutions there are.
        if (dist > radius0 + radius1)
        {
            // No solutions, the circles are too far apart.
            return 1;
        }
        else if (dist < Mathf.Abs(radius0 - radius1))
        {
            // No solutions, one circle contains the other.
            return -1;
        }
        else if ((dist == 0) && (radius0 == radius1))
        {
            // No solutions, the circles coincide.
            return 0;
        }
        return 2;
    }

    public static void LookAt2D(this Transform t, Vector3 target){
        Vector3 diff = target - t.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        t.rotation = Quaternion.Euler(0f, 0f, rot_z);
    }
}
