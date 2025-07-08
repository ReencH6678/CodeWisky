using UnityEngine;

public static class ItemSettings
{
    private static ItemSettingsSO _settings;
    private const string _ITEM_SETTINGS = "ItemSettings";

    private static ItemSettingsSO Settings
    {
        get
        {
            if (_settings == null)
            {
                _settings = Resources.Load<ItemSettingsSO>(_ITEM_SETTINGS);
                if (_settings == null)
                {
                    Debug.LogError("ItemSettingsSO not found. Place it under Resources with the name '"+ _ITEM_SETTINGS + "'.");
                }
            }
            return _settings;
        }
    }

    public static Sprite MissingTexture => Settings.MissingTexture;

    public static string MissingName => Settings.MissingName;

    public static string MissingDescription => Settings.MissingDescription;
}
