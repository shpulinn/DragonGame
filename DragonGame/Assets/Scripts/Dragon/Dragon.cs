using UnityEngine;

public class Dragon : MonoBehaviour
{
    //private DragonMovement _movement;
    private DragonMovementRB _movementRB;
    private DragonTail _tail;
    private DragonCollision _collision;
    private DragonScaleBuffs _scaleBuffs;
    private bool _isGameOver = false;

    private void Start()
    {
        //_movement = GetComponent<DragonMovement>();
        _movementRB = GetComponent<DragonMovementRB>();
        _tail = GetComponent<DragonTail>();
        _collision = GetComponent<DragonCollision>();
        _scaleBuffs = GetComponent<DragonScaleBuffs>();

        GameManager.Instance.OnGameOverEvent += OnGameOver;
    }

    private void Update()
    {
        if (_isGameOver) return;

        bool isControlled = InputManager.Instance.Joystick;
        Vector3 moveVector = isControlled ? InputManager.Instance.MoveVector : Vector3.zero;
        _movementRB.Move(moveVector, isControlled);
    }

    private void OnGameOver()
    {
        _isGameOver = true;
    }
}