using UnityEngine;
using System;

public class ChunkTrigger : MonoBehaviour
{
    public static event Action<ChunkTrigger> OnChunkTriggered;

    private bool _wasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_wasTriggered)
            return;
        
        if (other.CompareTag("Player"))
        {
            OnChunkTriggered?.Invoke(this);
            _wasTriggered = true;
        }
    }

    private void OnDisable()
    {
        _wasTriggered = false;
    }
}