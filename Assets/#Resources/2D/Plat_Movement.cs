using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]

public class Plat_Movement : Plat_CharBase
{

    private Rigidbody m_Rigidbody;
    private Vector2 m_playerMovementDirection;
    private bool m_isMoving;

    protected override void Awake()
    {
        base.Awake();

        m_inputActions.Plat_Player.Move.started += OnMoveAction;
        m_inputActions.Plat_Player.Move.canceled += OnMoveActionCancelled;

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMoveActionCancelled(InputAction.CallbackContext context)
    {
        StopMovement();
    }

    private void StopMovement()
    {

    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        HandleMovement(context.ReadValue<Vector2>());
    }

    private void HandleMovement(Vector2 movementInput)
    {
        m_playerMovementDirection = new Vector2(movementInput.x, movementInput.y);

        m_isMoving = movementInput.x != 0 || movementInput.y != 0;
    }
}
