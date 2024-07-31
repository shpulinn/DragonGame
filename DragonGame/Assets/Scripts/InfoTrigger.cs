using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InfoTrigger : MonoBehaviour
{
    [SerializeField] private GameObject InfoScreenPrefab;
    
    private void OnTriggerEnter(Collider other)
    {
        // at this moment only player can move on a scene
        if (InfoScreenPrefab == null)
        {
            return;
        }

        // stop the player for him can read information without crashing 
        if (other.TryGetComponent(out DragonMovement dragonMovement))
        {
            dragonMovement.enabled = false;
        }
        InfoScreenPrefab.SetActive(true);
        InputManager.Instance.DisposeControl();
        Destroy(gameObject);
    }
}