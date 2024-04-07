using UnityEngine;
using UnityEngine.AI;

public class StageDoor : MonoBehaviour
{
    private Animator _animator;
    private NavMeshObstacle _meshObstacle;
    private static readonly int Open = Animator.StringToHash("Open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _meshObstacle = GetComponentInChildren<NavMeshObstacle>();
    }

    public void OpenDoor()
    {
        _animator.SetTrigger(Open);
        _meshObstacle.gameObject.SetActive(false);
    }
}
