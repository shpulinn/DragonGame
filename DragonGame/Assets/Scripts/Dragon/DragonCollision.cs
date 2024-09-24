using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

public class DragonCollision : MonoBehaviour
{
    [SerializeField] private List<DragonBuffs> dragonBuffs = new List<DragonBuffs>();

    [Space] [SerializeField] private DragonStats DragonStats;

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
        var currentBuff = ScriptableObject.CreateInstance<DragonBuffs>();
        if (BuffManager.Instance.IsAnyBuffActive)
        {
            currentBuff = BuffManager.Instance.GetActiveBuff();
        }
        if (other.TryGetComponent(out ICollidable collidable))
        {
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
        
        if (other.TryGetComponent(out IDestroyable IDestroyable))
        {
            if (other.TryGetComponent(out Destroyable destroyable))
            {
                if (currentBuff.enableBreakingObstacles)
                {
                    IDestroyable.OnPlayerCollide();
                    OnObjectDestroyedEvent?.Invoke();
                } else if (destroyable.DestructionLevelNeeded > 0 && DragonStats.DestructionLevel >= destroyable.DestructionLevelNeeded)
                {
                    IDestroyable.OnPlayerCollide();
                    OnObjectDestroyedEvent?.Invoke();
                }   
            }
        }
    }

    public void SetCrushBuff(bool value)
    {
        _isCrushBuffed = value;
    }
}