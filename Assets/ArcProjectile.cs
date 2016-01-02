using UnityEngine;
using System.Collections;

public class ArcProjectile : MonoBehaviour
{
    public Transform PositionA;
    public Transform PositionB;
    public Transform Midpoint;

    public int Segments = 8;

    LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        lineRenderer.SetVertexCount(Segments);
        var plottedPoints = MathEx.GetBezierPoints(PositionA.position, Midpoint.position, PositionB.position, Segments);
        for (int i = 0; i < plottedPoints.Length; i++)
        {
            lineRenderer.SetPosition(i, plottedPoints[i]);
        }
    }
}
