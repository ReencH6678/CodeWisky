using UnityEngine;
using System.Collections;

public class Potion : MonoBehaviour, IThowen
{
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _flyDuration;

    [SerializeField] private GameObject _shadow;

    private float _startTime;

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

        gameObject.SetActive(false);

    }

    public GameObject Copy()
    {
        return this.gameObject;
    }

    public void FallDawn()
    {
        throw new System.NotImplementedException();
    }

    public void Fly()
    {
        throw new System.NotImplementedException();
    }

    public void GiveEffect()
    {
        throw new System.NotImplementedException();
    }

    private Vector2 GetNextPosition(Vector3 startPosition, Vector3 targetPosition, float progress)
    {
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        float height = _maxHeight * (progress * 4 * (1 - progress));

        return new Vector2(linearPosition.x, linearPosition.y + height);
    }

}
