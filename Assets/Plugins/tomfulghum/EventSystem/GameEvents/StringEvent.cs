using System;
using UnityEngine;
using UnityEngine.Events;

namespace tomfulghum.EventSystem
{
    [Serializable]
    public class StringEventResponse : UnityEvent<string> { }
    public delegate void StringEventDelegate(string _);

    [CreateAssetMenu(menuName = "Game Events/String Event", order = 4)]
    public class StringEvent : GameEvent
    {
        event StringEventDelegate Trigger;

        public void Register(StringEventDelegate response)
        {
            Trigger += response;
        }

        public void Deregister(StringEventDelegate response)
        {
            Trigger -= response;
        }

        public void Raise(string data)
        {
            base.Raise();

            Trigger?.Invoke(data);

            foreach (var eventListener in eventListeners) {
                eventListener.InvokeStringEventResponse(data);
            }
        }
    }
}
