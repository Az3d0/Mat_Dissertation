using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class P_Movement : PlayerCharacter
{
    private Vector3 m_playerMovementDirection;
    private bool m_isMoving;

    protected override void Awake()
    {
        base.Awake ();

        m_inputActions.Player.Move.performed += OnMoveAction;
        m_inputActions.Player.Move.canceled += OnMoveAction;
    }

    private void Update()
    {
        
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
