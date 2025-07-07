using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler), typeof(StairController))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _thoweble;

    private Mover _mover;
    private InputHandler _inputHandler;
    private Thower _thower;
    private StairController _stairController;
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputHandler = GetComponent<InputHandler>();
        _thower = GetComponent<Thower>();
        _stairController = GetComponent<StairController>();
    }

    private void Update()
    {
        if (_inputHandler.Direction != Vector2.zero)
            _mover.Move(_inputHandler.Direction, _stairController.GetStairMovement(_inputHandler.Direction));

        if (_inputHandler.IsLeftMouseButtonDown)
            _thower.ThoweObject(_thoweble.GetComponent<IItem>(), _inputHandler.GetMouseWorldPosition());

        if (_inputHandler.IsRightMouseButtonDown)
            _thoweble.GetComponent<IItem>().Use(this.gameObject);

    }
}
