using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEditor.UI
{
    [CustomEditor(typeof(CustomToggle), true)]
    [CanEditMultipleObjects]
    public class CustomToggleEditor : ToggleEditor
    {
        SerializedProperty m_Group;

        protected override void OnEnable()
        {
            base.OnEnable();

            m_Group = serializedObject.FindProperty("m_multipleGroup");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();

            serializedObject.Update();
            EditorGUILayout.PropertyField(m_Group);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
