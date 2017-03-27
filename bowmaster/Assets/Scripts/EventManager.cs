using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class TransformEvent : UnityEvent<Transform>
{

}

/// <summary>
/// Manager of game events
/// </summary>
public class EventManager : MonoBehaviour
{
    /// <summary>
    /// dictionary of all events
    /// </summary>
    private Dictionary<string, TransformEvent> eventDictionary;

    private static EventManager eventManager;

    /// <summary>
    /// singleton
    /// </summary>
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, TransformEvent>();
        }
    }

    /// <summary>
    /// Start listening new event
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void StartListening(string eventName, UnityAction<Transform> listener)
    {
        TransformEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new TransformEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Stop listening event
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="listener"></param>
    public static void StopListening(string eventName, UnityAction<Transform> listener)
    {
        if (eventManager == null) return;
        TransformEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Trigger event
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="t"></param>
    public static void TriggerEvent(string eventName, Transform t)
    {
        TransformEvent thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(t);
        }
    }
}