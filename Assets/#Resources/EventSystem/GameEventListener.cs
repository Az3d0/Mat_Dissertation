using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CustomGameEvent : UnityEvent<Component, object>
{

}
public class GameEventListener : MonoBehaviour
{
    public GameEvent GameEvent;
    public UnityEvent Response;

    public delegate void test(object o);
    private void OnEnable()
    {
        GameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        GameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(object data)
    {
        //Response?.Invoke(data);

        object obj = Response.GetPersistentTarget(0);
        object[] args = {data};

        MethodInfo method = obj.GetType().GetMethod(Response.GetPersistentMethodName(0));

        method.Invoke(obj, args);
    }
}
