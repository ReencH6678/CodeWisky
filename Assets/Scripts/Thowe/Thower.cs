using System.Collections;
using UnityEngine;

public class Thower : MonoBehaviour
{
    [SerializeField] private float _maxThrowDistance;

    [SerializeField] private float _ellipseRatio;
    [SerializeField] private Vector3 _offset;

    [SerializeField] private EllipseDebuger _ellipseDebuger;

    private void Awake()
    {
        _ellipseDebuger = GetComponent<EllipseDebuger>();

        if (_ellipseDebuger != null)
            _ellipseDebuger.CreateRangeIndicator(_maxThrowDistance, _ellipseRatio, transform, _offset);
    }

    public void ThoweObject(IThowen thowerableObject, Vector3 mousePosition)
    {
        IThowen thowerableObjectCopy = Instantiate(thowerableObject.Copy(), transform.position, Quaternion.identity).GetComponent<IThowen>();
        StartCoroutine(thowerableObjectCopy.Move(transform.position, GetFallPoint(mousePosition)));
    }

    private Vector2 GetFallPoint(Vector3 mousePosition)
    {
        Vector2 targetPosition = mousePosition - transform.position;
        float angle = Mathf.Atan2(targetPosition.y, targetPosition.x);

        return new Vector3(Mathf.Cos(angle) * _maxThrowDistance, Mathf.Sin(angle) * _maxThrowDistance * _ellipseRatio) + _offset + transform.position;
    }
}
