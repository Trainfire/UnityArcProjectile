using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour
{
    public Transform Begin;
    public Transform End;

    public int Segments;
    public float Slack = 0.5f;
    public float Width = 0.1f;

    public bool useLocalNormalForSlack = false;

    LineRenderer lineRenderer;
    EdgeCollider2D edgeCollider;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        edgeCollider = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        lineRenderer.SetWidth(Width, Width);
        lineRenderer.SetVertexCount(Segments);

        Vector3[] points = MathEx.GetBezierPoints(Begin.position, GetSlack(), End.position, Segments);
        for (int i = 0; i < points.Length; i++)
        {
            lineRenderer.SetPosition(i, points[i]);
        }

        List<Vector2> points2D = new List<Vector2>();
        for (int i = 0; i < points.Length; i++)
        {
            points2D.Add(new Vector2(points[i].x, points[i].y));
        }

        edgeCollider.points = points2D.ToArray();

        var normal = MathEx.GetNormal(Begin.position, End.position);
        var lerp = Vector3.Lerp(Begin.position, End.position, 0.5f);
        Debug.DrawLine(lerp, lerp + normal * 5f);
    }

    Vector3 GetSlack()
    {
        var midpoint = Vector3.Lerp(Begin.position, End.position, 0.5f);

        if (useLocalNormalForSlack)
        {
            var normal = MathEx.GetNormal(End.position, Begin.position);
            return midpoint + normal * Slack;
        }

        return new Vector3(midpoint.x, midpoint.y - Slack, midpoint.z);
    }
}
