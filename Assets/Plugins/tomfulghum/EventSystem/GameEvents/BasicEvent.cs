using System;
using UnityEngine;
using UnityEngine.Events;

namespace tomfulghum.EventSystem
{
    [Serializable]
    public class BasicEventResponse : UnityEvent { }
    public delegate void BasicEventDelegate();

    [CreateAssetMenu(menuName = "Game Events/Basic Event", order = 0)]
    public class BasicEvent : GameEvent
    {
        event BasicEventDelegate Trigger;

        public void Register(BasicEventDelegate response)
        {
            Trigger += response;
        }

        public void Deregister(BasicEventDelegate response)
        {
            Trigger -= response;
        }

        public new void Raise()
        {
            base.Raise();

            Trigger?.Invoke();
            foreach (var eventListener in eventListeners) {
                eventListener.InvokeBasicEventResponse();
            }
        }
    }
}
