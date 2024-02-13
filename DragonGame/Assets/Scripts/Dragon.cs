using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    private bool _isPressed = false;
    
    public float speed = 1f;
    public List<DragonPart> tailPrefabs = new List<DragonPart>();
    //public int initialSize = 4;
    public float tailDistance = 1f;

    private List<Transform> tail = new List<Transform>();
    private Vector3 lastPosition = new Vector3(0,0,0);
    
    private List<GameObject> tailSegments; // Stores the tail segments

    private DragonLine _dragonLine;

    private NavMeshAgent _navMeshAgent;

    private Vector3 previousPosition;

    void Start()
    {
        _dragonLine = GetComponent<DragonLine>();
        tailSegments = new List<GameObject>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        // Создаем начальный размер змейки
        for (int i = 0; i < tailPrefabs.Count; i++)
        {
            AddTail(tailPrefabs[i]);
        }

        previousPosition = transform.position;
    }

    void AddTail(DragonPart prefab)
    {
        DragonPart newTail = Instantiate(prefab, lastPosition + new Vector3(0, 1, -1.05f * (tailSegments.Count + 1)), Quaternion.identity);
        if (tailSegments.Count < 1)
            newTail.SetTarget(transform);
        else
            newTail.SetTarget(tailSegments[^1].transform);
        tailSegments.Add(newTail.gameObject);
        _dragonLine.AddDragonPart(newTail.gameObject);
    }

     
    void Update()
    {
        // Move the head
        //if (_isPressed == false) return;

        if (GameManager.Instance.IsPaused)
        {
            return;
        }

        if (InputManager.Instance.Joystick)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(transform.position + InputManager.Instance.MoveVector);
        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _navMeshAgent.isStopped = true;
        }
            
        /*
        if (!_navMeshAgent.pathPending)
        {
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    //_animator.SetBool(animRunningBool, false);
                    //_isRunning = false;
                    _navMeshAgent.isStopped = true;
                }
            }
        }*/


        if (InputManager.Instance.Joystick)
        {
            Vector3 previousSegmentPosition = transform.position;
            tailSegments[0].transform.position = Vector3.Slerp(tailSegments[0].transform.position, previousSegmentPosition, Time.deltaTime * speed);
            // Обновляем позиции хвоста, чтобы они следовали за головой
            for (int i =  1; i < tailSegments.Count; i++)
            {
                // Вычисляем позицию предыдущего сегмента
                previousSegmentPosition = tailSegments[i -  1].transform.position;
                // Перемещаем текущий сегмент к позиции предыдущего
                while (Vector3.Distance(tailSegments[i].transform.position, tailSegments[i-1].transform.position) > tailDistance)
                {
                    tailSegments[i].transform.position = Vector3.Slerp(tailSegments[i].transform.position,
                        previousSegmentPosition, Time.deltaTime * speed);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollidable collidable))
        {
            collidable.OnPlayerCollision();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            transform.position = transform.position;
        }
    }

    private void OnMouseDown()
    {
        _isPressed = true;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
    }
    
    #region Testing

    public void SetTailDistance(float value)
    {
        tailDistance = value;
    }

    public void SetTailSpeed(float value)
    {
        speed = value;
    }
    
    #endregion
}
