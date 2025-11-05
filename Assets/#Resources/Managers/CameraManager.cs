using NUnit.Framework;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

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

    public void EnableCamera(CinemachineCamera camera)
    {
        if(m_enabledCamera != null)
        {
            m_enabledCamera.Priority = 0;
        }

        camera.Priority = 1;

        m_enabledCamera = camera;
    }
}
