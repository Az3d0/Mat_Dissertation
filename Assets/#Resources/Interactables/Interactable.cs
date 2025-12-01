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


    [SerializeField] private UnityEvent m_onTargetedEvents;
    [SerializeField] private UnityEvent m_onTargetCancelledEvents;

    [SerializeField] private UnityEvent m_onHitEvents;

    [Header("Intearction Type")]
    [SerializeField] bool m_proximityBased;
    private bool m_inTriggerZone;

    public void OnTargeted()
    {
        m_onTargetedEvents?.Invoke();

    }

    public void OnTargetCancelled()
    {
        m_onTargetCancelledEvents?.Invoke();
    }
    public void OnHit()
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
