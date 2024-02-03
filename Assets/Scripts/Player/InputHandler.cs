using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private StateMachine stateMachine;

    public static InputHandler Instance { get; private set; }
    
    private PlayerControls _playerControls;
    private Vector2 _movementValue;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    private void Update()
    {
        _movementValue = _playerControls.Movement.Move.ReadValue<Vector2>();
    }
    

    public Vector2 GetMovementDirectionValue()
    {
        return _movementValue;
    }

    public bool IsAttacking()
    {
        return _playerControls.Combat.Attack.triggered;
    }

    public bool IsDodging()
    {
        return _playerControls.Combat.Dodge.triggered;
    }
}
