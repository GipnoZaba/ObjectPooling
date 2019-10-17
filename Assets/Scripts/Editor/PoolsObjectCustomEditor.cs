using ObjectPooling;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset]
    public static bool OpenEditor(int instanceId, int line)
    {
        PoolsObject obj = EditorUtility.InstanceIDToObject(instanceId) as PoolsObject;
        
        if (obj != null)
        {
            PoolsObjectWindow.Open(obj);
        }

        return false;
    }
}

[CustomEditor(typeof(PoolsObject))]
public class PoolsObjectCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open editor"))
        {
            PoolsObjectWindow.Open((PoolsObject) target);
        }
        
        base.OnInspectorGUI();
    }
}