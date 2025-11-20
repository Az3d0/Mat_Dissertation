using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour
{

    [Header("Events")]
    [Space(10)]


    [SerializeField] UnityEvent m_onTargetedEvents;
    [SerializeField] UnityEvent m_onHitEvents;

    [Header("Intearction Type")]
    [SerializeField] bool m_proximityBased;
    private bool m_inTriggerZone;

    public virtual void OnTargeted()
    {
        m_onTargetedEvents?.Invoke();

    }

    public virtual void OnHit()
    {
        m_onHitEvents?.Invoke();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (!m_proximityBased) return;

        if(other.TryGetComponent<InputHandler>(out var fpsChar))
        {
            m_inTriggerZone = true;
            Debug.Log($"Character in TriggerZone: {gameObject.name}");
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (!m_proximityBased) return;

        if (other.TryGetComponent<InputHandler>(out var fpsChar))
        {
            m_inTriggerZone = false;
            Debug.Log($"Character left TriggerZone: {gameObject.name}");
        }
    }
}
