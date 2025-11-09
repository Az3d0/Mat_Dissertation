using System;
using Unity.Cinemachine.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof(CharacterController))]
[RequireComponent (typeof(FPS_InputHandler))]
public class FPS_Movement : MonoBehaviour
{

    [SerializeField] private float  m_speed = 1;
    private Vector3 m_playerMovementDirection;
    private bool m_isMoving;
    private Vector3 m_playerMovementForce;
    private CharacterController m_CharacterController;
    private FPS_InputHandler m_inputHandler;
    private void Awake()
    {

        m_CharacterController = GetComponent<CharacterController>();
        m_inputHandler = GetComponent<FPS_InputHandler>();

        m_inputHandler.m_inputActions.Player.Move.performed += OnMoveAction;
        m_inputHandler.m_inputActions.Player.Move.canceled += OnMoveActionCancelled;

    }

    private void OnMoveActionCancelled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void FixedUpdate()
    {
        if (m_isMoving)
        {
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

    private void StopMovement()
    {
        m_playerMovementDirection = Vector3.zero;
        m_isMoving = false;
    }
}
