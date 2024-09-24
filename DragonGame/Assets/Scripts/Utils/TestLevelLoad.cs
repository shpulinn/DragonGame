using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLevelLoad : MonoBehaviour
{
    public void LoadTestLevel()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
