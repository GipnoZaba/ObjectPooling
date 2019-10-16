using ObjectPooling;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class AssetHandler
{
    [OnOpenAsset]
    public static bool OpenEditor(int instanceId, int line)
    {
        ObjectPools obj = EditorUtility.InstanceIDToObject(instanceId) as ObjectPools;
        
        if (obj != null)
        {
            PoolsEditorWindow.Open(obj);
        }

        return false;
    }
}

[CustomEditor(typeof(ObjectPools))]
public class PoolsCustomEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Open editor"))
        {
            PoolsEditorWindow.Open((ObjectPools) target);
        }
        
        base.OnInspectorGUI();
    }
}