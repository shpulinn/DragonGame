using TMPro;
using UnityEngine;

public class InfiniteLevelCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentDistanceText;
    [SerializeField] private TextMeshProUGUI bestDistanceText;
    
    private void OnEnable()
    {
        Transform dragon = FindObjectOfType<Dragon>().transform;
        int currentDistance = (int)Mathf.Floor(dragon.position.z);
        currentDistanceText.text = currentDistance.ToString();

        float bestDistance = PlayerPrefs.GetInt("Distance", 0);

        if (currentDistance > bestDistance)
        {
            bestDistanceText.text = currentDistance.ToString();
            PlayerPrefs.SetInt("Distance", currentDistance);
        }
        else
        {
            bestDistanceText.text = bestDistance.ToString();
        }
    }
}