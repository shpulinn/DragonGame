using UnityEngine;
using UnityEngine.AI;

public class DragonMovement : MonoBehaviour
{
    [SerializeField] private float normalMoveSpeed = 8f;
    [SerializeField] private float slowMoveSpeed = 4f;
    [SerializeField] private float headRotationSpeed;
    private NavMeshAgent _navMeshAgent;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        // stopping dragon's movement after small delay for nav esh could be built
        Invoke(nameof(StopMoveAfterDelay), .05f);
    }

    public void Move(Vector3 moveVector, bool isControlled)
    {
        if (!_navMeshAgent.isOnNavMesh)
            return;
        if (isControlled)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.Move(moveVector * (normalMoveSpeed * Time.deltaTime));
            
            var rotation = Quaternion.LookRotation(moveVector.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, headRotationSpeed * Time.deltaTime);
        }
        else
        {
            if (enabled)
            {
                if (_navMeshAgent.isStopped)
                {
                    return;
                }
                _navMeshAgent.Move(transform.forward * (slowMoveSpeed * Time.deltaTime));
            }
        }
    }

    private void StopMoveAfterDelay()
    {
        _navMeshAgent.isStopped = true;
    }
}