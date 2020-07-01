using System.Collections.Generic;
using UnityEngine.Events;

// Useful script for events handling (mostly used for UI and animations)
// Events may reduce code cohesion and code size
public static class EventManager {

    private static readonly Dictionary <string, UnityEvent> EventDictionary = new Dictionary<string, UnityEvent>();
    
    public static void StartListening (string eventName, UnityAction listener) {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            EventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void TriggerEvent (string eventName) {
        if (EventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.Invoke();
        }
    }
    
}
