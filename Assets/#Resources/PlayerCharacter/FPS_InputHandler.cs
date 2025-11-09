using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPS_InputHandler : MonoBehaviour

{
    [HideInInspector] public InputSystem_Actions m_inputActions;
    private  void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }
}
