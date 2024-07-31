using UnityEngine;

public class DragonCollision : MonoBehaviour
{
    public delegate void OnObjectDestroyed();
    public OnObjectDestroyed OnObjectDestroyedEvent;

    private bool _isCrushBuffed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollidable collidable))
        {
            collidable.OnPlayerCollision();
        }

        if (_isCrushBuffed)
        {
            if (other.TryGetComponent(out IDestroyable destroyable))
            {
                destroyable.OnPlayerCollide();
                OnObjectDestroyedEvent?.Invoke();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = transform.position;
        }
    }

    public void SetCrushBuff(bool value)
    {
        _isCrushBuffed = value;
    }
}