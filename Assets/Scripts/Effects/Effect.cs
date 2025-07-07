using UnityEngine;

public class Effect : MonoBehaviour
{
    public virtual void Apply(GameObject target) { }
    public virtual bool CanApply(GameObject target){ return false; }
}
