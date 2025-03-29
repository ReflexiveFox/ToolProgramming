using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F2_MovementVisualizer : F0_VisualizerBase
{
    private void OnDrawGizmos()
    {
        setMover();
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position+mover.direction.normalized*mover.movementSpeed);
    }
}
