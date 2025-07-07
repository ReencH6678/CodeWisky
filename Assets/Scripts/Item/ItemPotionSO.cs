using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Potion", menuName ="Item/Potion")]
public class ItemPotionSO : ItemSO
{
   [SerializeField] private List<Effect> _effect;

    public override void Use(GameObject target) 
    {
        foreach (var effect in _effect)
        {
            effect.Apply(target);
        }
    }
}