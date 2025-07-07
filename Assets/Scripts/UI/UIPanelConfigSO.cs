using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "new UIPanelConfig", menuName = "SO/UI/UIPanelConfig")]
public class UIPanelConfigSO : ScriptableObject
{
    [Header("Panel Configuration")]
    public string panelName;
    public GameObject panelObject;
    [Tooltip("If enabled, this panel will stay visible and cannot be closed automatically by other panels.")]
    public bool forcePanelVisible = false;

    [Header("Hierarchy")]
    [Tooltip("This panel will open by default when the parent panel becomes visible. If this is the this panel, it will be visible at scene start.")]
    [SerializeField] private UIPanelConfigSO _defaultOpenPanel;
    [HideInInspector] public int defaultOpenPanelIndex;
    public UIPanelConfigSO[] childPanels;

    [Header("Compatibility")]
    [Tooltip("Panels that can be open simultaneously with this panel. If empty, this panel closes all others.")]
    public List<UIPanelConfigSO> compatiblePanels = new List<UIPanelConfigSO>();
    private UIPanelConfigSO _parentPanel;

    public void SetParentPanel(UIPanelConfigSO panel) 
    {
        _parentPanel = panel;
    }

    public bool TryGetParentPanel(out UIPanelConfigSO config) 
    {
        config = _parentPanel;
        return _parentPanel != null; 
    }

    public bool HasParentPanel()
    {
        return _parentPanel != null;
    }

    public bool HasChildPanels()
    {
        return childPanels != null && childPanels.Length > 0;
    }

    public bool HasCompatiblePanels()
    {
        return compatiblePanels != null && compatiblePanels.Count > 0;
    }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(panelName))
        {
            Debug.LogWarning($"UIPanelConfig: Panel name is empty in {name}!", this);
        }

        if (panelObject == null)
        {
            Debug.LogWarning($"UIPanelConfig: Panel object is not assigned in {name}!", this);
        }

        // sets the index of the default open panel
        if (_defaultOpenPanel != null)
        {
            // if the same panel is the default open panel
            if (_defaultOpenPanel == this)
            {
                defaultOpenPanelIndex = 0;
            }
            else {
                // searches the default panel in the child elements
                for (int i = 0; i < childPanels.Length; i++)
                {
                    if (_defaultOpenPanel == childPanels[i])
                    {
                        defaultOpenPanelIndex = i + 1;
                        break;
                    }

                    if (i == childPanels.Length - 1)
                    {
                        defaultOpenPanelIndex = -1;
                        Debug.LogWarning($"UIPanelConfig: DefaultOpenPanel is not in childPanels", this);
                    }
                }
            }

        } else 
        {
            defaultOpenPanelIndex = -1;
        }
    }
}