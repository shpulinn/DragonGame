using System.Collections.Generic;
using UnityEngine;

public class DragonLine : MonoBehaviour
{
    [SerializeField] private LineRenderer LineRenderer;
    [SerializeField] private List<Transform> dragonsParts;

    public void AddDragonPart(GameObject part)
    {
        dragonsParts.Add(part.transform);
    }

    private void Update()
    {
        for (int i = 0; i < dragonsParts.Count; i++)
        {
            LineRenderer.SetPosition(i, dragonsParts[i].position);
        }
    }
}
