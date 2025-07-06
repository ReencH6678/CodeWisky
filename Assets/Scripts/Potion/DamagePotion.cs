using UnityEngine;

[RequireComponent(typeof(IEffect))]
public class DamagePotion : Potion
{
    [SerializeField] private IEffect _effect;

    private void Awake()
    {
        _effects.Add(gameObject.GetComponent<IEffect>());
    }
}
