using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] private float timeBetweenShots = 2f;
    [SerializeField] private Transform humanTransform;
    [SerializeField] private Transform rocketShootingPoint;
    [SerializeField] private GameObject rocketPrefab;

    private void Start()
    {
        StartCoroutine(WaitAndAShotCoroutine());
    }

    private IEnumerator WaitAndAShotCoroutine()
    {
        while (true)
        {
            var rocket = Instantiate(rocketPrefab, rocketShootingPoint.position, Quaternion.identity);
            rocket.transform.rotation = Quaternion.LookRotation(humanTransform.right);
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }
}
