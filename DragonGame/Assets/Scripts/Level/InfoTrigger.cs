using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InfoTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InfoScreenPrefab;

    private DragonMovementRB _movementRb;
    
    private void OnTriggerEnter(Collider other)
    {
        // at this moment only player can move on a scene
        if (InfoScreenPrefab == null)
        {
            return;
        }

        // stop the player for him can read information without crashing 
        if (other.TryGetComponent(out DragonMovementRB dragonMovement))
        {
            _movementRb = dragonMovement;
            _movementRb.DisableMovement();
        }
        InfoScreenPrefab.SetActive(true);
        InputManager.Instance.DisposeControl();
    }

    public void EnableMovement()
    {
        if (_movementRb)
        {
            _movementRb.EnableMovement();
        }
        Destroy(gameObject);
    }
}