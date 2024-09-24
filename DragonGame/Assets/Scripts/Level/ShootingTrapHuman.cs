using System;
using UnityEngine;
using UnityEngine.Events;

public class ShootingTrapHuman : MonoBehaviour
{
    public UnityEvent OnDragonTrigger;

    private Animator _animator;
    private int _animDeadID;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animDeadID = Animator.StringToHash("Dead");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DragonCollision dragonCollision))
        {
            OnDragonTrigger?.Invoke();
            _animator.SetBool(_animDeadID, true);
            Destroy(gameObject, 1f);
        }
    }
}