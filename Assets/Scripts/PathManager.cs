using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

namespace CustomTools
{
    public class PathManager : MonoBehaviourSingleton<PathManager>
    {
        [SerializeField] private Color pathManagerColor = Color.yellow;
        [SerializeField] private GameObject pathTemplate;
        [Space(10)]
        public List<Path> paths = new List<Path>();

        private void Start()
        {
            paths = new List<Path>();
        }

        public Path CreatePath(float randomPathPosition, bool canPathLoop)
        {
            GameObject pathGO = Instantiate(pathTemplate);
            pathGO.name = $"Path_Container_{paths.Count}";
            pathGO.transform.position = Vector3.zero;

            Path path = pathGO.GetComponent<Path>();
            path.Initialize(3, randomPathPosition, canPathLoop);
            paths.Add(path);
            return path;
        }

        public void OnDrawGizmos()
        {
            //GUIStyle style = new GUIStyle();
            //style.normal.textColor = Color.white;
            //style.fontSize = 22;
            //Handles.Label(transform.position + Vector3.up, $"Paths {paths.Count}", style);
            Gizmos.DrawIcon(transform.position, "Assets/Textures/Icons/flag_white.png", false, pathManagerColor);
        }
    }
}