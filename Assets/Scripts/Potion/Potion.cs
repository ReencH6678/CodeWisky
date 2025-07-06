using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Potion : MonoBehaviour, IThowen, IItem
{
    [SerializeField] private float _maxHeight;
    [SerializeField] private float _flyDuration;
    [SerializeField] private float _effectRadius;
    [SerializeField] private GameObject _shadow;

    protected List<IEffect> _effects = new List<IEffect>();
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

        SetEffects();
    }

    public GameObject Copy()
    {
        return this.gameObject;
    }

    public void SetEffects()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _effectRadius);

        foreach (Collider2D target in targets)
        {
            if (CanUseOn(target.gameObject))
                ApplyEffects(target.gameObject);
        }

        Destroy(gameObject);
    }

    private Vector2 GetNextPosition(Vector3 startPosition, Vector3 targetPosition, float progress)
    {
        Vector3 linearPosition = Vector3.Lerp(startPosition, targetPosition, progress);
        float height = _maxHeight * (progress * 4 * (1 - progress));

        return new Vector2(linearPosition.x, linearPosition.y + height);
    }

    public void Use(GameObject target)
    {
        ApplyEffects(target.gameObject);
    }

    private bool CanUseOn(GameObject target)
    {
        bool canUse = false;

        foreach (IEffect effect in _effects)
            canUse = effect.CanApply(target);

        return canUse;
    }

    private void ApplyEffects(GameObject target)
    {
        if (target.TryGetComponent<IEffectable>(out IEffectable effectable))
        {
            foreach (IEffect effect in _effects)
            {
                effectable.ReceiveEffect(effect);
            }
        }
    }
}
