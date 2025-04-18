using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;

namespace CustomTools
{
    [ExecuteAlways]
    public class Path : MonoBehaviour
    {
        [SerializeField] private bool loop = false;
        [SerializeField, Range(0f, 10f)] private float heightOffset = .5f;
        [SerializeField, Range(1f, 10f)] private float distanceBetweenPoints = .5f;
        [Tooltip("Quanto casuale deve essere la posizione di ogni punto?")]
        [SerializeField, Range(0f, 10f)] private float positionRandomnessFactor = .5f;

        [SerializeField] private GameObject pathPointTemplate;
        [Header("Colors")]
        [SerializeField] private Color startPointColor = Color.green;
        [SerializeField] private Color endPointColor = Color.red;
        [SerializeField] private Color defaultPointColor = Color.blue;
        [SerializeField] private Color pathColor = Color.magenta;
        [Space(10)]
        [SerializeField] private List<Transform> pathPoints = new List<Transform>();
        
        public void Initialize(int pointCount, float randomPathPosition, bool canPathLoop)
        {
            pathPoints = new List<Transform>();
            loop = canPathLoop;
            positionRandomnessFactor = randomPathPosition;
            for (int i = 0; i < pointCount; i++)
            {
                AddPoint(canAddAtBeginning: false);
            }
        }

        public void AddPoint(bool canAddAtBeginning)
        {
            GameObject pathPoint = Instantiate(pathPointTemplate);
            
            pathPoint.transform.SetParent(transform);

            Vector2 randomPoint = Random.insideUnitCircle;
            Vector3 unitRandomPoint3D = new Vector3(randomPoint.x, 0, randomPoint.y);
            
            //Logica piazzamento del punto nella lista
            if(canAddAtBeginning)
                pathPoints.Insert(0, pathPoint.transform);
            else
                pathPoints.Add(pathPoint.transform);

            int pointIndex = canAddAtBeginning ? 0 : pathPoints.Count - 1;

            pathPoint.transform.localPosition = Vector3.up * heightOffset + (Vector3.forward * distanceBetweenPoints * pointIndex) + unitRandomPoint3D * positionRandomnessFactor;

            for (int i = 0; i < pathPoints.Count; i++)
            {
                pathPoints[i].name = $"PathPoint_{i}";
            }
        }

        private void OnDrawGizmos()
        {
            for (int i = 0; i < pathPoints.Count; i++)
            {
                //Coloriamo le icone dei punti del percorso
                Color pointColor = i == 0 ? startPointColor : i == pathPoints.Count - 1 ? endPointColor : defaultPointColor;
                Transform pathPoint = pathPoints[i];
                Gizmos.DrawIcon(pathPoint.position, "Assets/Textures/Icons/flag_white.png", true, pointColor);

                //Disegniamo il percorso
                if (i < pathPoints.Count - 1) // [ 0, 1, 2 ] Count = 3
                {
                    Gizmos.color = pathColor;
                    Gizmos.DrawLine(pathPoints[i].position, pathPoints[i + 1].position);
                    //Handles.DrawLine(pathPoints[i].position, pathPoints[i + 1].position, );
                }
                else if (loop)
                {
                    Gizmos.color = pathColor;
                    Gizmos.DrawLine(pathPoints[i].position, pathPoints[0].position);
                    //Handles.DrawLine(pathPoints[i].position, pathPoints[0].position, );
                }
            }
        }
    }
}