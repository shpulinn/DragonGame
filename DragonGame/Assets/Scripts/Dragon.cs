using System;
using System.Collections;
using System.Collections.Generic;
using InsaneOne.TailEffect;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    public delegate void OnObjectDestroyed();

    public OnObjectDestroyed OnObjectDestroyedEvent;

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
    
    private List<NavMeshAgent> navAgents;

    public TextMeshProUGUI testText;
    
    [SerializeField] private float headRotationSpeed;

    private bool _isGameOver = false;

    private TailFx _tailFx;

    private bool _isCrushBuffed = false;

    private Vector3 _normalScale;
    private float _normalSpaceBetween;

    private UIManager _uiManager;

    public void SetSpaceBetween(float value)
    {
        _tailFx.spaceBetween = value;
    }

    public float GetSpaceBetween()
    {
        return _tailFx.spaceBetween;
    }

    void Start()
    {
        _tailFx = GetComponent<TailFx>();

        _normalScale = transform.GetChild(0).localScale;
        _normalSpaceBetween = GetSpaceBetween();
        
        _dragonLine = GetComponent<DragonLine>();
        tailSegments = new List<GameObject>();

        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;

        GameManager.Instance.OnGameOverEvent += OnGameOver;

        _uiManager = FindObjectOfType<UIManager>();
        
        // Создаем начальный размер змейки
        for (int i = 0; i < tailPrefabs.Count; i++)
        {
            //AddTail(tailPrefabs[i]);
        }
        
        navAgents = new List<NavMeshAgent>();
        foreach (var segment in tailSegments)
        {
            //var agent = segment.GetComponent<NavMeshAgent>();
            //navAgents.Add(agent);
        }

        previousPosition = transform.position;
    }

    private void OnGameOver()
    {
        _isGameOver = true;
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
        if (_isGameOver)
        {
            return;
        }
        
        if (InputManager.Instance.Joystick)
        {
            _navMeshAgent.isStopped = false;
            for (int i =  0; i < navAgents.Count; i++)
            {
                navAgents[i].isStopped = false;
            }
            _navMeshAgent.SetDestination(transform.position + InputManager.Instance.MoveVector);
            
            var rotation = Quaternion.LookRotation(InputManager.Instance.MoveVector.normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, headRotationSpeed * Time.deltaTime);

        }
        else
        {
            _navMeshAgent.SetDestination(transform.position);
            _navMeshAgent.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ICollidable collidable))
        {
            collidable.OnPlayerCollision();
        }

        if (_isCrushBuffed)
        {
            if (other.TryGetComponent(out IDestroyable destroyable))
            {
                destroyable.OnPlayerCollide();
                OnObjectDestroyedEvent?.Invoke();
            }
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

    public void ApplyScaleBuff(Vector3 newScale, float newSpaceBetween, float duration)
    {
        _isCrushBuffed = true;
        //transform.GetChild(0).localScale = newScale;
        _tailFx.ApplyScale(newScale);
        SetSpaceBetween(newSpaceBetween);
        Invoke(nameof(RemoveBuff), duration);
        _uiManager.StartTimer(duration);
    }

    public void RemoveBuff()
    {
        _isCrushBuffed = false;
        //transform.GetChild(0).localScale = _normalScale;
        _tailFx.ApplyScale(_normalScale);
        SetSpaceBetween(_normalSpaceBetween);
    }
}
