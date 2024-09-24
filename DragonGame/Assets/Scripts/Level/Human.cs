using System;
using UnityEngine;

public class Human : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;

    private Animator _animator;
    private static readonly int Dead = Animator.StringToHash("Dead");

    private bool _isDead = false;

    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void OnPlayerCollision()
    {
        if (_isDead) return;
        _isDead = true;
        GlobalEventManager.SendHumanCollected();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        _animator.SetBool(Dead, true);
        Invoke(nameof(DisableAfterDelay), 1f);
    }

    private void DisableAfterDelay()
    {
        gameObject.SetActive(false);
    }
}
