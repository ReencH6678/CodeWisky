using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler), typeof(StairController))]
[RequireComponent(typeof(ActionContainer))]
public class Player : MonoBehaviour
{
    [SerializeField] private ItemSO _selectedItem;

    private Mover _mover;
    private InputHandler _inputHandler;
    private Thrower _thower;
    private StairController _stairController;
    private ActionContainer _actionContainer;
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputHandler = GetComponent<InputHandler>();
        _thower = GetComponent<Thrower>();
        _stairController = GetComponent<StairController>();
        _actionContainer = GetComponent<ActionContainer>();
    }

    private void Update()
    {
        if (_inputHandler.Direction != Vector2.zero)
            _mover.Move(_inputHandler.Direction, _stairController.GetStairMovement(_inputHandler.Direction));

        if (_inputHandler.IsLeftMouseButtonDown)
        {
            _selectedItem.Use(_actionContainer, _inputHandler.GetMouseWorldPosition());
        }

        if (_inputHandler.IsRightMouseButtonDown)
            _selectedItem.Consume(_actionContainer);

    }
}
