using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private static Hotbar _instance;
    public static Hotbar Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("Hotbar: No instance of Hotbar found in the scene!");
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    [Header("Hotbar config")]
    [SerializeField] private Color _normalColor = new Color(255, 255, 255);
    [SerializeField] private Color _highlightColor = new Color(150, 150, 150);
    [Space(5)]
    [SerializeField] private Image _imageSlot1;
    [SerializeField] private Image _imageSlot2;
    [SerializeField] private Image _imageSlot3;
    [Space(5)]
    [SerializeField] private Image _imageItemSlot1;
    [SerializeField] private Image _imageItemSlot2;
    [SerializeField] private Image _imageItemSlot3;

    public event UnityAction<ItemSO> OnHotbarChange;

    public int _selectedItemSlot = 0;
    public int _selectedHotbarSlot = 0;
    private List<HotbarSlot> _hotbarSlots = new();

    class HotbarSlot 
    {
        public ItemSO[] itemSlots;

        public void SetItem(int i, ItemSO item) 
        {
            if(itemSlots == null) itemSlots = new ItemSO[3];
            itemSlots[i] = item;
        }

        public ItemSO GetItem(int i) 
        {
            if(itemSlots == null || !IsIndexInBounds(i, 0, 2)) return null;
            return itemSlots[i];
        }

        public bool TryGetItem(int i, out ItemSO item)
        {
            item = GetItem(i);
            return item != null;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _hotbarSlots.Add(new HotbarSlot());
        UpdateHotbarVisuals();
    }

    #region Public
    public bool SetItemSlot(int i) 
    {
        if (!IsIndexInBounds(i, 0, 2)) 
        {
            Debug.LogWarning("Hotbar: new itemSlot was out of bounds ("+ i +")", this);
            return false;
        }

        _selectedItemSlot = i;

        HotbarChanged();
        return true;
    }

    public void IncreaseHotbarSlot() 
    {
        _selectedHotbarSlot++;
        if (IsIndexInBounds(_selectedHotbarSlot, 0, _hotbarSlots.Count - 1)) 
        {
            _selectedHotbarSlot = 0;
        }

        HotbarChanged();
    }

    public void DecreaseHotbarSlot()
    {
        _selectedHotbarSlot--;
        if (IsIndexInBounds(_selectedHotbarSlot, 0, _hotbarSlots.Count - 1))
        {
            _selectedHotbarSlot = _hotbarSlots.Count - 1;
        }

        HotbarChanged();
    }

    public ItemSO GetSelectedItem() 
    {
        return _hotbarSlots[_selectedHotbarSlot].GetItem(_selectedItemSlot);
    }

    public void AddHotbarSlot() 
    {
        _hotbarSlots.Add(new HotbarSlot());
    }

    public bool AddItemToSelectedSlot(int itemSlot, ItemSO item)
    {
        return AddItemToSlot(_selectedHotbarSlot, itemSlot, item);
    }

    public bool AddItemToSlot(int slot, int itemSlot, ItemSO item) 
    {
        if (!IsIndexInBounds(slot, 0, _hotbarSlots.Count - 1)) 
        {
            Debug.LogWarning("Hotbar: Item " + item.GetItemName() +"could not be added, because slot ("+ slot  + ") was out of bounds", this);
            return false;
        }

        if (!IsIndexInBounds(itemSlot, 0, 2))
        {
            Debug.LogWarning("Hotbar: Item " + item.GetItemName() + "could not be added, because slot (" + slot + ") was out of bounds", this);
            return false;
        }

        _hotbarSlots[slot].SetItem(itemSlot, item);

        HotbarChanged();
        return true;
    }

    // includes the lower and upper bounds
    static public bool IsIndexInBounds(int index, int lower, int upper) 
    {
        return (index >= lower && index <= upper);
    }

    #endregion

    #region Private
    private HotbarSlot GetSelectedHotbarSlot()
    {
        return _hotbarSlots[_selectedHotbarSlot];
    }

    private void HotbarChanged() 
    {
        OnHotbarChange?.Invoke(GetSelectedItem());
        UpdateHotbarVisuals();
    }

    private void UpdateHotbarVisuals() 
    {
        HotbarSlot currentSlot = GetSelectedHotbarSlot();

        _imageSlot1.color = GetItemSlotColor(_selectedItemSlot == 0);
        _imageSlot2.color = GetItemSlotColor(_selectedItemSlot == 1);
        _imageSlot3.color = GetItemSlotColor(_selectedItemSlot == 2);


        SetImageSpriteSlot(ref _imageItemSlot1, currentSlot.GetItem(0));
        SetImageSpriteSlot(ref _imageItemSlot2, currentSlot.GetItem(1));
        SetImageSpriteSlot(ref _imageItemSlot3, currentSlot.GetItem(2));
    }

    private Color _imageEmptyColor = new Color(0, 0, 0, 0);
    private Color _imageColor = new Color(255, 255, 255);
    private void SetImageSpriteSlot(ref Image image, ItemSO item)
    {
        Sprite s = GetItemSlotSprite(item);

        if (s == null) 
        {
            image.sprite = null;
            image.color = _imageEmptyColor;
            return;
        }

        image.sprite = s;
        image.color = _imageColor;
    }

    private Sprite GetItemSlotSprite(ItemSO item) 
    {
       return (item == null) ? null : item.GetSprite();
    }

    private Color GetItemSlotColor(bool b) 
    { 
        return (b) ? _highlightColor : _normalColor;
    }

    #endregion

}