using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumScript : MonoBehaviour
{
    Rigidbody2D rbody2D;

    public float velocityThreshold;

    public float leftPushRange;
    public float rightPushRange;

    // Start is called before the first frame update
    void Start()
    {
        rbody2D = gameObject.GetComponent<Rigidbody2D>();

        rbody2D.angularVelocity = velocityThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        Push();
    }

    private void Push()
    {
        
        if(transform.rotation.z > 0 && transform.rotation.z < rightPushRange && (rbody2D.angularVelocity > 0) && rbody2D.angularVelocity < velocityThreshold)
        {
            rbody2D.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < 0 && transform.rotation.z > leftPushRange && (rbody2D.angularVelocity < 0) && (rbody2D.angularVelocity > (velocityThreshold * -1)))
        {
            rbody2D.angularVelocity = velocityThreshold * -1;
        }
        
        

        /*
        if (transform.rotation.z > rightPushRange)
        {
            rbody2D.angularVelocity = velocityThreshold;
        }
        else if (transform.rotation.z < leftPushRange)
        {
            rbody2D.angularVelocity = velocityThreshold * -1;
        }
        */
        
    }


}
