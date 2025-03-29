using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F1_1_PathPointsVisualizerEnh : F0_VisualizerBase
{
    public void OnDrawGizmosSelected()
    {
        setMover();
        int i = 0;
        foreach (Transform pathPoint in mover.pathPoints)
        {
            if (i == mover.targetPos)
            {
                Gizmos.color = new Color(1f, .2f, .2f);
                Gizmos.DrawSphere(pathPoint.position, mover.pathClearRadius);
            }

            i++;
        }
    }
    
    public void OnDrawGizmos()
    {
        setMover();
        foreach (Transform pathPoint in mover.pathPoints)
        {
            Gizmos.DrawIcon(pathPoint.position, "Assets/Textures/Icons/flag.png");
        }
    }
}
