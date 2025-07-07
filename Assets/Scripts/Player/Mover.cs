using UnityEngine;

[RequireComponent(typeof(SpeedMultiplier), typeof(DirectionMultiplier))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpeedMultiplier _speedMultiplier;
    private DirectionMultiplier _directionMultiplier;

    private void Awake()
    {
        _speedMultiplier = GetComponent<SpeedMultiplier>();
        _directionMultiplier = GetComponent<DirectionMultiplier>();
    }

    public void Move(Vector2 direction, Vector2 stairDirection)
    {
        direction += _directionMultiplier.DirectionRario;
        ApplyStairMovement(ref direction, stairDirection);
        transform.Translate((direction * _speed * Time.deltaTime) * _speedMultiplier.SpeedRatio);
    }

    private void ApplyStairMovement(ref Vector2 direction, Vector2 stairDirection)
    {
        direction += stairDirection;
    }
}
