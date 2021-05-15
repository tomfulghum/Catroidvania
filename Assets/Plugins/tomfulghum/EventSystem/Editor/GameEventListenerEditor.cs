using UnityEditor;

namespace tomfulghum.EventSystem
{
    [CustomEditor(typeof(GameEventListener))]
    public class GameEventListenerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            SerializedProperty basicEventResponse = serializedObject.FindProperty("m_basicEventResponse");
            SerializedProperty boolEventResponse = serializedObject.FindProperty("m_boolEventResponse");
            SerializedProperty intEventResponse = serializedObject.FindProperty("m_intEventResponse");
            SerializedProperty floatEventResponse = serializedObject.FindProperty("m_floatEventResponse");
            SerializedProperty stringEventResponse = serializedObject.FindProperty("m_stringEventResponse");

            var gameEvent = serializedObject.FindProperty("m_event").objectReferenceValue as GameEvent;

            if (gameEvent) {
                if (gameEvent.GetType().Equals(typeof(BasicEvent))) {
                    EditorGUILayout.PropertyField(basicEventResponse);
                } else if (gameEvent.GetType().Equals(typeof(BoolEvent))) {
                    EditorGUILayout.PropertyField(boolEventResponse);
                } else if (gameEvent.GetType().Equals(typeof(IntEvent))) {
                    EditorGUILayout.PropertyField(intEventResponse);
                } else if (gameEvent.GetType().Equals(typeof(FloatEvent))) {
                    EditorGUILayout.PropertyField(floatEventResponse);
                } else if (gameEvent.GetType().Equals(typeof(StringEvent))) {
                    EditorGUILayout.PropertyField(stringEventResponse);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
