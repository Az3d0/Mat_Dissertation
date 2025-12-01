using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class MixingTable : MonoBehaviour
{

    [SerializeField] AK.Wwise.Event m_track;
    [SerializeField] AK.Wwise.Switch m_nullSwitch;

    private TrackSwitcher[] m_switchers;
    private List<GameObject> m_switcherObjects;
    private bool m_trackPlaying = false;

    private void Awake()
    {
        m_switcherObjects = new List<GameObject>();
        m_switchers = transform.GetComponentsInChildren<TrackSwitcher>();
        foreach (TrackSwitcher switcher in m_switchers)
        {
            m_switcherObjects.Add(switcher.gameObject);
            Debug.Log(switcher.gameObject);
        }
    }
    public void ToggleMusic(Lever startLever)
    {
        m_trackPlaying = startLever.State;
        if (!m_trackPlaying)
        {
            m_track.Post(gameObject);
            m_trackPlaying=true;
        }
        else
        {
            m_track.Stop(gameObject);
            m_trackPlaying = false;

            ResetOtherLevers(startLever.gameObject);
            m_nullSwitch.SetValue(gameObject);
        }
    }
    public void TrySwitch(TrackSwitcher switcher)
    {
        Debug.Log($"TrySwitch Triggered. Toggling switch: {switcher.Switch.Name}");

        switcher.Switch.SetValue(gameObject);
        ResetOtherLevers(switcher.gameObject);
    }

    public void ResetOtherLevers(GameObject switcherObject)
    {
        foreach(GameObject go in m_switcherObjects)
        {
            if (go == switcherObject)
            { 
                continue;
            }

            if (go.TryGetComponent<Lever>(out Lever lever))
            {
                if (lever.State) lever.ToggleState();
            }
        }
    }

    public void ToggleRecord(Lever lever)
    {

    }

}

