using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1_PathPointsVisualizer : F0_VisualizerBase
{
    public void OnDrawGizmos()
    {
        setMover();

        int i = 0;
        foreach (Transform pathPoint in mover.pathPoints)
        {
            if( i == mover.targetPos )
                Gizmos.color = new Color(1f, .2f, .2f);
            else
                Gizmos.color = new Color(.6f, .6f, .6f);
            Gizmos.DrawSphere(pathPoint.position,1);
            i++;
        }
    }
}
