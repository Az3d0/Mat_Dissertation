using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]

[RequireComponent (typeof(InputHandler))]
public class Plat_Movement : MonoBehaviour
{

    private Rigidbody2D m_rigidbody;
    private Vector2 m_playerMovementDirection;
    private bool m_isMoving;
    private InputHandler m_inputHandler;

    [SerializeField] private float m_speed = 1f;

    private void Awake()
    {
        m_inputHandler = GetComponent<InputHandler>();
        m_rigidbody = GetComponent<Rigidbody2D>();

        Debug.Log(m_inputHandler);
        m_inputHandler.m_inputActions.Plat_Player.Move.performed += OnMoveAction;
        m_inputHandler.m_inputActions.Plat_Player.Move.canceled += OnMoveAction;
    }
    private void FixedUpdate()
    {
        if (m_isMoving)
        {
            m_rigidbody.AddForce(m_playerMovementDirection * m_speed * 0.01f, ForceMode2D.Force);
        }
    }

    private void OnMoveAction(InputAction.CallbackContext context)
    {
        HandleMovement(context.ReadValue<Vector2>());
    }

    private void HandleMovement(Vector2 movementInput)
    {
        Debug.Log("plat character moving");
        m_playerMovementDirection = new Vector2(movementInput.x, movementInput.y);

        m_isMoving = movementInput.x != 0 || movementInput.y != 0;
    }
}
