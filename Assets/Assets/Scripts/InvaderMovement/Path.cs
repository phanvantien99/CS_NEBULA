using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private List<GameObject> _pointsObject = new List<GameObject>();
    [SerializeField] private PathType pathType;
    private Vector3[] waysPoint;

    private List<Vector3> _waysPointTemp = new List<Vector3>();

    public Vector3[] WaysPoint { get => waysPoint; set => waysPoint = value; }

    // Start is called before the first frame update
    private void OnEnable() {
        foreach (Transform child in transform)
        {
            _pointsObject.Add(child.gameObject);
        }
    }

    public void convertWayPoint()
    {
        _waysPointTemp.Clear();
        for (int i = 0; i < _pointsObject.Count; i++)
        {
            _waysPointTemp.Add(_pointsObject[i].transform.position);
        }
        WaysPoint = _waysPointTemp.ToArray();
    }

    #region DrawPath

    private void OnDrawGizmos()
    {
        convertWayPoint();
        if (WaysPoint == null || WaysPoint.Length < 2)
            return;

        switch (pathType)
        {
            case PathType.CatmullRom:
                DrawCatmullRomPath();
                break;
            case PathType.CubicBezier:
                DrawCubicBezierPath();
                break;
            case PathType.Linear:
                DrawLinearPath();
                break;
        }
    }

    private void DrawCatmullRomPath()
    {
        Gizmos.color = Color.red;
        List<Vector3> points = GetCatmullRomPoints(WaysPoint);

        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }

    private List<Vector3> GetCatmullRomPoints(Vector3[] waysPoint, int resolution = 10)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < waysPoint.Length - 1; i++)
        {
            Vector3 p0 = i == 0 ? waysPoint[i] : waysPoint[i - 1];
            Vector3 p1 = waysPoint[i];
            Vector3 p2 = waysPoint[i + 1];
            Vector3 p3 = i == waysPoint.Length - 2 ? waysPoint[i + 1] : waysPoint[i + 2];

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 point = GetCatmullRomPosition(t, p0, p1, p2, p3);
                points.Add(point);
            }
        }

        return points;
    }
    private Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float t2 = t * t;
        float t3 = t2 * t;

        float b0 = 0.5f * (-t3 + 2.0f * t2 - t);
        float b1 = 0.5f * (3.0f * t3 - 5.0f * t2 + 2.0f);
        float b2 = 0.5f * (-3.0f * t3 + 4.0f * t2 + t);
        float b3 = 0.5f * (t3 - t2);

        return (b0 * p0) + (b1 * p1) + (b2 * p2) + (b3 * p3);
    }
    private void DrawCubicBezierPath()
    {
        Gizmos.color = Color.green;
        List<Vector3> points = GetCubicBezierPoints(WaysPoint);

        for (int i = 0; i < points.Count - 1; i++)
        {
            Gizmos.DrawLine(points[i], points[i + 1]);
        }
    }

    private List<Vector3> GetCubicBezierPoints(Vector3[] waysPoint, int resolution = 10)
    {
        List<Vector3> points = new List<Vector3>();

        for (int i = 0; i < waysPoint.Length - 3; i += 3)
        {
            Vector3 p0 = waysPoint[i];
            Vector3 p1 = waysPoint[i + 1];
            Vector3 p2 = waysPoint[i + 2];
            Vector3 p3 = waysPoint[i + 3];

            for (int j = 0; j <= resolution; j++)
            {
                float t = j / (float)resolution;
                Vector3 point = GetCubicBezierPosition(t, p0, p1, p2, p3);
                points.Add(point);
            }
        }

        return points;
    }

    private Vector3 GetCubicBezierPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float ttt = tt * t;
        float uuu = uu * u;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }

    private void DrawLinearPath()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < WaysPoint.Length - 1; i++)
        {
            Gizmos.DrawLine(WaysPoint[i], WaysPoint[i + 1]);
        }
    }
    #endregion

}
