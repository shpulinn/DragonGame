using UnityEngine;
using UnityEngine.AI;

public class BossChasing : MonoBehaviour
{
    [SerializeField] private float detectingRadius;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float angularSpeed = 250f;
    [SerializeField] private LayerMask dragonLayer;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.speed = moveSpeed;
        _agent.angularSpeed = angularSpeed;
    }

    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(transform.position, detectingRadius, dragonLayer);
        if (cols.Length < 1)
        {
            _agent.isStopped = true;
            return;
        } _agent.isStopped = false;
        
        foreach (var t in cols)
        {
            if (t.TryGetComponent(out Dragon dragon) /*|| t.TryGetComponent(out DragonPart dragonPart)*/)
            {
                _agent.SetDestination(t.transform.position);
            }
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
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectingRadius);
    }
}
