using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    private GameObject m_platPlayer;
    private GameObject m_FPSPlayer;
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
    }
    }
