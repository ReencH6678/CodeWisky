using UnityEngine;
using System.Collections;
public interface IThowen
{
    void SetEffects();
    IEnumerator Move(Vector3 startPosition, Vector3 targetPosition);
    GameObject Copy();
}
