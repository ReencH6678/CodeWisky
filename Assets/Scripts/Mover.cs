using UnityEngine;

[RequireComponent(typeof(SpeedMultiplier))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;

    private SpeedMultiplier _speedMultiplier;

    private void Awake()
    {
        _speedMultiplier = GetComponent<SpeedMultiplier>();
    }

    public void Move(Vector2 direction)
    {
        transform.Translate(direction * _speed * _speedMultiplier.SpeedRatio * Time.deltaTime);
        Debug.Log(_speedMultiplier.SpeedRatio * _speed * direction);
        Debug.Log("Multiply" + direction * _speed * _speedMultiplier.SpeedRatio);
    }
}
