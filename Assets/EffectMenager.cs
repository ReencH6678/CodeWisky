using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMenager : MonoBehaviour
{
    //[SerializeField] private CombiMenager _potionMenager;
    
    private List<Effect> _effects = new List<Effect>();

    private void AddEffect(Potion potion)
    {
      //  _potionMenager.FindeCombi(potion, _effects);
    }

    private void RealaseEffect()
    {
        _effects.RemoveAt(0);
    }
}
