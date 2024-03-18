using System;
using UnityEngine;

public class Human : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;

    private Animator _animator;
    private static readonly int Dead = Animator.StringToHash("Dead");

    private bool _isDead = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnPlayerCollision()
    {
        if (_isDead) return;
        _isDead = true;
        GameManager.Instance.CatchHuman();
        AudioSource.PlayClipAtPoint(collectSound, transform.position);
        _animator.SetBool(Dead, true);
        Destroy(gameObject, 1.01f);
    }
}
