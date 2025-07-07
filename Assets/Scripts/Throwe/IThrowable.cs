using UnityEngine;

public interface IThrowable
{
    void HandleObjectLanding();
    void StartMove(Vector2 fallPosition);
}
