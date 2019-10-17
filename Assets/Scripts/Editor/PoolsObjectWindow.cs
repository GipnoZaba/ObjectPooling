using ObjectPooling;
using UnityEditor;
using UnityEngine;

public class PoolsObjectWindow : ExtendedEditorWindow
{
    public static void Open(PoolsObject pools)
    {
        PoolsObjectWindow windows = GetWindow<PoolsObjectWindow>();
        windows._serializedObject = new SerializedObject(pools);
    }

    private void OnGUI()
    {
        _currentProperty = _serializedObject.FindProperty("_pools");

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
        
        DrawSidebar(_currentProperty);
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

        if (_selectedProperty != null)
        {
            DrawProperties(_selectedProperty, true);
        }
        else
        {
            EditorGUILayout.LabelField("Select a pool from the list ");
        }
        
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Apply();
    }

    private void DrawSelectedPropertiesPanel()
    {
        _currentProperty = _selectedProperty;
        
        EditorGUILayout.BeginVertical("box");
        /*
        DrawField("maxSize", true);
        DrawField("prefab", true);
        */
        EditorGUILayout.EndVertical();
    }
}