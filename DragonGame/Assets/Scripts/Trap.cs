using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour, ICollidable
{
    public void OnPlayerCollision()
    {
        GameManager.Instance.GameOver();
    }
}
