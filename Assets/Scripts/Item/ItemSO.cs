using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    public string itemName;
    public string description;
    [Space(5)]
    public GameObject prefab;
    public Sprite sprite;
    public bool isConsumable = false;

    // schlagen, werfen, ... (action)
    public virtual void Use(GameObject target) { }
    // Essen, Trinken
    public virtual void Consume(GameObject target) { }
}