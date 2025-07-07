using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Canvas _canvas;
    [Space(5)]
    [SerializeField] private UIPanelConfigSO[] _uiPanels;

    private Dictionary<string, UIPanelData> _panelsByName = new();
    private Dictionary<UIPanelConfigSO, UIPanelData> _panelsByConfig = new();

    private struct UIPanelData
    {
        public UIPanelConfigSO config;
        public GameObject gameObject;

        public bool isActive() {
            return gameObject.activeInHierarchy;
        }
    }

    private void Awake()
    {
        instance = this;

        ValidateConfiguration();
        InitializePanels();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePanel("Escape_menu");
        }
    }

    private void ValidateConfiguration()
    {
        if (_canvas == null)
        {
            Debug.LogError("UIManager: Canvas is not assigned!", this);
            return;
        }

        if (_uiPanels == null || _uiPanels.Length == 0)
        {
            Debug.LogError("UIManager: No UI panels configured!", this);
            return;
        }

        var nameSet = new HashSet<string>();
        foreach (var panel in _uiPanels)
        {
            if (panel == null)
            {
                Debug.LogError("UIManager: Null panel found in configuration!", this);
                continue;
            }

            string cleanName = CleanName(panel.panelName);
            if (!nameSet.Add(cleanName))
            {
                Debug.LogError($"UIManager: Duplicate panel name found: {panel.panelName}", this);
            }

            SetParentPanels(panel);
        }
    }

    private void InitializePanels()
    {
        if (_canvas == null) return;

        foreach (var panel in _uiPanels)
        {
            if (panel != null)
            {
                CreatePanel(_canvas.transform, panel, panel.defaultOpenPanelIndex == 0);
            }
        }
    }

    #region Public

    public bool OpenPanel(string panelName)
    {
        if (!TryGetPanelData(panelName, out var panelData))
        {
            Debug.LogWarning($"UIManager: Panel '{panelName}' not found!", this);
            return false;
        }

        CloseIncompatiblePanels(panelData.config);

        SetPanelVisibility(panelData, true);
        return true;
    }

    public bool TogglePanel(string panelName) {
        return (IsPanelOpen(panelName)) ? ClosePanel(panelName) : OpenPanel(panelName);
    }

    public bool ClosePanel(string panelName)
    {
        if (!TryGetPanelData(panelName, out var panelData))
        {
            Debug.LogWarning($"UIManager: Panel '{panelName}' not found!", this);
            return false;
        }

        SetPanelVisibility(panelData, false);
        return true;
    }

    public void CloseAllPanels()
    {
        foreach (var kvp in _panelsByName)
        {
            SetPanelVisibility(kvp.Value, false);
        }
    }

    public void CloseAllPanelsExcept(params string[] exceptions)
    {
        var exceptionSet = new HashSet<string>();
        foreach (var exception in exceptions)
        {
            exceptionSet.Add(CleanName(exception));
        }

        foreach (var kvp in _panelsByName)
        {
            if (!exceptionSet.Contains(kvp.Key))
            {
                SetPanelVisibility(kvp.Value, false);
            }
        }
    }

    public bool IsPanelOpen(string panelName)
    {
        return TryGetPanelData(panelName, out var panelData) && panelData.isActive();
    }

    public string[] GetOpenPanels()
    {
        var openPanels = new List<string>();
        foreach (var kvp in _panelsByName)
        {
            if (kvp.Value.isActive())
            {
                openPanels.Add(kvp.Value.config.panelName);
            }
        }
        return openPanels.ToArray();
    }

    #endregion

    #region Private

    private bool TryGetPanelData(string panelName, out UIPanelData panelData)
    {
        return _panelsByName.TryGetValue(CleanName(panelName), out panelData);
    }

    private void CloseIncompatiblePanels(UIPanelConfigSO targetPanel)
    {
        if (!targetPanel.HasCompatiblePanels())
        {
            // if there are no compatiblePanels
            foreach (var kvp in _panelsByConfig.ToArray())
            {
                if (kvp.Key != targetPanel && !kvp.Key.forcePanelVisible)
                {
                    SetPanelVisibility(kvp.Value, false);
                }
            }
        }
        else
        {
            // Only close not compatible panels
            var compatibleSet = new HashSet<UIPanelConfigSO>(targetPanel.compatiblePanels);
            compatibleSet.Add(targetPanel); // the panel is compatible with it self
           
            if (targetPanel.TryGetParentPanel(out UIPanelConfigSO parentPanel)) 
            {
                compatibleSet.Add(parentPanel);
            }

            foreach (var kvp in _panelsByConfig.ToArray())
            {
                if (!compatibleSet.Contains(kvp.Key) && !kvp.Key.forcePanelVisible)
                {
                    SetPanelVisibility(kvp.Value, false);
                }
            }
        }
    }

    private void SetPanelVisibility(UIPanelData panelData, bool visibility)
    {
        if (panelData.gameObject != null)
        {
            panelData.gameObject.SetActive(visibility);

            // Opens default panel
            int defaultOpen = panelData.config.defaultOpenPanelIndex - 1;
            if (defaultOpen >= 0)
            {
                string defaultChildPanelName = panelData.config.childPanels[defaultOpen].panelName;
                if (TryGetPanelData(defaultChildPanelName, out UIPanelData data))
                {
                    SetPanelVisibility(data, visibility);
                }
            }

            var updatedData = panelData;

            string cleanName = CleanName(panelData.config.panelName);
            _panelsByName[cleanName] = updatedData;
            _panelsByConfig[panelData.config] = updatedData;
        }
        else
        {
            Debug.LogError($"UIManager: GameObject for panel '{panelData.config.panelName}' is null!", this);
        }
    }

    private void CreatePanel(Transform parent, UIPanelConfigSO panel, bool isActive)
    {
        if (panel.panelObject == null)
        {
            Debug.LogError($"UIManager: Panel '{panel.panelName}' has no GameObject assigned!", this);
            return;
        }

        GameObject panelGO = Instantiate(panel.panelObject, parent);
        panelGO.name = panel.panelName;
        panelGO.SetActive(isActive);

        var panelData = new UIPanelData
        {
            config = panel,
            gameObject = panelGO
        };

        string cleanName = CleanName(panel.panelName);
        _panelsByName[cleanName] = panelData;
        _panelsByConfig[panel] = panelData;

        if (panel.HasChildPanels())
        {
            GameObject childGO = new GameObject("ChildContainer");

            childGO.transform.SetParent(panelGO.transform, false);

            RectTransform childRect = childGO.AddComponent<RectTransform>();

            childRect.anchorMin = Vector2.zero;
            childRect.anchorMax = Vector2.one;

            childRect.offsetMin = Vector2.zero;
            childRect.offsetMax = Vector2.zero;

            childRect.localScale = Vector3.one;
            childRect.localPosition = Vector3.zero;

            CreateChildPanels(childGO.transform, panel.childPanels, panel.defaultOpenPanelIndex);
        }
    }

    private void CreateChildPanels(Transform parent, UIPanelConfigSO[] childPanels, int defaultOpenIndex)
    {
        for (int i = 0; i < childPanels.Length; i++)
        {
            if (childPanels[i] != null)
            {
                CreatePanel(parent, childPanels[i], i == (defaultOpenIndex - 1));
            }
        }
    }

    private void SetParentPanels(UIPanelConfigSO panel)
    {
        if (!panel.HasChildPanels()) return;

        foreach (var cPanel in panel.childPanels)
        {
            if (cPanel == null)
            {
                Debug.LogWarning($"UIManager: Null child panel in {panel.name}", this);
                continue;
            }

            cPanel.SetParentPanel(panel);
            SetParentPanels(cPanel);
        }
    }

    private string CleanName(string name)
    {
        return string.IsNullOrEmpty(name) ? string.Empty : name.ToLower().Trim();
    }

    #endregion
}