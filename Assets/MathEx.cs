using UnityEngine;
using System.Collections;

public static class MathEx
{
    public static Vector3[] GetBezierPoints(Vector3 start, Vector3 midpoint, Vector3 end, int segments)
    {
        Vector3[] points = new Vector3[segments];
        float t = 0f;
        for (int i = 0; i < segments; i++)
        {
            t = i / ((float)segments - 1);
            points[i] = MathEx.GetQuadraticCoordinates(t, start, midpoint, end);
        }
        return points;
    }

    public static Vector3 GetQuadraticCoordinates(float t, Vector3 p0, Vector3 c0, Vector3 p1)
    {
        return Mathf.Pow(1 - t, 2) * p0 + 2 * t * (1 - t) * c0 + Mathf.Pow(t, 2) * p1;
    }

    public static Vector3 GetNormal(Vector3 start, Vector3 end)
    {
        var direction = start - end;
        var v = Vector3.Cross(direction, Vector3.forward);
        return v.normalized;
    }
}
