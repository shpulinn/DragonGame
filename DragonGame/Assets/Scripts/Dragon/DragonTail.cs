using System.Collections.Generic;
using UnityEngine;

public class DragonTail : MonoBehaviour
{
    public List<DragonPart> tailPrefabs = new List<DragonPart>();
    public float tailDistance = 1f;
    private List<GameObject> tailSegments;
    private DragonLine _dragonLine;
    private TailEffect _tailEffect;

    private void Start()
    {
        _tailEffect = GetComponent<TailEffect>();
        _dragonLine = GetComponent<DragonLine>();
        tailSegments = new List<GameObject>();
    }

    public void AddTail(DragonPart prefab)
    {
        // Implement AddTail logic here
    }

    public void SetSpaceBetween(float value)
    {
        _tailEffect.SpaceBetween = value;
    }

    public float GetSpaceBetween()
    {
        return _tailEffect.SpaceBetween;
    }
}