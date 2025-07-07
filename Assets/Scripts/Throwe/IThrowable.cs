using UnityEngine;
using System.Collections;
public interface IThrowable
{
    void HandleObjectLanding();
    IEnumerator Move(Vector3 startPosition, Vector3 targetPosition);
    GameObject Copy();
}
