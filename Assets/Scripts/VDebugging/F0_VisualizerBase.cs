using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F0_VisualizerBase : MonoBehaviour
{
    protected PathMover mover;
    
    protected void setMover()
    {
        if(mover == null)
            mover = GetComponent<PathMover>();
    }
}
