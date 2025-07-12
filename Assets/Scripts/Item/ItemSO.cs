using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSO : ScriptableObject
{
    [Header("General Info")]
    [SerializeField] private string _itemName;
    [SerializeField] private string _itemDescription;
    public InventoryCategory Category = InventoryCategory.Material;

    [Header("Visual / Prefab")]
    public GameObject Prefab;
    [SerializeField] private Sprite _sprite;

    [Header("Item Settings")]
    public bool IsConsumable = false;
    public bool IsStackable = true;

    // schlagen, werfen, ... (action)
    public virtual void Use(ActionContainer target, Vector2 direction) {}
    public virtual void Use(ActionContainer target) {}
    // Essen, Trinken
    public virtual void Consume(ActionContainer target) { }

    public string GetItemName()
    {
        return string.IsNullOrWhiteSpace(_itemName) ? ItemSettings.MissingName : _itemName;
    }

    public string GetItemDescription()
    {
        return string.IsNullOrWhiteSpace(_itemDescription) ? ItemSettings.MissingDescription : _itemDescription;
    }

    public Sprite GetSprite() 
    {
        return (_sprite == null) ? ItemSettings.MissingTexture : _sprite;
    }
}