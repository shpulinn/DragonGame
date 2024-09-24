using System.Collections.Generic;
using UnityEngine;

public class MovingHuman : Human
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float minDistanceToPoint = .1f;
    [SerializeField] private GameObject wayPointsTransform;
    [SerializeField] private List<Transform> wayPoints;
    [Space] [SerializeField] private GameObject ExplosionParticles;

    private int _currentWayPointIndex = 0;

    protected override void Start()
    {
        base.Start();
        wayPointsTransform.transform.parent = null;
    }

    public override void OnPlayerCollision()
    {
        base.OnPlayerCollision();
        if (GameManager.Instance.IsPaused) return;
        GameManager.Instance.TryTakeDamage();
        moveSpeed = 0;
        if (ExplosionParticles)
        {
            Instantiate(ExplosionParticles, transform.position, Quaternion.identity);
        }
    }
    
    private void Update()
    {
        if (Vector3.Distance(transform.position, wayPoints[_currentWayPointIndex].position) > minDistanceToPoint)
        {
            Vector3 direction = wayPoints[_currentWayPointIndex].position - transform.position;
            direction.y = 0;
        
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        } 
        else 
        {
            _currentWayPointIndex++;
            if (_currentWayPointIndex == wayPoints.Count)
            {
                _currentWayPointIndex = 0;
            }
        }
    }
}