using UnityEditor;
using UnityEngine;

public class ExtendedEditorWindow : EditorWindow
{
    protected SerializedObject _serializedObject;
    protected SerializedProperty _currentProperty;

    protected string _selectedPropertyPath;
    protected SerializedProperty _selectedProperty;
    
    protected void DrawProperties(SerializedProperty property, bool isDrawChildren)
    {
        string lastPropertyPath = string.Empty;
        foreach (SerializedProperty p in property)
        {
            if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                EditorGUILayout.EndHorizontal();

                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    DrawProperties(p, isDrawChildren);
                    EditorGUI.indentLevel++;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lastPropertyPath) && p.propertyPath.Contains(lastPropertyPath))
                {
                    continue;
                }

                lastPropertyPath = p.propertyPath;
                EditorGUILayout.PropertyField(p, isDrawChildren);
            }
        }
    }

    protected void DrawSidebar(SerializedProperty property)
    {
        foreach (SerializedProperty p in property)
        {
            if (GUILayout.Button(p.displayName))
            {
                _selectedPropertyPath = p.propertyPath;
            }
        }

        if (!string.IsNullOrEmpty(_selectedPropertyPath))
        {
            _selectedProperty = _serializedObject.FindProperty(_selectedPropertyPath);
        }
    }

    protected void DrawField(string propertyName, bool isRelative)
    {
        if (isRelative && _currentProperty != null)
        {
            EditorGUILayout.PropertyField(_currentProperty.FindPropertyRelative(propertyName), true);
        }
        else if (_serializedObject != null)
        {
            EditorGUILayout.PropertyField(_serializedObject.FindProperty(propertyName), true);
        }
    }

    protected void Apply()
    {
        _serializedObject.ApplyModifiedProperties();
    }
}
