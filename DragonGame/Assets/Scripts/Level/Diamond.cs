using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour, ICollidable
{
    [SerializeField] private AudioClip collectSound;

    public void OnPlayerCollision()
    {
        // for now, diamond is equal 100 coins
        GlobalEventManager.SendCoinCollected(100);
        if (collectSound)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }
        gameObject.SetActive(false);
    }
}
