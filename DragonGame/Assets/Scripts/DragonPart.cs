using UnityEngine;

public class DragonPart : MonoBehaviour
{
    [SerializeField] private Transform target;

    void Update()
    {
        if (GameManager.Instance.IsPaused)
        {
            return;
        }

        if (target != null)
            transform.LookAt(target);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollidable collidable))
        {
            collidable.OnPlayerCollision();
        }
    }
}
