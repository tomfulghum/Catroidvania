﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace tomfulghum.EventSystem
{
    [Serializable]
    public class IntEventResponse : UnityEvent<int> { }
    public delegate void IntEventDelegate(int _);

    [CreateAssetMenu(menuName = "Game Events/Int Event", order = 2)]
    public class IntEvent : GameEvent
    {
        event IntEventDelegate Trigger;

        public void Register(IntEventDelegate response)
        {
            Trigger += response;
        }

        public void Deregister(IntEventDelegate response)
        {
            Trigger -= response;
        }

        public void Raise(int data)
        {
            base.Raise();

            Trigger?.Invoke(data);
            foreach (var eventListener in eventListeners) {
                eventListener.InvokeIntEventResponse(data);
            }
        }
    }
}
