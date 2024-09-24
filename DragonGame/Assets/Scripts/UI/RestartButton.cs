using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void RestartLevel()
    {
        Time.timeScale = 1;

        if (CoinsMagnetItem.IsActive)
        {
            CoinsMagnetItem.IsActive = false;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
