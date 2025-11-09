using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPS_InputHandler))]
public class FPS_Camera : MonoBehaviour
{

    [SerializeField] private float m_verticalSpeed = 1;
    [SerializeField] private float m_horizontalSpeed = 1;

    [SerializeField] private GameObject m_playerBody;
    [SerializeField] private CinemachineCamera m_camera;

    private FPS_InputHandler m_InputHandler;
    private void Awake()
    {
        m_InputHandler = GetComponent<FPS_InputHandler>();
        m_InputHandler.m_inputActions.Player.Look.performed += OnLook;
    }

    private void OnLook(InputAction.CallbackContext context)
    {
        HandleHRotation(context.ReadValue<Vector2>().x);
        HandleVRotation(context.ReadValue<Vector2>().y);

    }

    private void HandleVRotation(float y)
    {
        Vector3 VRotation = m_camera.transform.eulerAngles;
        VRotation.z = 0;
        if (VRotation.x > 90) VRotation.x = - (360 - VRotation.x);
        VRotation.x += -y * 0.1f * m_verticalSpeed;

        VRotation.x = Mathf.Clamp(VRotation.x, -85, 85);
        m_camera.transform.eulerAngles = VRotation;
    }

    private void HandleHRotation(float x)
    {
        Vector3 HRotation = m_playerBody.transform.eulerAngles;
        HRotation.y += x * 0.1f * m_horizontalSpeed;
        m_playerBody.transform.eulerAngles = HRotation;
    }
}
