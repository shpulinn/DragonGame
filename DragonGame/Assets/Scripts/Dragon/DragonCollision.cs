using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragonCollision : MonoBehaviour
{
    [SerializeField] private List<DragonBuffs> dragonBuffs = new List<DragonBuffs>();
    
    public delegate void OnObjectDestroyed();
    public OnObjectDestroyed OnObjectDestroyedEvent;

    private bool _isCrushBuffed = false;

    private bool _collisionDisabled = false;
    private DragonBuffs _currentBuff;

    private void OnEnable()
    {
        GlobalEventManager.OnProgressReached.AddListener(DisableCollision);
    }

    private void DisableCollision(int value)
    {
        if (value > 1 && BuffManager.Instance.IsAnyBuffActive == false)
        {
            _collisionDisabled = true;
            var randomBuff = Random.Range(0, dragonBuffs.Count);
            _currentBuff = dragonBuffs[randomBuff];
            Invoke(nameof(EnableCollision), _currentBuff.buffDuration);
            BuffManager.Instance.ActivateBuff(_currentBuff);
        }
    }

    private void EnableCollision()
    {
        _collisionDisabled = false;
        _currentBuff = null;
    }

    private void OnTriggerEnter(Collider other)
    {
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