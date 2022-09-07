using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentSelectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(SegmentScript.Instance)
        {
            if(SegmentScript.Instance.segmentSelected >= 0)
            {
                gameObject.transform.GetChild(SegmentScript.Instance.segmentSelected).transform.gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.parent.transform.GetChild(4).gameObject.SetActive(false);
            }
            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
