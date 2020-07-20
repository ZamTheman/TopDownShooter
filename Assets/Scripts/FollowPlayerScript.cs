using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerScript : MonoBehaviour
{
    public Transform target;
    public bool FollowTarget;
    public bool RotateWithTarget;
    
    // Update is called once per frame
    void Update()
    {
        if (FollowTarget)
            transform.position = target.position;
        if (RotateWithTarget)
            transform.rotation = target.rotation;
    }
}
