using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    private const string _LAYERS_NAME = "LVL";
    private int _currentLayerIndex = 1;
    public void ChangeUp()
    {
        gameObject.layer = LayerMask.NameToLayer(_LAYERS_NAME + ++_currentLayerIndex);
        Debug.Log(_currentLayerIndex);
    }

    public void ChanngeDown()
    {
        gameObject.layer = LayerMask.NameToLayer(_LAYERS_NAME + --_currentLayerIndex);
        Debug.Log(_currentLayerIndex);
    }
}
