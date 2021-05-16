using System;
using UnityEngine;
using UnityEngine.Events;

namespace tomfulghum.EventSystem
{
    [Serializable]
    public class BoolEventResponse : UnityEvent<bool> { }
    public delegate void BoolEventDelegate(bool _);

    [CreateAssetMenu(menuName = "Game Events/Bool Event", order = 1)]
    public class BoolEvent : GameEvent
    {
        event BoolEventDelegate Trigger;

        public void Register(BoolEventDelegate response)
        {
            Trigger += response;
        }

        public void Deregister(BoolEventDelegate response)
        {
            Trigger -= response;
        }

        public void Raise(bool data)
        {
            base.Raise();

            Trigger?.Invoke(data);
            var eventListenersArr = new GameEventListener[eventListeners.Count];
            eventListeners.CopyTo(eventListenersArr);
            foreach (var eventListener in eventListenersArr) {
                eventListener.InvokeBoolEventResponse(data);
            }
        }
    }
}
