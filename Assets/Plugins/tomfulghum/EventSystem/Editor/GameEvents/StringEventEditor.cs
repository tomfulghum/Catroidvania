using UnityEngine;
using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(StringEvent))]
    public class StringEventEditor : Editor
    {
        string m_debugData = default;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameEvent = target as StringEvent;
            m_debugData = EditorGUILayout.TextField("Debug Data", m_debugData);

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise Event")) {
                gameEvent.Raise(m_debugData);
            }
        }
    }
}
