using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effector : MonoBehaviour, IEffectable
{
    public void ReceiveEffect(IEffect effect)
    {
        if (effect.CanApply(gameObject))
            effect.Apply(gameObject);
    }
}
