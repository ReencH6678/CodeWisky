using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler), typeof(StairController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _throwable;

    private Mover _mover;
    private InputHandler _inputHandler;
    private Thrower _thower;
    private StairController _stairController;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputHandler = GetComponent<InputHandler>();
        _thower = GetComponent<Thrower>();
        _stairController = GetComponent<StairController>();
    }

    private void Update()
    {
        if (_inputHandler.Direction != Vector2.zero)
            _mover.Move(_inputHandler.Direction, _stairController.GetStairMovement(_inputHandler.Direction));

        if (_inputHandler.IsLeftMouseButtonDown)
            _thower.ThoweObject(_throwable.GetComponent<IItem>(), _inputHandler.GetMouseWorldPosition());

        if (_inputHandler.IsRightMouseButtonDown)
            _throwable.GetComponent<IItem>().Use(this.gameObject);

    }
}
