using System.Collections;
using UnityEditor;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float _maxThrowDistance;

    [SerializeField] private float _ellipseRatio;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _debugColor;

    public void ThoweObject(IThrowable thowerableObject, Vector3 mousePosition)
    {
        IThrowable thowerableObjectCopy = Instantiate(thowerableObject.Copy(), transform.position, Quaternion.identity).GetComponent<IThrowable>();
        thowerableObject.Copy().layer = gameObject.layer;
        StartCoroutine(thowerableObjectCopy.Move(transform.position, GetFallPoint(mousePosition) + _offset));
    }

    private Vector3 GetFallPoint(Vector3 mousePosition)
    {
        return Trigonometry.CalculatePointOnCircle(transform.position, mousePosition, _maxThrowDistance, _ellipseRatio);
    }

    private void OnDrawGizmos()
    {
        int segments = 64;
        Handles.color = _debugColor;

        Vector3[] points = new Vector3[segments + 1];
        Vector3 center = transform.position + _offset;

        for (int i = 0; i <= segments; i++)
        {
            float angle = 2 * Mathf.PI * i / segments;
            float x = Mathf.Cos(angle) * _maxThrowDistance;
            float y = Mathf.Sin(angle) * _maxThrowDistance * _ellipseRatio;
            points[i] = center + new Vector3(x, y, 0);
        }

        Handles.DrawAAConvexPolygon(points); // Закрашенный многоугольник
    }
}
