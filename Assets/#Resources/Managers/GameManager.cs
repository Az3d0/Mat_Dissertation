using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private UnityEvent m_onDefaultStartEvents;

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
    private void Start()
    {
        m_onDefaultStartEvents.Invoke();
    }


}
