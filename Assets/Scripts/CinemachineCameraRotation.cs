using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCameraRotation : MonoBehaviour
{
    Vector3 origPosition;

    // Start is called before the first frame update
    void Start()
    {
        origPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.eulerAngles = origEuler;
    }
}
