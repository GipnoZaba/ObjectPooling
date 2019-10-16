using UnityEditor;

namespace Editor
{
    public class PoolingEditorWindow : EditorWindow
    {
        [MenuItem("Window/Pooling System/Pooling")]
        public static void ShowWindow()
        {
            GetWindow<PoolingEditorWindow>("Pooling");
        }

        private void OnGUI()
        {
            
        }
    }
}