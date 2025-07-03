using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private const string Horizontal = nameof(Horizontal);
    private const string Vertical = nameof(Vertical);
    private const string Fire1 = nameof(Fire1);

    public Vector2 Direction { get => _direction; private set => _direction = value; }
    private Vector2 _direction = new Vector2();

    public bool IsAttack { get; private set; }

    private void Update()
    {
        _direction.x = Input.GetAxis(Horizontal);
        _direction.y = Input.GetAxis(Vertical);

        IsAttack = Input.GetMouseButtonDown(0);
    }

    public Vector2 GetMouseDirection()
    {
        return Input.mousePosition - transform.position.normalized;
    }

    public Vector3 GetMouseWorldPosition()
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = _camera.WorldToScreenPoint(transform.position).z;
        return _camera.ScreenToWorldPoint(mouseScreenPos);
    }
}
