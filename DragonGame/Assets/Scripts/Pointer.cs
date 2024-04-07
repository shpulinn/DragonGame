using UnityEngine;

public class Pointer : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.OnInputHandled += OnInputHandled;
    }

    private void OnInputHandled()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnInputHandled -= OnInputHandled;
    }
}