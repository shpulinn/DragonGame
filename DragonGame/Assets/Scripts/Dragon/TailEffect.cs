using System;
using System.Collections.Generic;
using UnityEngine;

public enum TailAxis { TwoD, ThreeD }
public enum TailStyle { Hard, Stretchy, Combined }

public class TailEffect : MonoBehaviour
{
    public event Action Loaded;
        
    [SerializeField] GameObject headPrefab;
    [SerializeField] GameObject segmentPrefab;
    [SerializeField] GameObject tailPrefab;
    [SerializeField, Range(1, 100)] int segments = 3;

    [SerializeField] TailStyle style = TailStyle.Stretchy;
    [SerializeField] TailAxis followAxis = TailAxis.ThreeD;
    [SerializeField, Range(0.1f, 250f)] float stretchySpeed = 10f;
    [SerializeField, Range(0f, 100f)] float spaceBetween = 1f;
    [SerializeField] bool rotateToPrevious = true;

    public List<Transform> Parts => parts;
    public float SpaceBetween {
		get => spaceBetween;
        set => spaceBetween = value;
    }
    
    readonly List<Transform> parts = new List<Transform>();
    Transform _head;

    void Start()
    {
        // first variant
        /*
        head = MakePart(headPrefab);

        // Устанавливаем начальную позицию каждого сегмента с учетом spaceBetween
        Vector3 lastPosition = transform.position; // Начальная позиция головы

        for (var i = 0; i < segments; i++)
        {
            var segment = MakePart(segmentPrefab);
        
            // Задаем позицию сегмента с отступом
            Vector3 offset = -transform.forward * spaceBetween * (i + 1);
            segment.position = lastPosition + offset;
        
            lastPosition = segment.position;
        }

        // То же самое для последнего сегмента хвоста
        var tail = MakePart(tailPrefab);
        tail.position = lastPosition - transform.forward * spaceBetween;

        Loaded?.Invoke();
        */
        
        // second variant
        _head = MakePart(headPrefab);

        Vector3 lastPosition = _head.position;
        Vector3 direction = GetDirection(_head);

        for (var i = 0; i < segments; i++)
        {
            Transform segment = MakePart(segmentPrefab);
            lastPosition -= direction * spaceBetween;
            segment.position = lastPosition;
            
            if (rotateToPrevious)
            {
                if (followAxis == TailAxis.ThreeD)
                    segment.LookAt(parts[parts.Count - 2].position);
                else
                    LookAt2D(segment, parts[parts.Count - 2].position);
            }
        }

        Transform tail = MakePart(tailPrefab);
        lastPosition -= direction * spaceBetween;
        tail.position = lastPosition;
        
        if (rotateToPrevious)
        {
            if (followAxis == TailAxis.ThreeD)
                tail.LookAt(parts[parts.Count - 2].position);
            else
                LookAt2D(tail, parts[parts.Count - 2].position);
        }
        
        Loaded?.Invoke();
    }

	public void ApplyScale(Vector3 scale) {
		for (int i = 0; i < parts.Count; i++) {
			parts[i].localScale = scale;
		} 
	}
    
    void Update()
    {
        UpdateElements();
    }

    private void UpdateElements()
    {
        _head.position = transform.position;
        _head.rotation = transform.rotation;
        
        for (var i = 1; i < Parts.Count; i++)
        {
            var previous = Parts[i - 1];
            var self = Parts[i];
            
            var previousPos = previous.position;
            
            if (style == TailStyle.Hard || style == TailStyle.Combined)
            {
                var direction = (previousPos - self.position).normalized;
                self.position = previousPos - direction * spaceBetween;
            }
            
            if (style == TailStyle.Stretchy || style == TailStyle.Combined)
            { 
                var offsetDirection = GetDirection(rotateToPrevious ? previous : Parts[0]);
                var targetPos = previousPos - offsetDirection * spaceBetween;
                self.position = Vector3.Lerp(self.position, targetPos, Time.smoothDeltaTime * stretchySpeed);
            }

            if (!rotateToPrevious)
                continue;
            
            if (followAxis == TailAxis.ThreeD)
                self.LookAt(previous.position);
            else
                LookAt2D(self, previous.position);
        }
    }
    
    Transform MakePart(GameObject prefab)
    {
        var part = Instantiate(prefab, transform.position, Quaternion.identity).transform;
        Parts.Add(part);
        
        return part;
    }

    Vector3 GetDirection(Transform tr)
    {
        return followAxis switch
        {
            TailAxis.ThreeD => tr.forward,
            TailAxis.TwoD => tr.up,
            _ => default
        };
    }

    static void LookAt2D(Transform tr, Vector3 position)
    {
        var direction = (position - tr.position).normalized;
        tr.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}