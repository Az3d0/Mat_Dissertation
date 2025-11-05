using System;
using Unity.Cinemachine.Samples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class FPS_Movement : FPS_CharBase
{

    [SerializeField] private float  m_speed = 1;
    private Vector3 m_playerMovementDirection;
    private bool m_isMoving;
    private Vector3 m_playerMovementForce;
    private Rigidbody m_Rigidbody;
    protected override void Awake()
    {
        base.Awake();

        m_inputActions.Player.Move.performed += OnMoveAction;
        m_inputActions.Player.Move.canceled += OnMoveActionCancelled;

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMoveActionCancelled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void FixedUpdate()
    {
        if (m_isMoving)
        {
            m_Rigidbody.AddForce(m_playerMovementDirection * m_speed * 0.01f, ForceMode.Force);
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
        m_Rigidbody.linearVelocity = Vector3.zero; 
        m_isMoving = false;
    }
}
