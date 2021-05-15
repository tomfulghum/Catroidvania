using System.Collections.Generic;
using UnityEngine;

namespace tomfulghum.EventSystem
{
    public abstract class GameEvent : ScriptableObject
    {
        [SerializeField] bool debugMessages = false;

        protected HashSet<GameEventListener> eventListeners = new HashSet<GameEventListener>();

        protected virtual void Raise()
        {
            if (debugMessages && Debug.isDebugBuild)
                Debug.Log("Event raised: " + name);
        }

        public void Register(GameEventListener listener)
        {
            bool success = eventListeners.Add(listener);

            if (debugMessages && Debug.isDebugBuild) {
                if (success)
                    Debug.Log(listener.gameObject.name + " registered as listener for " + name + ".", listener);
                else
                    Debug.LogWarning(listener.gameObject.name + " is already registered as listener for " + name + "!", listener);
            }
        }

        public void Deregister(GameEventListener listener)
        {
            bool success = eventListeners.Remove(listener);

            if (debugMessages && Debug.isDebugBuild) {
                if (success)
                    Debug.Log(listener.gameObject.name + " deregistered as listener for " + name + ".", listener);
                else
                    Debug.LogWarning(listener.gameObject.name + " is not registered as listener for " + name + "!", listener);
            }
        }
    }
}
