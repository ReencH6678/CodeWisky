using UnityEngine;

public class DamageEffect : IEffect
{
    [SerializeField]private float _damage;

    public override void Apply(GameObject target)
    {
        HealthMenager health = target.GetComponent<HealthMenager>();

        health.SetDamage(_damage);
    }

    public override bool CanApply(GameObject target)
    {
        return target.TryGetComponent<HealthMenager>(out _);
    }
}
