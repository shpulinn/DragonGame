using System;
using UnityEngine;

public class ShootingRotatingTrap : ShootingTrap
{
    [SerializeField] private Transform rotatingTransform;
    [SerializeField] private float rotatingSpeed = 2f;
    [SerializeField] private Vector3 rotatingVector;

    private void Update()
    {
        rotatingTransform.Rotate(rotatingVector * (rotatingSpeed * Time.deltaTime));
    }
}