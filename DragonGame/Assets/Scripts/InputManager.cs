using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [SerializeField] private LayerMask clickableLayerMask;
    [SerializeField] private Joystick _moveJoystick;
    
    private bool _joystick = false;

    private bool _inputUI;

    private Vector3 _moveVector;


    #region Properties
    
    public bool Joystick => _joystick;
    public Vector3 MoveVector => _moveVector;
    
    #endregion
    
    private void Awake()
    {
        Instance = this;

        SetupControls();
    }

    private void SetupControls()
    {
        //
    }
    
    private void Update()
    {
        //MousePosition = Mouse.current.position.ReadValue();
        if (_moveJoystick.Horizontal == 0 && _moveJoystick.Vertical == 0)
        {
            _joystick = false;
            return;
        }

        _joystick = true;
        _moveVector = new Vector3(_moveJoystick.Horizontal, 0, _moveJoystick.Vertical);
    }

    public void ToggleControl(bool value)
    {
        if (value == _inputUI)
            return;
        _inputUI = value;
    }
    
    
}
