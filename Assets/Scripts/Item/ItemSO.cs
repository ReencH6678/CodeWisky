using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    public string ItemName;
    public string Description;
    [Space(5)]
    public GameObject Prefab;
    public Sprite Sprite;
    public bool IsConsumable = false;
    public bool IsThroweable;
    // schlagen, werfen, ... (action)
    public virtual void Use(ActionContainer target, Vector2 direction) {}
    public virtual void Use(ActionContainer target) {}
    // Essen, Trinken
    public virtual void Consume(ActionContainer target) { }
}