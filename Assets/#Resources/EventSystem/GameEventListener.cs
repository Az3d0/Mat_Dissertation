using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public List<GameEventChannel> EventChannels;

    private void OnEnable()
    {
        foreach(GameEventChannel channel in EventChannels)
        {
            channel.GameEvent.RegisterListener(channel);
        }
    }

    private void OnDisable()
    {
        foreach (GameEventChannel channel in EventChannels)
        {
            channel.GameEvent.UnregisterListener(channel);
        }
    }
}

[System.Serializable]
public class GameEventChannel
{
    public GameEvent GameEvent;
    public UnityEvent Response;


    public void OnEventRaised(object data)
    {
        if (data == null)
        {
            Response?.Invoke();
        }
        else
        {
            //loop through all events assigned in the editor
            for (int i = 0; i < Response.GetPersistentEventCount(); i++)
            {
                try
                {
                    //get UnityEvent i 
                    object obj = Response.GetPersistentTarget(i);

                    //modify parsed data
                    object[] args = { data };

                    //get the method by name assigned in editor
                    MethodInfo method = obj.GetType().GetMethod(Response.GetPersistentMethodName(i));

                    //invoke the UnityEvent using the parsed data instead of the one assigned in the editor
                    method.Invoke(obj, args);

                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Couldn't invoke action {Response.GetPersistentMethodName(i).ToString()}. Error:{e.Message}");
                }
            }
        }
    }

}