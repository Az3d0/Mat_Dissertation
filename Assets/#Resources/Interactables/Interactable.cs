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
    private bool m_isInteractionEnabled = true;
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
        if (!m_isInteractionEnabled) return;
        m_onHitEvents?.Invoke();
    }

    public void ToggleInteractionState()
    {
        m_isInteractionEnabled = !m_isInteractionEnabled;
    }

    //I might delete these later
    public void ToggleInteractionState(object obj)
    {
        if (obj.GetType() == typeof(GameObject))
        {
            if (((GameObject)obj).TryGetComponent(out Lever lever))
            {
                m_isInteractionEnabled = !lever.State;
                Debug.Log($"m_isInteractionEnabled set to {!lever.State} on {gameObject.name}.");
            }
            else
            {
                Debug.LogWarning("ToggleInteraction failed: no Lever component attached to GameObject");
            }
        }
        else if (obj.GetType() == typeof(Lever))
        {
            Lever lever = (Lever)obj;
            m_isInteractionEnabled = !lever.State;
        }
    }
    public void ToggleInteractionState(bool state)
    {
        m_isInteractionEnabled = state;
        Debug.Log($"m_isInteractionEnabled set to {state} on {gameObject.name}.");
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
