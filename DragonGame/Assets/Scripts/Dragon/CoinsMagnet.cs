using System;
using UnityEngine;

public class CoinsMagnet : MonoBehaviour
{
    [SerializeField] private LayerMask coinsLayer;
    [SerializeField] private float magnetRadius = 10f;
    [Tooltip("How fast coins would magnetize to dragon")]
    [SerializeField] private float magnetPower = 2f;

    private void Update()
    {
        if (CoinsMagnetItem.IsActive == false)
            return;
        
        var colls = Physics.OverlapSphere(transform.position, magnetRadius, coinsLayer);
        foreach (var coin in colls)
        {
            Vector3 direction = transform.position - coin.transform.position;
            coin.transform.Translate(direction * (Time.deltaTime * magnetPower));
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}