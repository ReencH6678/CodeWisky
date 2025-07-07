using UnityEngine;

[CreateAssetMenu(fileName = "newDamageEffect", menuName = "SO/Effect/DamageEffect")]
public class DamageEffectSO : EffectSO
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
