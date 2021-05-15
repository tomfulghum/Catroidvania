using System;
using UnityEngine;
using UnityEngine.Events;

namespace tomfulghum.EventSystem
{
    [Serializable]
    public class FloatEventResponse : UnityEvent<float> { }
    public delegate void FloatEventDelegate(float _);

    [CreateAssetMenu(menuName = "Game Events/Float Event", order = 3)]
    public class FloatEvent : GameEvent
    {
        event FloatEventDelegate Trigger;

        public void Register(FloatEventDelegate response)
        {
            Trigger += response;
        }

        public void Deregister(FloatEventDelegate response)
        {
            Trigger -= response;
        }

        public void Raise(float data)
        {
            base.Raise();

            Trigger?.Invoke(data);
            foreach (var eventListener in eventListeners) {
                eventListener.InvokeFloatEventResponse(data);
            }
        }
    }
}
