using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    [SerializeField] private GameObject m_defaultPlayer;

    private GameObject m_enabledPlayer;
    private GameObject m_dormantPlayer;
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

    public void TryUpdatePlayer(object obj)
    {
        if (obj.GetType() == typeof(GameObject))
        {
            if (((GameObject)obj).TryGetComponent(out InputHandler inputHandler))
            {
                if (m_enabledPlayer != null)
                {
                    m_dormantPlayer = m_enabledPlayer;
                    m_enabledPlayer.GetComponent<InputHandler>().DisablePlayer();
                }

                m_enabledPlayer = inputHandler.gameObject;
                inputHandler.EnablePlayer();
                Debug.Log($"New player enabled: {m_enabledPlayer.name}");
                
            }
            else
            {
                Debug.LogWarning("TryUpdatePlayer failed: no InputHandler component attached to GameObject");
            }
        }
        else
        {
            Debug.LogWarning("TryUpdatePlayer failed: no relevant data parsed");
        }
    }
    public void ResetDefaultPlayer()
    {
        if (m_defaultPlayer == null)
        {
            Debug.LogWarning("ResetDefaultPlayer failed: No default player assigned.");
            return;
        }

        TryUpdatePlayer(m_defaultPlayer);
    }
}
