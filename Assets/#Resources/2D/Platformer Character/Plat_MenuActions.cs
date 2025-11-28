using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputHandler))]
public class Plat_MenuActions : MonoBehaviour
{

    private InputHandler m_inputHandler;
    [SerializeField] private UnityEvent m_onExitEvent;

    private void Awake()
    {
        m_inputHandler = GetComponent<InputHandler>();
    }

    private void Start()
    {
        m_inputHandler.m_inputActions.Plat_Player.Exit.performed += OnExit;
    }

    private void OnExit(InputAction.CallbackContext context)
    {
        Debug.Log("Exit Button Pressed");
        m_onExitEvent?.Invoke();
    }
}
