using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputHandler))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _thoweble;

    private Mover _mover;
    private InputHandler _inputHandler;
    private Thower _thower;
    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _inputHandler = GetComponent<InputHandler>();
        _thower = GetComponent<Thower>();
    }

    private void Update()
    {
        if (_inputHandler.Direction != Vector2.zero)
            _mover.Move(_inputHandler.Direction);

        if (_inputHandler.IsAttack)
            _thower.ThoweObject(_thoweble.GetComponent<IThowen>(), _inputHandler.GetMouseWorldPosition());

    }
}
