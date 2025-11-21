using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject m_defaultUI;

    private Canvas m_enabledCanvas;
    private Canvas m_dormantCanvas;
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

    public void TryUpdateUI(object obj)
    {
        if (obj.GetType() == typeof(GameObject))
        {
            if (((GameObject)obj).TryGetComponent(out Canvas canvas))
            {
                if (m_enabledCanvas != null)
                {
                    m_dormantCanvas = m_enabledCanvas;
                    m_enabledCanvas.enabled = false;
                }

                m_enabledCanvas = canvas;
                m_enabledCanvas.enabled = true;

            }
            else
            {
                Debug.LogWarning("TryUpdateUI failed: no Canvas component attached to GameObject");
            }
        }
        else
        {
            Debug.LogWarning("TryUpdateUI failed: no relevant data parsed");
        }
    }

    public void TryUpdateUIAddatively(object obj)
    {
        if (obj.GetType() == typeof(GameObject))
        {
            if (((GameObject)obj).TryGetComponent(out Canvas canvas))
            {
                canvas.enabled = true;
            }
            else
            {
                Debug.LogWarning("TryUpdateUI failed: no Canvas component attached to GameObject");
            }
        }
        else
        {
            Debug.LogWarning("TryUpdateUI failed: no relevant data parsed");
        }
    }
    public void ResetDefaultUI()
    {
        if (m_defaultUI == null)
        {
            Debug.LogWarning("ResetDefaultUI failed: No default player assigned.");
            return;
        }

        TryUpdateUI(m_defaultUI);
    }
}
