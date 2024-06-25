using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantiAttentionBoxTrigger : MonoBehaviour
{
    public SantiInteractiveObjectController action;
    public float timer;
    public float actionTime;
    public bool enterTrigger = false;

    private void Start()
    {
        timer = actionTime;
    }

}
    
