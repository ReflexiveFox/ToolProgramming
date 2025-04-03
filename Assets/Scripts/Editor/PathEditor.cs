using UnityEditor;
using UnityEngine;

namespace CustomTools
{
    public class PathEditor : EditorWindow
    {
        public bool canPathLoop;
        public bool canPassRandomPosition;
        public string randomPathPositionInput;
        
        public float randomPathPositionValue;

        public string textValue;
        [MenuItem("Custom Tools/Path Editor")]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            EditorWindow.GetWindow(typeof(PathEditor));
        }

        void OnGUI()
        {
            GUILayout.Label("Path Editor Window", EditorStyles.boldLabel);
            GUILayout.BeginVertical();
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Path is a loop?", GUILayout.ExpandWidth(false));
            canPathLoop = GUILayout.Toggle(canPathLoop, GUIContent.none);
            GUILayout.EndHorizontal();

            GUILayout.Space(10);

            GUILayout.Label($"Randomness path position: {randomPathPositionValue}");
            randomPathPositionValue = GUILayout.HorizontalSlider(randomPathPositionValue, 0f, 10f, GUILayout.MaxWidth(400));
            float multiplier = Mathf.Pow(10, 2);
            randomPathPositionValue = Mathf.Round(randomPathPositionValue * multiplier) / multiplier;
            
            GUILayout.Space(20);

            if (GUILayout.Button("Create Path"))
            {
                PathManager.Instance.CreatePath(
                    canPassRandomPosition ? randomPathPositionValue : 2f,
                    canPathLoop
                    );                
            }
            
            GUILayout.EndVertical();
        }

        void OnInspectorUpdate()
        {
            // Call Repaint on OnInspectorUpdate as it repaints the windows
            // less times as if it was OnGUI/Update
            Repaint();
        }
    }
}