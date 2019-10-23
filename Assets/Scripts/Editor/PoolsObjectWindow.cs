﻿using ObjectPooling;
using UnityEditor;
using UnityEngine;

public class PoolsObjectWindow : ExtendedEditorWindow
{

    private const string PoolNameName = "_poolName";
    private const string PoolTypeName = "_poolType";
    private const string ObjectPrefabName = "_objectPrefab";
    private const string StartSizeName = "_startSize";
    private const string MaxSizeName = "_maxSize";
    private const string CallbackTypesName = "_callbackTypes";
    
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
            DrawSelectedPropertiesPanel();
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
        EditorGUIUtility.labelWidth = 75f;
        
        _currentProperty = _selectedProperty;
        
        EditorGUILayout.BeginVertical("box");
        
        DrawPoolSettings();

        EditorGUILayout.EndVertical();
    }

    private void DrawPoolSettings()
    {
        DrawField(PoolNameName, true);
        DrawField(PoolTypeName, true);
        DrawField(ObjectPrefabName, true);
        DrawField(StartSizeName, true);
        DrawField(CallbackTypesName, true);
        
        int poolTypeIndex = _selectedProperty.FindPropertyRelative(PoolTypeName).enumValueIndex;
        
        switch ((PoolType) poolTypeIndex)
        {
            case PoolType.FixedSize:
                DrawFixedSizePool();
                break;
            case PoolType.DynamicSize:
                break;
            case PoolType.FixedSizeReusable:
                break;
            case PoolType.DynamicSizeReusable:
                break;
        }
    }

    private void DrawFixedSizePool()
    {
        
    }
}