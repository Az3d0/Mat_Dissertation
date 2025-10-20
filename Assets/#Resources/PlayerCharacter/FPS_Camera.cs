using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPS_Camera : FPSChar_Base
{

    [SerializeField] private float m_verticalSpeed = 1;
    [SerializeField] private float m_horizontalSpeed = 1;

    [SerializeField] private GameObject m_playerBody;
    protected override void Awake()
    {
        base.Awake();

        m_playerBody = transform.parent.gameObject;
        m_inputActions.Player.Look.performed += OnLook;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        HandleHRotation(context.ReadValue<Vector2>().x);
        HandleVRotation(context.ReadValue<Vector2>().y);

    }

    private void HandleVRotation(float y)
    {
        Vector3 VRotation = transform.eulerAngles;
        VRotation.z = 0;
        if (VRotation.x > 90) VRotation.x = - (360 - VRotation.x);
        VRotation.x += -y * 0.1f * m_verticalSpeed;

        VRotation.x = Mathf.Clamp(VRotation.x, -85, 85);
        transform.eulerAngles = VRotation;
    }

    private void HandleHRotation(float x)
    {
        Vector3 HRotation = m_playerBody.transform.eulerAngles;
        HRotation.y += x * 0.1f * m_horizontalSpeed;
        m_playerBody.transform.eulerAngles = HRotation;
    }
}
