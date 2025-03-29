using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                canPathLoop = GUILayout.Toggle(canPathLoop, "Path is a loop?");
                randomPathPositionInput = randomPathPositionInput.Replace(',', '.');

                canPassRandomPosition = float.TryParse(randomPathPositionInput, out randomPathPositionValue);
                GUILayout.EndHorizontal();

            
            GUILayout.BeginHorizontal();
            // Option 1: Basic horizontal slider
            randomPathPositionValue = GUILayout.HorizontalSlider(randomPathPositionValue, 0f, 10f);

            // Option 2: Slider with label and value
            GUILayout.Label($"Randomness path position: "/*{Mathf.RoundToInt(randomPathPositionValue)}"*/);

            //GUILayout.Label("Randomness path position");
            //randomPathPositionInput = GUILayout.TextField(randomPathPositionInput);
            //randomPathPositionInput = randomPathPositionInput.Replace(',', '.');

            //canPassRandomPosition = float.TryParse(randomPathPositionInput, out randomPathPositionValue);
            GUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            if (GUILayout.Button("Create Path"))
            {
                PathManager.Instance.CreatePath(
                    canPassRandomPosition ? randomPathPositionValue : 2f,
                    canPathLoop
                    );                
            }
            
            GUILayout.EndVertical();
            GUILayout.Space(30);

            Transform act = Selection.activeTransform;
            // Handle enable/disable
            GUI.enabled = act != null && act.gameObject.GetComponent<PathMover>() != null;
            if (GUILayout.Button("Attach Debug Scripts"))
                attachDebug();
            GUI.enabled = act != null && act.gameObject.GetComponent<F1_1_PathPointsVisualizerEnh>() != null;
            if (GUILayout.Button("Detach Debug Scripts"))
                detachDebug();
            GUI.enabled = true;

            GUILayout.Space(30);
            if (GUILayout.Button("Build!"))
                invokeMenu();
        }

        public void invokeMenu()
        {
            EditorApplication.ExecuteMenuItem("File/Build Settings...");
        }


        public void attachDebug()
        {
            if (Selection.activeTransform != null)
            {
                Selection.activeTransform.gameObject.AddComponent<F1_1_PathPointsVisualizerEnh>();
                Scene currentScene = SceneManager.GetActiveScene();
                EditorSceneManager.MarkSceneDirty(currentScene);
            }
        }

        public void detachDebug()
        {
            if (Selection.activeTransform != null)
            {
                GameObject go = Selection.activeTransform.gameObject;
                F1_1_PathPointsVisualizerEnh cmp = go.GetComponent<F1_1_PathPointsVisualizerEnh>();
                GameObject.DestroyImmediate(cmp);

                Scene currentScene = SceneManager.GetActiveScene();
                EditorSceneManager.MarkSceneDirty(currentScene);
            }
        }

        void OnInspectorUpdate()
        {
            // Call Repaint on OnInspectorUpdate as it repaints the windows
            // less times as if it was OnGUI/Update
            Repaint();
        }
    }
}