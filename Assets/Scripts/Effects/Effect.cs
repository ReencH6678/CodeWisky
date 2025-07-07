using UnityEngine;

public class Effect : MonoBehaviour
{
    public string EffectName { get; private set; }

    public virtual void Apply(GameObject target) { }
    public virtual bool CanApply(GameObject target){ return false; }
}
