using System.Collections.Generic;
using UnityEngine;

public class StairController : MonoBehaviour
{
    [SerializeField] private float StairSlowDownXPos = 0.8f;
    [SerializeField] private float StairSlowDownXNeg = 0.6f;
    [SerializeField] private float StairSlowDownYPos = 0.8f;
    [SerializeField] private float StairSlowDownYNeg = 0.6f;

    private Stack<Stair> _currentStairs = new Stack<Stair>();

    public void AddStair(Stair stair)
    {
        _currentStairs.Push(stair);
    }

    public void SubStair()
    {
        if(_currentStairs.Count > 0)
        _currentStairs.Pop();
    }

    public Vector2 GetStairMovement(Vector2 direction)
    {
        if (_currentStairs.Count == 0) return direction;

        Stair stairs = _currentStairs.Peek();
        Vector2 stairsDirection = stairs.GetDirection();

        direction.y *= (Mathf.Sign(stairsDirection.y) == Mathf.Sign(direction.y)) ? StairSlowDownYNeg : StairSlowDownYPos;
        float originalLength = direction.magnitude;

        float angle = stairs.Angle;
        bool isVertical = angle == 0;

        bool isRight = angle > 90;

        if (isRight)
            angle = angle - 90;
        else
            angle = 90 - angle;

        float tan = -Mathf.Tan(angle * Mathf.Deg2Rad);

        if (isRight)
            tan *= -1;

        if (isVertical)
            tan = 0;

        if (Mathf.Sign(stairsDirection.x) != Mathf.Sign(direction.x) && direction.y > 0)
            tan /= 2;

        direction.y += direction.x * tan;
        direction = direction.normalized * originalLength;

        return direction;
    }
}
