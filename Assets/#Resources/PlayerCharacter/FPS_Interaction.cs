using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InputHandler))]
public class FPS_Interaction : MonoBehaviour
{

    private Ray m_ray;
    private GameObject m_targetedInteractableGO;
    private Interactable m_targetedInteractable;
    private InputHandler m_inputHandler;

    [SerializeField] private CinemachineCamera m_camera;

    [SerializeField] private float m_rayLength = 3f;

    public Action<RaycastHit> ObjectDetected;

    private void Awake()
    {
        m_inputHandler = GetComponent<InputHandler>();
        m_inputHandler.m_inputActions.Player.Interact.started += OnInteract;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if(m_targetedInteractableGO != null)
        {
            m_targetedInteractable.OnHit();
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
            if(hitInfo.collider.gameObject != m_targetedInteractableGO)
            {                
                //invoke OnTargetCancelled on the previously targeted object before it gets overriden
                if (m_targetedInteractable != null) m_targetedInteractable.OnTargetCancelled();

                if (hitInfo.collider.TryGetComponent(out Interactable interactable))
                {
                    //assign currently targeted interactable
                    m_targetedInteractable = interactable;
                    m_targetedInteractableGO = hitInfo.collider.gameObject;

                    //invoke script 
                    interactable.OnTargeted();
                    //ObjectDetected.Invoke(hitInfo);
                }
                else
                {
                    //assign null to currently target interactable
                    m_targetedInteractableGO = null;
                    m_targetedInteractable = null;
                }
            }
        }
        else
        {
            //invoke OnTargetCancelled on the previously targeted object before it gets overriden
            if (m_targetedInteractable != null)
            {
                m_targetedInteractable.OnTargetCancelled();
            }

            //assign null to currently target interactable
            m_targetedInteractableGO = null;
            m_targetedInteractable = null;
        }
    }

    public void EnableDebugMode(Ray ray)
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * m_rayLength, Color.red);
    }

}
