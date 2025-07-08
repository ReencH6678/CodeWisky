using UnityEngine;

[CreateAssetMenu(fileName = "ItemSettings", menuName = "SO/Item/ItemSettings", order = 1)]
public class ItemSettingsSO : ScriptableObject
{
    //[Tooltip("Gets shown when an item has no Name")]
    public string MissingName;
    //[Tooltip("Gets shown when an item has no Description")]
    public string MissingDescription;
    [Tooltip("Gets shown when an item has no texture")]
    public Sprite MissingTexture;
}
