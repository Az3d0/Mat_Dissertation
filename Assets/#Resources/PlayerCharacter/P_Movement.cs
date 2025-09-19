using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class P_Movement : PlayerCharacter
{

    [SerializeField] private float  m_speed = 1;
    private Vector3 m_playerMovementDirection;
    private bool m_isMoving;

    private Rigidbody m_Rigidbody;
    protected override void Awake()
    {
        base.Awake();

        m_inputActions.Player.Move.performed += OnMoveAction;
        m_inputActions.Player.Move.canceled += OnMoveAction;

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(m_isMoving)
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

        Debug.Log(m_isMoving);

        //m_animator.SetBool("isWalking", IsMoving);

    }
}
