using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ThrowerObjectMover : MonoBehaviour
{
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _flyDuration;
    private float _startTime;

    [SerializeField] private GameObject _shadow;

    public event UnityAction Landed;

    private void Awake()
    {
        _startTime = Time.time;
    }

    public IEnumerator Move(Vector3 startPosition, Vector3 targetPosition)
    {
        _startTime = Time.time;
        float progress;
        float elapsed;

        while (Time.time - _startTime < _flyDuration)
        {
            elapsed = Time.time - _startTime;
            progress = elapsed / _flyDuration;

            Vector2 nextPosition = GetNextPosition(startPosition, targetPosition, progress);
            Vector2 shadowPosition = Vector2.Lerp(startPosition, targetPosition, progress);

            _shadow.transform.position = shadowPosition;
            transform.position = nextPosition;

            yield return null;
        }

        Landed?.Invoke();
    }

    private Vector2 GetNextPosition(Vector3 startPosition, Vector3 targetPosition, float progress)
    {
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        float height = _maxHeight * (progress * 4 * (1 - progress));

        return new Vector2(linearPosition.x, linearPosition.y + height);
    }
}
