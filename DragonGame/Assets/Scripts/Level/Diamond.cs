using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;

    public void OnPlayerCollision()
    {
        CoinsManager.Instance.AddDiamond();
        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        gameObject.SetActive(false);
    }
}
