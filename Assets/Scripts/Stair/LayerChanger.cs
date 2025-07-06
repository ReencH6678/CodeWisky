using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChanger : MonoBehaviour
{
    public void ChangeLayer(LayerMask layer)
    {
        gameObject.layer = layer;
    }
}
