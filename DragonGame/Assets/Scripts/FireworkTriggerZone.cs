using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkTriggerZone : MonoBehaviour, ICollidable
{
    [SerializeField] private MeshRenderer planeMesh;
    [SerializeField] private Material disabledMat;
    [SerializeField] private Material activeMat;

    [SerializeField] private ParticleSystem firework;

    [SerializeField] private GameObject rocketPrefab;

    [SerializeField] private Transform bossTransform;
    [SerializeField] private AudioClip interactionSound;
    //[SerializeField] private float activeTime = 3f;

    private bool _isActive = false;
    private float _timer = 0;

    // void Update()
    // {
    //     if (_isActive)
    //     {
    //         _timer += Time.deltaTime;
    //         if (_timer >= activeTime)
    //         {
    //             _isActive = false;
    //             planeMesh.material = disabledMat;
    //             GameManager.Instance.DeactivateZone();
    //             _timer = 0;
    //         }
    //     }
    // }

    public void OnPlayerCollision()
    {
        if (_isActive == false)
        {
            _isActive = true;
            planeMesh.material = activeMat;
            //GameManager.Instance.ActivateZone();
            firework.Play();
            if (rocketPrefab)
            {
                var rocket = Instantiate(rocketPrefab, firework.transform.position, Quaternion.identity);
                rocket.transform.LookAt(bossTransform);
                //Vector3 trajectory = bossTransform.position - transform.position;
            }
            if (interactionSound)
            {
                AudioSource.PlayClipAtPoint(interactionSound, transform.position);
            }
        }
    }
}
