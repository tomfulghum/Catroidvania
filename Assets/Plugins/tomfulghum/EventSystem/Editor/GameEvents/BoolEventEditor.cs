using UnityEngine;
using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(BoolEvent))]
    public class BoolEventEditor : Editor
    {
        bool m_debugData = default;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameEvent = target as BoolEvent;
            m_debugData = EditorGUILayout.Toggle("Debug Data", m_debugData);

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise Event")) {
                gameEvent.Raise(m_debugData);
            }
        }
    }
}
