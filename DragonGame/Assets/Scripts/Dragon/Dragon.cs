using UnityEngine;

public class Dragon : MonoBehaviour
{
    private DragonMovement _movement;
    private DragonTail _tail;
    private DragonCollision _collision;
    private DragonBuffs _buffs;
    private bool _isGameOver = false;

    private void Start()
    {
        _movement = GetComponent<DragonMovement>();
        _tail = GetComponent<DragonTail>();
        _collision = GetComponent<DragonCollision>();
        _buffs = GetComponent<DragonBuffs>();

        GameManager.Instance.OnGameOverEvent += OnGameOver;
    }

    private void Update()
    {
        if (_isGameOver) return;

        bool isControlled = InputManager.Instance.Joystick;
        Vector3 moveVector = isControlled ? InputManager.Instance.MoveVector : Vector3.zero;
        _movement.Move(moveVector, isControlled);
    }

    private void OnGameOver()
    {
        _isGameOver = true;
    }
}