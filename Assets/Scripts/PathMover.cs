using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[HelpURL("http://example.com/docs/MyComponent.html")]
[Icon("Assets/Textures/Icons/bomb.png")]
public class PathMover : MonoBehaviour
{
    [Tooltip("Speed in m/s")]
    [Range(0.2f, 50.0f)]
    public float movementSpeed = 2;
    [Range(1.0f, 5.0f)]
    public float pathClearRadius = 1;
    [Space(20)]
    [Header("Path Points")]
    public List<Transform> pathPoints;
    [HideInInspector]
    public int targetPos = 0;
    [HideInInspector]
    public Vector3 direction;
    
    void Start()
    {
        if (pathPoints.Count == 0)
           return;
        
        transform.position = pathPoints[targetPos].position;
    }
    
    void Update()
    {
        direction = pathPoints[targetPos].position - transform.position;
        float distance = direction.magnitude;

        if (distance <= pathClearRadius)
            targetPos = (targetPos + 1) % pathPoints.Count;

        transform.LookAt(pathPoints[targetPos]);
        
        transform.position = transform.position + direction.normalized * (movementSpeed * Time.deltaTime);
    }
}
