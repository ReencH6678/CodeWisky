using UnityEngine;

[RequireComponent(typeof(Effect))]
public class DamagePotion : Potion
{
    [SerializeField] private Effect _effect;

    private void Awake()
    {
        _effects.Add(gameObject.GetComponent<Effect>());
    }
}
