using UnityEngine;
using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(FloatEvent))]
    public class FloatEventEditor : Editor
    {
        float m_debugData = default;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameEvent = target as FloatEvent;
            m_debugData = EditorGUILayout.FloatField("Debug Data", m_debugData);

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise Event")) {
                gameEvent.Raise(m_debugData);
            }
        }
    }
}
