using UnityEngine;

public class IEffect : MonoBehaviour
{
    public virtual void Apply(GameObject target) { }
    public virtual bool CanApply(GameObject target){ return false; }
}
