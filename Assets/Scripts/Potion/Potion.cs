using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(ThrowerObjectMover))]
public abstract class Potion : MonoBehaviour, IThrowable
{
    [SerializeField] private ItemPotionSO _currentPotion;// curryPotion

    [SerializeField] private float _effectRadius;
    protected List<Effect> _effects = new List<Effect>();

    private ThrowerObjectMover _throwerObjectMover;

    private void Awake()
    {
        _throwerObjectMover = GetComponent<ThrowerObjectMover>();
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
        StartCoroutine(_throwerObjectMover.Move(transform.position, fallPosition));
    }

    public void Use(GameObject target)
    {
        ApplyEffects(target.gameObject);
    }

    private bool CanUseOn(GameObject target)
    {
        bool canUse = false;

        foreach (Effect effect in _effects)
            canUse = effect.CanApply(target);

        return canUse;
    }

    private void ApplyEffects(GameObject target)
    {
        if (target.TryGetComponent<IEffectable>(out IEffectable effectable))
        {
            foreach (Effect effect in _effects)
            {
                effectable.ReceiveEffect(effect);
            }
        }
    }

}
