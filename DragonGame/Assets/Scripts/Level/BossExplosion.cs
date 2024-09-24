using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    [SerializeField] private float attackRangeRadius = 10f;
    [SerializeField] private float delayBetweenAttacks = 4f;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private LayerMask dragonLayer;

    private void Start()
    {
        StartCoroutine(WaitAndSpawnExplosion());
    }

    private IEnumerator WaitAndSpawnExplosion()
    {
        while (true)
        {
            yield return new WaitForSeconds(delayBetweenAttacks);

            Collider[] cols = Physics.OverlapSphere(transform.position, attackRangeRadius, dragonLayer);

            if (cols.Length > 0)
            {
                Instantiate(explosionPrefab, cols[0].transform.position, quaternion.identity);
            }
            else yield return null;
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out DragonCollision dragonCollision) || other.TryGetComponent(out DragonPart dragonPart))
        {
            GameManager.Instance.TryTakeDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRangeRadius);
    }
}