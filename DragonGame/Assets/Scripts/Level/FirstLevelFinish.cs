using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FirstLevelFinish : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // at this moment only player can move on a scene
        GameManager.Instance.GameWin();
    }
}