using UnityEngine;
using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(BasicEvent))]
    public class BasicEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var basicEvent = target as BasicEvent;

            GUI.enabled = Application.isPlaying;
            if (GUILayout.Button("Raise Event")) {
                basicEvent.Raise();
            }
        }
    }
}
