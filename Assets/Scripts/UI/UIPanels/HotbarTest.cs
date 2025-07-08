using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarTest : MonoBehaviour
{
    [SerializeField] private ItemSO item;

    private Hotbar hotbar;

    private void Start()
    {
        hotbar = Hotbar.Instance;
        hotbar.OnHotbarChange += ItemChanged;

    }

    private void OnEnable()
    {
        if (hotbar != null) 
            hotbar.OnHotbarChange += ItemChanged;
    }

    private void OnDisable()
    {
        hotbar.OnHotbarChange -= ItemChanged;
    }

    private void ItemChanged(ItemSO item) 
    {
        if (item == null) 
        {
            Debug.Log("No Item");
            return;
        }

        Debug.Log("CurrentItem: "+ item.GetItemName());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            hotbar.SetItemSlot(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            hotbar.SetItemSlot(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            hotbar.SetItemSlot(2);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            hotbar.AddItemToSelectedSlot(hotbar._selectedItemSlot, item);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            hotbar.AddItemToSelectedSlot(hotbar._selectedItemSlot, null);
        }
    }
}
