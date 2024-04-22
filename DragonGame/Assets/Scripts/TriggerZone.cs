using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerZone : MonoBehaviour, ICollidable
{
    [SerializeField] private MeshRenderer planeMesh;
    [SerializeField] private Material disabledMat;
    [SerializeField] private Material activeMat;
    [SerializeField] private float activeTime = 3f;
    [SerializeField] private AudioClip interactionSound;

    private bool _isActive = false;
    private float _timer = 0;

    void Update()
    {
        if (_isActive)
        {
            _timer += Time.deltaTime;
            if (_timer >= activeTime)
            {
                _isActive = false;
                planeMesh.material = disabledMat;
                GameManager.Instance.DeactivateZone();
                _timer = 0;
            }
        }
    }

    public void OnPlayerCollision()
    {
        if (_isActive == false)
        {
            _isActive = true;
            planeMesh.material = activeMat;
            GameManager.Instance.ActivateZone();
            if (interactionSound)
            {
                AudioSource.PlayClipAtPoint(interactionSound, transform.position);
            }
        }
    }
}
