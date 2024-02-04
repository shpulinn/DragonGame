using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float smoothTime = 0.3f;

    private bool _isPressed = false;
    
    public float speed = 1f;
    public List<DragonPart> tailPrefabs = new List<DragonPart>();
    //public int initialSize = 4;
    public float tailDistance = 1f;

    private List<Transform> tail = new List<Transform>();
    private Vector3 lastPosition = new Vector3(0,0,0);
    
    private List<Vector3> positionHistory; // Stores the positions for the tail to follow
    private List<GameObject> tailSegments; // Stores the tail segments

    private DragonLine _dragonLine;

    private Rigidbody _rb;

    private Joystick _joystick;

    void Start()
    {
        _dragonLine = GetComponent<DragonLine>();
        positionHistory = new List<Vector3>();
        tailSegments = new List<GameObject>();

        _rb = GetComponent<Rigidbody>();

        _joystick = FindObjectOfType<Joystick>();
        
        // Создаем начальный размер змейки
        for (int i = 0; i < tailPrefabs.Count; i++)
        {
            AddTail(tailPrefabs[i]);
        }
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
    
    /*
     *
     * void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horizontalInput * speed * Time.deltaTime, 0, 0);

        positions.Enqueue(transform.position);

        if (positions.Count > bodyParts.Length)
        {
            positions.Dequeue();
        }

        for (int i = 0; i < bodyParts.Length; i++)
        {
            Vector3 targetPosition = positions.ElementAtOrDefault(bodyParts.Length - i - 1);
            bodyParts[i].position = Vector3.Lerp(bodyParts[i].position, targetPosition, Time.deltaTime);
        }
    }
     * 
     */

    // private void FixedUpdate()
    // {
    //     if (_joystick.Direction != Vector2.zero)
    //     {
    //         _rb.AddForce(transform.GetChild(0).transform.forward * moveSpeed);
    //     }
    //     else
    //     {
    //         _rb.velocity = Vector3.zero;
    //     }
    // }

    void Update()
    {
        // Move the head
        //if (_isPressed == false) return;

        if (GameManager.Instance.IsPaused)
        {
            return;
        }

        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //mousePos.z
        // var mousePos = Input.mousePosition;
        // mousePos.z = 10; // выбираем расстояние = 10 единиц от камеры
        // var mousePosWorld = Camera.main.ScreenToWorldPoint(mousePos);

        //transform.LookAt(new Vector3(mousePos.x, 1, mousePos.z));



        //transform.position = new Vector3(mousePos.x, 1, mousePos.z);
        transform.position = Vector3.Lerp(transform.position, new Vector3(_joystick.Horizontal, 1, _joystick.Vertical), moveSpeed * Time.deltaTime);

        //Vector3 moveVector = (new Vector3(mousePos.x, 1, mousePos.z) - transform.position).normalized;
        //Vector3 inputVector = _joystick.Direction;
        Vector3 moveVector = (new Vector3(_joystick.Horizontal, 0, _joystick.Vertical) - transform.position).normalized;
        
        
        //transform.GetChild(0).transform.LookAt(new Vector3(mousePosWorld.x, 1, mousePosWorld.z));
        transform.GetChild(0).transform.LookAt(new Vector3(_joystick.Horizontal, 1, _joystick.Vertical));
        
        
        
        // поворачиваем змейку в сторону движения
        //transform.LookAt(new Vector3(mousePos.x, 1, mousePos.z));

        // Сохранение позиции головы в истории
        positionHistory.Insert(0, transform.position);

        // Движение сегментов хвоста
        for (int i = 0; i < tailSegments.Count; i++)
        {
            int historyIndex = (int)((i + 1) * tailDistance / Time.deltaTime);
            if (historyIndex < positionHistory.Count)
            {
                Vector3 targetPosition = positionHistory[historyIndex];
                //tailSegments[i].transform.position = Vector3.Slerp(tailSegments[i].transform.position, targetPosition, smoothTime);
                // Используйте Vector3.Lerp вместо прямого присвоения позиции
                tailSegments[i].transform.position = Vector3.Lerp(tailSegments[i].transform.position, targetPosition, smoothTime);
            }
        }

        // Ограничение размера истории позиций
        // int maxHistorySize = (int)((tailSegments.Count + 1) * tailDistance / Time.deltaTime);
        // if (positionHistory.Count > maxHistorySize)
        // {
        //     positionHistory.RemoveRange(maxHistorySize, positionHistory.Count - maxHistorySize);
        // }
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
        _rb.isKinematic = false;
    }

    private void OnMouseUp()
    {
        _isPressed = false;
        _rb.isKinematic = true;
    }
}
