using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]


public class Plat_Movement : Plat_CharBase
{

    private Rigidbody2D m_Rigidbody;
    private Vector2 m_playerMovementDirection;
    private bool m_isMoving;

    [SerializeField] private float m_speed = 1f;

    protected override void Awake()
    {
        base.Awake();

        m_inputActions.Plat_Player.Move.started += OnMoveAction;
        m_inputActions.Plat_Player.Move.canceled += OnMoveActionCancelled;

        m_Rigidbody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (m_isMoving)
        {
            m_Rigidbody.AddForce(m_playerMovementDirection * m_speed * 0.01f, ForceMode2D.Force);
        }
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
