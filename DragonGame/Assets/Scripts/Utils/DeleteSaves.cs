using System;
using UnityEngine;

public class DeleteSaves : MonoBehaviour
{
    private SaveSystem _saveSystem;

    private void Awake()
    {
        _saveSystem = new SaveSystem();
    }

    public void DeleteSavesClick()
    {
        _saveSystem.DeleteSaves();
    }
}