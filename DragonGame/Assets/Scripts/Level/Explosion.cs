using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float timeBeforeExplosion = 3f;
    [SerializeField] private GameObject circleRadiusObj;
    [Space]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private AudioClip explosionSound;
    [Space]
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private LayerMask dragonLayer;

    private float _currentTime = 0;
    
    private void Update()
    {
        circleRadiusObj.transform.localScale = Vector3.one * curve.Evaluate(_currentTime);

        _currentTime += Time.deltaTime;

        if (_currentTime >= timeBeforeExplosion)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, explosionRadius, dragonLayer);
            if (cols.Length > 0)
            {
                if (cols[0].TryGetComponent(out Dragon dragon) || cols[0].TryGetComponent(out DragonPart dragonPart))
                {
                    GameManager.Instance.TryTakeDamage();
                    Instantiate(explosionEffect, transform.position, Quaternion.identity);
                    if (explosionSound)
                    {
                        AudioSource.PlayClipAtPoint(explosionSound, transform.position);
                    }
                }
            }
            circleRadiusObj.SetActive(false);
            Destroy(gameObject);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}