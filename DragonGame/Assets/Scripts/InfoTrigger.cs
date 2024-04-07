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
        InfoScreenPrefab.SetActive(true);
        InputManager.Instance.DisposeControl();
        Destroy(gameObject);
    }
}