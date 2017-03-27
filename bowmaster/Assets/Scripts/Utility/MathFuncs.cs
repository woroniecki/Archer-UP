using UnityEngine;

public class MathFuncs
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
}
