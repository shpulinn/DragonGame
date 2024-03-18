using UnityEngine;

public class Destroyable : MonoBehaviour, IDestroyable
{
    [SerializeField] private ParticleSystem destroyingParticles;
    
    public void OnPlayerCollide()
    {
        // анимация разрушения объекта
        if (destroyingParticles)
        {
            Instantiate(destroyingParticles, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
