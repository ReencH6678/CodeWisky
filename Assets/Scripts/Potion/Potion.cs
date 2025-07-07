using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(ThrowerObjectMover))]
public class Potion : MonoBehaviour, IThrowable
{
    private float _effectRadius;
    protected List<EffectSO> _effects = new List<EffectSO>();

    private ThrowerObjectMover _throwerObjectMover;

    private void OnEnable()
    {
        _throwerObjectMover = GetComponent<ThrowerObjectMover>();
        _throwerObjectMover.Landed += HandleObjectLanding;
    }

    private void OnDisable()
    {
        _throwerObjectMover.Landed -= HandleObjectLanding;
    }

    public Potion SetEffects(List<EffectSO> effects)
    {
        foreach (EffectSO effect in effects)
            _effects.Add(effect);

        return this;
    }

    public Potion SetRadius(float radius)
    {
        _effectRadius = radius;

        return this;
    }

    public void HandleObjectLanding()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _effectRadius);

        foreach (Collider2D target in targets)
        {
            if (CanUseOn(target.gameObject))
                ApplyEffects(target.gameObject);
        }

        Destroy(gameObject);
    }

    public void StartMove(Vector2 fallPosition)
    {
        StartCoroutine(this._throwerObjectMover.Move(transform.position, fallPosition));
    }

    public void Use(GameObject target)
    {
        ApplyEffects(target.gameObject);
    }

    private bool CanUseOn(GameObject target)
    {
        bool canUse = false;

        foreach (EffectSO effect in _effects)
            canUse = effect.CanApply(target);

        return canUse;
    }

    private void ApplyEffects(GameObject target)
    {
        if (target.TryGetComponent<IEffectable>(out IEffectable effectable))
        {
            foreach (EffectSO effect in _effects)
            {
                effectable.ReceiveEffect(effect);
            }
        }
    }

}
