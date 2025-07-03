using UnityEngine;
using System.Collections;
public interface IThowen
{
    void FallDawn();
    void Fly();
    IEnumerator Move(Vector3 startPosition, Vector3 targetPosition);
    GameObject Copy();

    
}
