using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour

{
    [HideInInspector] public InputSystem_Actions m_inputActions;
    private void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Disable();
    }

    public void EnablePlayer()
    {
        m_inputActions.Enable();
        gameObject.GetComponent<Renderer>().enabled = true;
    }
    public void DisablePlayer()
    {
        m_inputActions.Disable();
        gameObject.GetComponent<Renderer>().enabled = false;
    }
}
