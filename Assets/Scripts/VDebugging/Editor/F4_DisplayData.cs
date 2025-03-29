using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(PathMover))]
public class F4_DisplayData : Editor
{
    public void OnSceneGUI()
    {
        PathMover pm = target as PathMover;
        GUIStyle st = new GUIStyle();
        st.fontSize = 32;
        st.normal.textColor = Color.green;
        Handles.Label(pm.transform.position + new Vector3(0,1.2f,0), pm.movementSpeed.ToString(), st);
    }

}
