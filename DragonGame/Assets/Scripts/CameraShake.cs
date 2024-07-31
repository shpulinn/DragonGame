using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    [SerializeField] private Transform camTransform;
	
    // How long the object should shake for.
    [SerializeField] private float shakeDuration = 0f;
	
    // Amplitude of the shake. A larger value shakes the camera harder.
    [SerializeField] private float shakeAmount = 0.7f;
    [SerializeField] private float decreaseFactor = 1.0f;
	
    Vector3 _originalPos;

    [SerializeField] private DragonCollision _dragonCollision;
	
    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }

        if (_dragonCollision == null)
        {
            Debug.LogWarning("There is no dragon collision attached to " + name);
        }
        _dragonCollision.OnObjectDestroyedEvent += Shake;
    }
	
    void OnEnable()
    {
        _originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = _originalPos + Random.insideUnitSphere * shakeAmount;
			
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = _originalPos;
        }
    }

    private void Shake()
    {
        shakeDuration = .3f;
    }

    private void OnDisable()
    {
        _dragonCollision.OnObjectDestroyedEvent -= Shake;
    }
}
