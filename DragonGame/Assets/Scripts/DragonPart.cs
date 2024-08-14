using ScriptableObjects;
using UnityEngine;

public class DragonPart : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
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

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.TryGetComponent(out ICollidable collidable))
    //     {
    //         collidable.OnPlayerCollision();
    //     }
    // }
    
    private void OnTriggerEnter(Collider other)
    {
        DragonBuffs _currentBuff = BuffManager.Instance.GetActiveBuff();
        if (other.TryGetComponent(out ICollidable collidable))
        {
            if (other.TryGetComponent(out Human human))
            {
                if (_currentBuff && _currentBuff.enableMoveThroughPeople)
                {
                    return;
                }
            } else if (other.TryGetComponent(out Trap trap))
            {
                if (_currentBuff && _currentBuff.enableMoveThroughTraps)
                {
                    return;
                }
            }
            
            collidable.OnPlayerCollision();
        }
    }
}
