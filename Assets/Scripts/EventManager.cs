using System.Collections.Generic;
using UnityEngine.Events;

// Useful script for events handling (mostly used for UI and animations)
// Reduces code cohesion and code size
public static class EventManager {
    
    private static Dictionary <string, UnityEvent> _eventDictionary;

    // Call this on the first game load
    public static void InitDict() {
        _eventDictionary = new Dictionary<string, UnityEvent>();
    }

    public static void StartListening (string eventName, UnityAction listener) {
        if (_eventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.AddListener(listener);
        } else {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            _eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void TriggerEvent (string eventName) {
        if (_eventDictionary.TryGetValue(eventName, out var thisEvent)) {
            thisEvent.Invoke();
        }
    }
    
}
