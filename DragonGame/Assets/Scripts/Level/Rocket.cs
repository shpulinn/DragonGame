using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private int damage = 34;
    [Space]
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private GameObject ExplosionParticles;
    [Space]
    [SerializeField] private float lifeTime = 5f;

    private Rigidbody _rb;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        
        _rb.AddForce(transform.forward * moveSpeed, ForceMode.Impulse);

        Destroy(gameObject, lifeTime);  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Boss boss))
        {
            boss.GetDamage(damage);
        }

        if (other.TryGetComponent(out DragonCollision dragonCollision) || other.TryGetComponent(out DragonPart dragonPart))
        {
            GameManager.Instance.TryTakeDamage();
        }

        if (ExplosionParticles)
        {
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        }

        if (explosionSound)
        {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        
        Destroy(gameObject);
    }
}
