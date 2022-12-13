using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

[CustomEditor(typeof(OpacityToggle), true)]
[CanEditMultipleObjects]
public class OpacityToggleEditor : ToggleEditor
{
    SerializedProperty m_Opacity;
    SerializedProperty m_OpacityGroup;

    protected override void OnEnable()
    {
        base.OnEnable();

        m_Opacity = serializedObject.FindProperty("m_Opacity");
        m_OpacityGroup = serializedObject.FindProperty("m_opacityGroup");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.Space();

        serializedObject.Update();
        EditorGUILayout.PropertyField(m_Opacity);
        EditorGUILayout.PropertyField(m_OpacityGroup);

        serializedObject.ApplyModifiedProperties();
    }
}
