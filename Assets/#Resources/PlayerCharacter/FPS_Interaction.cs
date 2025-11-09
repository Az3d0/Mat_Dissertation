using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(FPS_InputHandler))]
public class FPS_Interaction : MonoBehaviour
{

    private Ray m_ray;
    private GameObject m_hitInteractableGO;
    private Interactable m_hitInteractable;
    private FPS_InputHandler m_inputHandler;

    [SerializeField] private CinemachineCamera m_camera;

    [SerializeField] private float m_rayLength = 3f;

    public Action<RaycastHit> ObjectDetected;

    private void Awake()
    {
        m_inputHandler = GetComponent<FPS_InputHandler>();
        m_inputHandler.m_inputActions.Player.Interact.started += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if(m_hitInteractableGO != null)
        {
            m_hitInteractable.OnHit();
        }
    }

    private void Update()
    {
        InteractionDetector();
    }

    void InteractionDetector()
    {
        m_ray = new Ray(m_camera.transform.position, m_camera.transform.forward);
        
        if (Physics.Raycast(m_ray, out RaycastHit hitInfo, m_rayLength))
        {
            EnableDebugMode(m_ray);

            if (hitInfo.collider.TryGetComponent(out Interactable interactable) && hitInfo.collider.gameObject != m_hitInteractableGO)
            {
                m_hitInteractableGO = hitInfo.collider.gameObject;
                m_hitInteractable = interactable;
                //invoke script 
                interactable.OnTargeted();
                //ObjectDetected.Invoke(hitInfo);
            }
        }
        else
        {
            if(m_hitInteractableGO != null)
            {
                m_hitInteractableGO = null;
                m_hitInteractable = null;

                Debug.Log("no interactable located");
            }
        }
    }

    public virtual void EnableDebugMode(Ray ray)
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * m_rayLength, Color.red);
    }

}
