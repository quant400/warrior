using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInputToggle : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

#if UNITY_ANDROID

        gameObject.SetActive(true);

#endif

    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
