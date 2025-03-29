using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F3_PathVisualizer : F0_VisualizerBase
{
    private void OnDrawGizmos()
    {
        setMover();
        for (int i = 0; i<mover.pathPoints.Count; i++)
        {
            int nextHop = (i + 1) % mover.pathPoints.Count;
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(mover.pathPoints[i].position, mover.pathPoints[nextHop].position);
        }
    }
}
