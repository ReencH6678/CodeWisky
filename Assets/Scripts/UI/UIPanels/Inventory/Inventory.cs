using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InventoryCategory
{
    Material,
    Potion,
    Spezial
}

public class Inventory : MonoBehaviour
{
    [System.Serializable]
    public class InventoryElement
    {
        public int indexSlot;
        public int itemCount;
        public InventoryCategory category;
        public ItemSO item;
    }

    [SerializeField] private int _numberOfSlots = 24;
    [Space(5)]
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private List<InventorySlot> _slots = new();        // Objects that are in the UI
    [SerializeField] private List<InventoryElement> _elements = new();  // Data for the slots inthe UI

    // so kann man eine liste von element bekommen die eine bestimmten typen
    // var materials = _inventoryElements.Where(e => e.category == InventoryCategory.Material).ToList();

    #region Public

    public bool AddItem(ItemSO newItem)
    {
        int index = GetNextFreeIndex(newItem);
        if (index < 0)
        {
            Debug.LogWarning("Inventory: No available slot to add item.", this);
            return false;
        }

        return AddItem(newItem, index);
    }

    public bool AddItem(ItemSO newItem, int index)
    {
        if (TryGetElementBySlot(index, out InventoryElement inventoryElement)) 
        {
            // stack wenn selbe item
            if (newItem == inventoryElement.item && newItem.IsStackable)
            {
                inventoryElement.itemCount++;
            }
            else {
                Debug.LogWarning($"Inventory: Slot {index} is occupied by a different item.", this);
                return false;
            }
        }
        else
        {
            AddItemElement(newItem, index);
        }

        return true;
    }

    #endregion

    #region Private

    private void AddItemElement(ItemSO newItem, int index) 
    {
        InventoryElement e = new InventoryElement
        {
            indexSlot = index,
            itemCount = 1,
            category = newItem.Category,
            item = newItem
        };

        _elements.Add(e);
    }

    // Wäre schneller eine Mapping zu machen wo man mit dem index direkt den InventoryElement bekommt.
    // ist aber schwerer drauf aufzupassen das man diese liste dann auch immer mit updatet
    // private Dictionary<int, InventoryElement> _slotIndexMap = new();
    private bool TryGetElementBySlot(int index, out InventoryElement inventoryElement) 
    {
        inventoryElement = null;
        
        foreach (InventoryElement itemElement in _elements)
        {
            if (itemElement.indexSlot == index) 
            {
                inventoryElement = itemElement;
                break;
            }
        }

        return (inventoryElement != null);
    }

    private int GetNextFreeIndex(ItemSO newItem)
    {
        int existingIndex = -1;
        HashSet<int> usedIndices = new();

        foreach (var e in _elements)
        {
            if (e.item == newItem && existingIndex == -1)
            {
                existingIndex = e.indexSlot;
            }
            else
            {
                usedIndices.Add(e.indexSlot);
            }
        }

        // Falls das Item bereits vorhanden ist (Stackbar etc.)
        if (existingIndex != -1)
            return existingIndex;

        // Nächsten freien Slot finden
        for (int i = 0; i < _numberOfSlots; i++)
        {
            if (!usedIndices.Contains(i))
                return i;
        }

        Debug.LogWarning("Inventory: Could not get next free index", this);
        return -1;
    }

    #endregion
}