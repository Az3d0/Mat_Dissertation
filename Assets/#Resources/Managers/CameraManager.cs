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

    public void TryUpdateCamera(object camera)
    {
        Debug.Log($"TryUpdateCamera Triggered. object: {camera.ToString()}");
        if(camera.GetType() == typeof(GameObject))
        {
            if (((GameObject)camera).TryGetComponent(out CinemachineCamera ccamera))
            {
                if (m_enabledCamera != null)
                {
                    m_enabledCamera.Priority = 0;
                }

                ccamera.Priority = 1;

                m_enabledCamera = ccamera;
            }
            else
            {
                Debug.LogWarning("TryUpdateCamera: GameObject does not have CinemachineCamera component");
            }
        }
        else
        {
            Debug.LogWarning("TryUpdateCamera: Parsed information must be GameObject");
        }

    }

    public void TestOnTryUpdateCamera(object obj)
    {
        if(obj.GetType() == typeof(string))
        {
            Debug.Log(obj.ToString());
        }
    }
}
