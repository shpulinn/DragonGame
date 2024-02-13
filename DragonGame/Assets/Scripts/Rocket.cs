using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private int damage = 34;
    [SerializeField] private AudioClip explosionSound;

    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boss boss))
        {
            boss.GetDamage(damage);
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            // show hit particles
            Destroy(gameObject);
        }
    }
}
