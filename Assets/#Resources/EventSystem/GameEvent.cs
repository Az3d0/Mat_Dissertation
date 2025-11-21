using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    public List<GameEventChannel> Listeners = new List<GameEventChannel>();
    private void Raise(object data)
    {
        foreach (GameEventChannel listener in Listeners)
        {
            listener.OnEventRaised(data);
        }
    }
    public void ParseGameObjectData(GameObject data)
    {
        Raise(data);
    }

    public void ParseFloatData(float data)
    {
        Raise(data);
    }

    public void ParseStringData(string data)
    {
        Raise(data);
    }

    public void ParseNoData()
    {
        Raise(null);
    }
    public void RegisterListener(GameEventChannel listener)
    {
        if(!Listeners.Contains(listener)) Listeners.Add(listener);
    }

    public void UnregisterListener(GameEventChannel listener)
    {
        if (Listeners.Contains(listener)) Listeners.Remove(listener);
    }
}