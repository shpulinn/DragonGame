using System.Collections.Generic;
using UnityEngine;

public class ArrowHint : MonoBehaviour
{
    [SerializeField] private List<GameObject> linkedObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (linkedObjects.Count > 0)
            {
                foreach (GameObject linkedObject in linkedObjects)
                {
                    Destroy(linkedObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
