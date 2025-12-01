using System;
using UnityEngine;

public class MixingTable : MonoBehaviour
{

    AK.Wwise.Switch m_switch;
    [SerializeField] AK.Wwise.Event m_track;
    public void TryPlayMusic()
    {
        m_track.Post(gameObject);
    }
    public void TrySwitch(TrackSwitcher switcher)
    { 
        switcher.Switch.SetValue(gameObject);
    }
}

