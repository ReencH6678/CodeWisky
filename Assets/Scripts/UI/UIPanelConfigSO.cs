using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new UIPanelConfig", menuName = "SO/UI/UIPanelConfig")]
public class UIPanelConfigSO : ScriptableObject
{
    [Header("Panel Configuration")]
    public string panelName;
    public GameObject panelObject;

    [Header("Hierarchy")]
    public UIPanelConfigSO[] childPanels;

    [Header("Compatibility")]
    [Tooltip("Panels that can be open simultaneously with this panel. If empty, this panel closes all others.")]
    public List<UIPanelConfigSO> compatiblePanels = new List<UIPanelConfigSO>();
    private UIPanelConfigSO _parentPanel;

    public void SetParentPanel(UIPanelConfigSO panel) 
    {
        _parentPanel = panel;
    }

    public UIPanelConfigSO GetParentPanel() 
    {
        return _parentPanel;
    }

    public bool HasChildPanels()
    {
        return childPanels != null && childPanels.Length > 0;
    }

    public bool HasCompatiblePanels()
    {
        return compatiblePanels != null && compatiblePanels.Count > 0;
    }

    // Validation in der ScriptableObject
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
    }
}