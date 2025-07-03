using System.Collections;
using UnityEngine;

public class PotionMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _moveDistance;

    public IEnumerator Move(Vector2 direction, float flyDistance)
    {
        var waitForSecond = new WaitForSeconds(_speed);

        Vector2 nextPosition = Vector2.zero;
        float endXPosition = transform.position.x + flyDistance;

        float porabelX = transform.localPosition.x;
        float porabelY = transform.localPosition.y;

        while (transform.position.x != flyDistance)
        {
            nextPosition.y = Mathf.Pow(transform.position.x, 2);
            nextPosition.x += _moveDistance;
        }

        yield return waitForSecond;
    }
}
