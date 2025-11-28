using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraManager : MonoBehaviour

{
    public static CameraManager Instance;

    private List<CinemachineCamera> m_cameras;
    private CinemachineCamera m_enabledCamera;
    private CinemachineCamera m_dormantCamera;

    
    [SerializeField] private GameObject m_defaultCamera;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        FindCamerasInScene();
    }

    private void FindCamerasInScene()
    {
        m_cameras = new List<CinemachineCamera>();
        var cameraObjects = FindObjectsByType(typeof(CinemachineCamera), FindObjectsSortMode.None);
        foreach ( var cameraObject in cameraObjects )
        {
            m_cameras.Add(cameraObject.GetComponent<CinemachineCamera>());
        }
    }

    public void TryUpdateCamera(object obj)
    {
        if(obj.GetType() == typeof(GameObject))
        {
            if (((GameObject)obj).TryGetComponent(out CinemachineCamera ccamera))
            {
                if (m_enabledCamera != null)
                {
                    m_dormantCamera = m_enabledCamera;
                    m_enabledCamera.Priority = 0;
                }

                m_enabledCamera = ccamera;
                ccamera.Priority = 1;
            }
            else
            {
                Debug.LogWarning("TryUpdateCamera failed: no CinemachineCamera component attached to GameObject");
            }
        }
        else if (obj.GetType() == typeof(CinemachineCamera))
        {
            CinemachineCamera ccamera = (CinemachineCamera)obj;
            if (m_enabledCamera != null)
            {
                m_dormantCamera = m_enabledCamera;
                m_enabledCamera.Priority = 0;
            }

            m_enabledCamera = ccamera;
            ccamera.Priority = 1;
        }
        else
        {
            Debug.LogWarning("TryUpdateCamera failed: no relevant data parsed");
        }

    }

    public void ResetDefaultCamera()
    {
        if (m_defaultCamera == null)
        {
            Debug.LogWarning("ResetDefaultCamera failed: No default camera assigned."); 
            return;
        }

        TryUpdateCamera(m_defaultCamera);
    }

    public void TryEnableDormantCamera()
    {
        TryUpdateCamera(m_dormantCamera);
    }

}
