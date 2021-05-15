using UnityEngine;

namespace tomfulghum.EventSystem
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] GameEvent m_event = default;

        [SerializeField, HideInInspector] BasicEventResponse m_basicEventResponse = default;
        [SerializeField, HideInInspector] BoolEventResponse m_boolEventResponse = default;
        [SerializeField, HideInInspector] IntEventResponse m_intEventResponse = default;
        [SerializeField, HideInInspector] FloatEventResponse m_floatEventResponse = default;
        [SerializeField, HideInInspector] StringEventResponse m_stringEventResponse = default;

        void OnEnable()
        {
            m_event?.Register(this);
        }

        void OnDisable()
        {
            m_event?.Deregister(this);
        }

        internal void InvokeBasicEventResponse()
        {
            m_basicEventResponse?.Invoke();
        }

        internal void InvokeBoolEventResponse(bool data)
        {
            m_boolEventResponse?.Invoke(data);
        }

        internal void InvokeIntEventResponse(int data)
        {
            m_intEventResponse?.Invoke(data);
        }

        internal void InvokeFloatEventResponse(float data)
        {
            m_floatEventResponse?.Invoke(data);
        }

        internal void InvokeStringEventResponse(string data)
        {
            m_stringEventResponse?.Invoke(data);
        }
    }
}