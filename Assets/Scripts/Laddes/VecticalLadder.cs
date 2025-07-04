using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VecticalLadder : MonoBehaviour
{
    [SerializeField] private float _speedMultiply;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<SpeedMultiplier>(out SpeedMultiplier speedMultiplier))
        {
            speedMultiplier.AddSpeed(_speedMultiply);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<SpeedMultiplier>(out SpeedMultiplier speedMultiplier))
        {
            speedMultiplier.SubSpeed(_speedMultiply);
        }
    }
}
