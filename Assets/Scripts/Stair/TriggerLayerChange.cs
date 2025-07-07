using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TriggerLayerChange : MonoBehaviour
{
    [SerializeField] private Layers _layer;

    [SerializeField] private enum Layers
    {
        LVL1,
        LVL2,
        LVL3
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<LayerChanger>(out _))
            collision.gameObject.layer = LayerMask.NameToLayer(_layer.ToString());
    }
}
