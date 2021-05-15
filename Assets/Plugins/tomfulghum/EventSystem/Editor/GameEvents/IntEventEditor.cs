using UnityEngine;
using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(IntEvent))]
    public class IntEventEditor : Editor
    {
        int m_debugData = default;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var gameEvent = target as IntEvent;
            m_debugData = EditorGUILayout.IntField("Debug Data", m_debugData);

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise Event")) {
                gameEvent.Raise(m_debugData);
            }
        }
    }
}
