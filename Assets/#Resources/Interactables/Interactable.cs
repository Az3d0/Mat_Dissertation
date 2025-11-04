using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{

    [Header("OnTargeted")]
    [SerializeField] List<UnityEvent> m_onTargetedEvents;
    [Header("OnHit")]
    [SerializeField] List<UnityEvent> m_onHitEvents;

    [Header("Intearction Type")]
    [SerializeField] bool m_proximityBased;
    private bool m_inTriggerZone;
    private void Awake()
    {
    }

    public virtual void OnTargeted()
    {
        foreach (var e in m_onTargetedEvents)
        {
            e.Invoke();
        }
    }

    public virtual void OnHit()
    {
        foreach (var e in m_onHitEvents)
        {
            e.Invoke();
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!m_proximityBased) return;

        if(other.TryGetComponent<FPS_CharBase>(out var fpsChar))
        {
            m_inTriggerZone = true;
            Debug.Log($"Character in TriggerZone: {gameObject.name}");
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (!m_proximityBased) return;

        if (other.TryGetComponent<FPS_CharBase>(out var fpsChar))
        {
            m_inTriggerZone = false;
            Debug.Log($"Character in TriggerZone: {gameObject.name}");
        }
    }
}
