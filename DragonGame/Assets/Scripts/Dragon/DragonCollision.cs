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
        if (BuffManager.Instance.IsAnyBuffActive)
            return;
        if (value <= 1) 
            return;
        var randomBuff = Random.Range(0, dragonBuffs.Count);
        _currentBuff = dragonBuffs[randomBuff];
        _collisionDisabled = true;
        Invoke(nameof(EnableCollision), _currentBuff.buffDuration);
        BuffManager.Instance.ActivateBuff(_currentBuff);
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
            var currentBuff = BuffManager.Instance.GetActiveBuff();
            if (other.TryGetComponent(out Human human))
            {
                if (currentBuff && currentBuff.enableMoveThroughPeople)
                {
                    return;
                }
            } else if (other.TryGetComponent(out Trap trap))
            {
                if (currentBuff && currentBuff.enableMoveThroughTraps)
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