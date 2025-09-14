using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using UnityEngine;

public class P_Interaction : MonoBehaviour
{
    private InputSystem_Actions m_inputActions;

    private Ray m_ray;
    private GameObject m_hitObject;

    [SerializeField] private float m_rayLength = 3f;

    public Action<RaycastHit> ObjectDetected;

    private void Awake()
    {
        m_inputActions = new InputSystem_Actions();
        m_inputActions.Enable();
    }

    private void Update()
    {
        InteractionDetector();
    }

    void InteractionDetector()
    {
        m_ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(m_ray, out RaycastHit hitInfo, m_rayLength))
        {
            EnableDebugMode(m_ray);

            if (hitInfo.collider.TryGetComponent(out Interactable interactable) && hitInfo.collider.gameObject != m_hitObject)
            {
                m_hitObject = hitInfo.collider.gameObject;
                //invoke script 
                interactable.OnTargeted();
                //ObjectDetected.Invoke(hitInfo);
            }
        }
        else
        {
            if(m_hitObject != null)
            {
                m_hitObject = null;
                Debug.Log("no interactable located");
            }
        }
    }

    public virtual void EnableDebugMode(Ray ray)
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * m_rayLength, Color.red);
    }

}
