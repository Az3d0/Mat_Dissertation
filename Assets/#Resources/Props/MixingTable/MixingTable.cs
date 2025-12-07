using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MixingTable : MonoBehaviour
{

    [SerializeField] AK.Wwise.Event m_track;
    [SerializeField] AK.Wwise.Switch m_nullSwitch;

    private TrackSwitcher[] m_switchers;
    private List<GameObject> m_switcherObjects;
    private TrackSwitcher m_activeSwitcher = null;
    private bool m_trackPlaying = false;

    //recorder fields
    private Stopwatch m_stopwatch;
    private int m_beatCount;
    [SerializeField] private int m_bpm = 100;
    private float m_adjustedTimestep;
    private bool m_isRecording = false;

    private List<Timestamp> m_timestamps;
    private void Awake()
    {
        //initate switchers
        m_switcherObjects = new List<GameObject>();
        m_switchers = transform.GetComponentsInChildren<TrackSwitcher>();

        foreach (TrackSwitcher switcher in m_switchers)
        {
            m_switcherObjects.Add(switcher.gameObject);
            UnityEngine.Debug.Log(switcher.gameObject);
        }

        //initate stopwatch
        m_stopwatch = new Stopwatch();
        m_beatCount = 0;
        m_timestamps = new List<Timestamp>();
    }

    void Update()
    {
        BPMCounter();
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
        }
    }

    private event Action<object> OnTrackSwitcher;
    public void TrySwitch(TrackSwitcher switcher)
    {
        TryMakeTimeStamp(OnTrackSwitcher, switcher);

        ResetOtherLevers(switcher.gameObject);
        if (!switcher == m_activeSwitcher)
        {
            switcher.Switch.SetValue(gameObject);
            m_activeSwitcher = switcher;
        }
        else
        {
            m_nullSwitch.SetValue(gameObject);
            m_activeSwitcher = null;
        }
        UnityEngine.Debug.Log($"TrySwitch Triggered. Toggling switch: {switcher.Switch.Name}");
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
    public void ToggleRecordEnabled(Lever lever)
    {
        m_isRecording = lever.State;
    }
    //THIS IS TEMPORARILY SPLIT INTO 2 FUNCTIONS. CHECK IN WITH MAT
    public void ToggleRecording(Lever lever)
    {
        if (lever.State)
        {
            if (m_isRecording)
            {
                m_beatCount = 0;
                m_stopwatch.Start(); 
                m_timestamps = new List<Timestamp>();

            }
        }
        else
        {
            m_stopwatch.Stop();
        }

    }
    private void BPMCounter()
    {
        if (m_isRecording && m_stopwatch.IsRunning)
        {
            m_adjustedTimestep = (float)m_bpm / 60f;
            float elapsedTime = (float)m_stopwatch.ElapsedMilliseconds / 1000f * m_adjustedTimestep;
            if ((int)elapsedTime > m_beatCount)
            {
                m_beatCount++;
            }
        }
    }
    private void TryMakeTimeStamp(Action<object> a, object var)
    {
        if (m_isRecording)
        {
            Timestamp timestamp = new Timestamp(1f, a);
            m_timestamps.Add(timestamp);
        }
    }

}

public struct Timestamp
{
    public Timestamp(float time, Action<object> a)
    {
        m_elapsedTime = time;
        m_action = a;
    }
    private float m_elapsedTime;
    private Action<object> m_action;
}

