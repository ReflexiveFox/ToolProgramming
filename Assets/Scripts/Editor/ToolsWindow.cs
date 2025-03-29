using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ToolsWindow : EditorWindow
{
    public string textValue;
    [MenuItem("FITSTIC/ToolsWindow")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        EditorWindow.GetWindow(typeof(ToolsWindow));
    }
    
    void OnGUI()
    {
        GUILayout.Label ("Tools Window", EditorStyles.boldLabel);
        GUILayout.BeginVertical();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Test Button"))
            EditorUtility.DisplayDialog("Do you really...", "Ok, this is the message", "GO GO GO!");
        GUILayout.Button("Useless Button");
        GUILayout.EndHorizontal();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Label("Label",  GUILayout.Width(100));
        textValue = GUILayout.TextArea(textValue);
        GUILayout.EndHorizontal();
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