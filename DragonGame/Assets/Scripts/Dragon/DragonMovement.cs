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
        _navMeshAgent.isStopped = true;
    }

    public void Move(Vector3 moveVector, bool isControlled)
    {
        if (isControlled)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.Move(moveVector * (normalMoveSpeed * Time.deltaTime));
            
            var rotation = Quaternion.LookRotation(moveVector.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, headRotationSpeed * Time.deltaTime);
        }
        else
        {
            if (_navMeshAgent.isStopped || this.enabled == false)
            {
                return;
            }
            _navMeshAgent.Move(transform.forward * (slowMoveSpeed * Time.deltaTime));
        }
    }
}