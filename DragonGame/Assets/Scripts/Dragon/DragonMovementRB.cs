using UnityEngine;

public class DragonMovementRB : MonoBehaviour
{
    [SerializeField] private float normalMoveSpeed = 8f;   // Скорость обычного движения
    [SerializeField] private float slowMoveSpeed = 4f;     // Скорость медленного движения
    [SerializeField] private float headRotationSpeed = 5f; // Скорость поворота
    private Rigidbody _rigidbody;
    private bool _hadFirstTouch = false;

    private bool _canMove = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        // Убедимся, что физика контролирует объект, отключив управление позицией напрямую
        _rigidbody.freezeRotation = true;

        GameManager.Instance.OnGameOverEvent += StopMoveAfterDelay;
        
        Invoke(nameof(StopMoveAfterDelay), .1f);
    }

    public void Move(Vector3 moveVector, bool isControlled)
    {
        if (_canMove == false)
        {
            _rigidbody.velocity = Vector3.zero;
            return;
        }
        if (isControlled && _hadFirstTouch == false)
        {
            _hadFirstTouch = true;
        }

        if (_hadFirstTouch == false)
        {
            return;
        }
        if (isControlled && moveVector != Vector3.zero)
        {
            // Рассчитываем вектор движения
            Vector3 movement = moveVector * normalMoveSpeed;

            // Применяем движение через velocity
            _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
            
            // Плавный поворот в сторону управления
            Quaternion targetRotation = Quaternion.LookRotation(moveVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, headRotationSpeed * Time.deltaTime);
        }
        else
        {
            // Если игрок не управляет, дракон продолжает движение вперёд с медленной скоростью
            Vector3 movement = transform.forward * slowMoveSpeed;

            // Применяем движение через velocity
            _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);
        }
    }

    private void StopMoveAfterDelay()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public void DisableMovement()
    {
        _canMove = false;
    }

    public void EnableMovement()
    {
        _canMove = true;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnGameOverEvent -= StopMoveAfterDelay;
    }
}
