using System.Collections;
using UnityEditor;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    [SerializeField] private float _maxThrowDistance;
    [SerializeField] private float _heightRatio;
    [SerializeField] private float _ellipseRatio;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Color _debugColor;

    public void ThroweObject(GameObject thowerableObject, Vector3 targetPosition)
    {
        if (thowerableObject.TryGetComponent<IThrowable>(out IThrowable throwable))
        {
            thowerableObject.transform.position = transform.position;
            thowerableObject.transform.rotation = Quaternion.identity;
            thowerableObject.layer = gameObject.layer;
            thowerableObject.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = gameObject.GetComponent<SpriteRenderer>().sortingLayerName;
            thowerableObject.GetComponent<IThrowable>().StartMove(GetFallPoint(targetPosition));
        }
    }

    private Vector3 GetFallPoint(Vector3 mousePosition)
    {
        Vector3 fallPoint = Trigonometry.CalculatePointOnCircle(transform.position, mousePosition, _maxThrowDistance, _ellipseRatio);
        Vector3 fallDirection = (fallPoint - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(fallPoint, Vector3.forward);

        if (hit.collider.gameObject.layer < gameObject.layer || hit.collider.gameObject.TryGetComponent<Wall>(out _) && hit.collider != null)
            fallPoint = new Vector3(fallPoint.x, fallPoint.y + (hit.collider.transform.position.y + _heightRatio) * fallDirection.y, fallPoint.z);

        return fallPoint;
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

        Handles.DrawAAConvexPolygon(points);
    }
}
