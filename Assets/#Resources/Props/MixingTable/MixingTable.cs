using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MixingTable : MonoBehaviour
{

    [SerializeField] AK.Wwise.Event m_track;
    [SerializeField] AK.Wwise.Switch m_nullSwitch;

    private TrackSwitcher m_activeSwitcher = null;
    private bool m_isPlaying = false;

    //recorder fields
    private Stopwatch m_recordStopwatch;
    private bool m_isRecording = false;
    private LinkedList<Timestamp> m_timestamps;

    //replayer
    private Stopwatch m_replayStopwatch;
    private bool m_isReplaying = false;
    private LinkedListNode<Timestamp> m_currentlyReplayedStamp;
    //beat
    private int m_beatCount;
    [SerializeField] private int m_bpm = 100;
    private float m_adjustedTimestep;

    private void Awake()
    {
        //initate stopwatch
        m_recordStopwatch = new Stopwatch();
        m_beatCount = 0;
        m_timestamps = new LinkedList<Timestamp>();
        m_replayStopwatch = new Stopwatch();

    }

    void Update()
    {
        BPMCounter();
        ReplayRecording();
    }

    public void ToggleMusic(Lever startLever)
    {
        m_isPlaying = startLever.State;
        UnityEngine.Debug.Log($"ToggleMusic Triggered {m_isPlaying}");
        if (m_isPlaying)
        {
            m_track.Post(gameObject);
        }
        else
        {
            m_track.Stop(gameObject);
        }
    }

    public void TrySwitch(TrackSwitcher switcher)
    {
        //timestamp currently doesnt record nullswitch as nullswitch. change that? It probably doesn't matter.
        TryMakeTimeStamp(switcher);

        if (switcher != m_activeSwitcher)
        {
            switcher.Switch.SetValue(gameObject);
            m_activeSwitcher = switcher;
            UnityEngine.Debug.Log($"TrySwitch Triggered. Toggling switch: {switcher.Switch.Name}");
        }
        else
        {
            m_nullSwitch.SetValue(gameObject);
            m_activeSwitcher = null;
            UnityEngine.Debug.Log($"TrySwitch Triggered. Toggling switch: null switch");
        }
    }

    public void ToggleRecording(Lever lever)
    {
        if (m_isReplaying) return;

        UnityEngine.Debug.Log("recording started");
        if (lever.State)
        {
            m_beatCount = 0;
            m_recordStopwatch.Reset();
            m_recordStopwatch.Start();
            m_timestamps = new LinkedList<Timestamp>();
        }
        else
        {
            TryMakeTimeStamp("end");
            m_recordStopwatch.Stop();
            // we can save the list somewhere here
            m_currentlyReplayedStamp = m_timestamps.First;
            DebugTimestamps(m_timestamps);

        }
        m_isRecording = lever.State;
    }

    private void DebugTimestamps(LinkedList<Timestamp> timestamps)
    {
        int count = 0;
        UnityEngine.Debug.Log($"debuging {timestamps.Count} timestamps recorded:");

        foreach (Timestamp timestamp in timestamps)
        {
            UnityEngine.Debug.Log($"Timestamp {count}: time {timestamp.ElapsedTime}, action {timestamp.Var}"); 
            count++;
        }
    }
    private void BPMCounter()
    {
        if (m_isRecording && m_recordStopwatch.IsRunning)
        {
            m_adjustedTimestep = (float)m_bpm / 60f;
            float elapsedTime = (float)m_recordStopwatch.ElapsedMilliseconds / 1000f * m_adjustedTimestep;
            if ((int)elapsedTime > m_beatCount)
            {
                m_beatCount++;
            }
        }
    }
    private void TryMakeTimeStamp(object var)
    {
        if (m_isRecording)
        {
            Timestamp timestamp = new Timestamp((float)m_recordStopwatch.Elapsed.TotalSeconds, var);
            m_timestamps.AddLast(timestamp);
        }
    }

    public void ToggleReplayRecording(Lever lever)
    {
        UnityEngine.Debug.Log("Replaying Recording");
        ToggleMusic(lever);
        m_isReplaying = true;
        m_replayStopwatch.Reset();
        m_replayStopwatch.Start();
    }

    //put this into update
    private void ReplayRecording()
    {
        if (m_isReplaying)
        {
            Timestamp timestamp = m_currentlyReplayedStamp.Value;
            if (m_replayStopwatch.Elapsed.TotalSeconds >= timestamp.ElapsedTime)
            {
                if (timestamp.Var.GetType() == typeof(TrackSwitcher))
                {
                    TrackSwitcher switcher = (TrackSwitcher)timestamp.Var;
                    TrySwitch(switcher);
                    UnityEngine.Debug.Log($"{timestamp.ElapsedTime} - {switcher.Switch}");

                    m_currentlyReplayedStamp = m_currentlyReplayedStamp.Next;
                }
                // this isn't great code
                else if (timestamp.Var.GetType() == typeof(string))
                {
                    UnityEngine.Debug.Log($"{timestamp.ElapsedTime} - {timestamp.Var}");
                    m_track.Stop(gameObject);
                    m_isPlaying = false;
                    m_isReplaying = false;
                }
            }
        }
    }

}

public struct Timestamp
{
    public Timestamp(float time, object var)
    {
        ElapsedTime = time;
        Var = var;
    }
    public float ElapsedTime;
    public object Var;
}

