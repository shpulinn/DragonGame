using System;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private int damage = 34;

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
            // show hit particles
            Destroy(gameObject);
        }
    }
}
