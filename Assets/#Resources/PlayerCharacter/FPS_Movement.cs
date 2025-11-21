using System;
using Unity.Cinemachine;
using Unity.Cinemachine.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(InputHandler))]
public class FPS_Movement : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField] private float  m_walkSpeed = 1;

    [Range(1f,5f)]
    [SerializeField] private float m_sprintSpeedMultiplier;

    private Vector3 m_playerMovementDirection;
    private bool m_isMoving;
    private bool m_isSprinting;
    private Vector3 m_playerMovementForce;

    private CharacterController m_CharacterController;
    private InputHandler m_inputHandler;
    private void Awake()
    {

        m_CharacterController = GetComponent<CharacterController>();
        m_inputHandler = GetComponent<InputHandler>();

        m_inputHandler.m_inputActions.Player.Move.performed += OnMoveAction;
        m_inputHandler.m_inputActions.Player.Move.canceled += OnMoveAction;
        m_inputHandler.m_inputActions.Player.Sprint.performed += OnSprintAction;
        m_inputHandler.m_inputActions.Player.Sprint.canceled += OnSprintAction;

    }

    private void OnSprintAction(InputAction.CallbackContext context)
    {
        m_isSprinting = context.ReadValueAsButton();
        Debug.Log(context.ReadValueAsButton());
    }


    private void FixedUpdate()
    {
        if (m_isMoving)
        {
            float totalSpeed = m_walkSpeed;
            if (m_isSprinting) totalSpeed = m_walkSpeed * m_sprintSpeedMultiplier;

            m_CharacterController.Move(m_playerMovementDirection * totalSpeed * 0.02f);

        }
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        HandleMovement(context.ReadValue<Vector2>());
    }



    private void HandleMovement(Vector2 movementInput)
    {
        m_playerMovementDirection = new Vector3(movementInput.x, 0, movementInput.y);

        m_isMoving = movementInput.x != 0 || movementInput.y != 0;

        //m_animator.SetBool("isWalking", IsMoving);

    }
}
